﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using thinger.DataConvertLib;
using xbd.NodeSetting.Base;
using xbd.NodeSetting.Common;
using xbd.NodeSetting.Enums;

namespace xbd.NodeSetting.ModbusRTU
{
    public class ModbusRTUDevice : DeviceBase
    {

        /// <summary>
        /// 端口号
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get; set; }

        /// <summary>
        /// 校验位
        /// </summary>
        public Parity Parity { get; set; }

        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { get; set; }

        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits StopBits { get; set; }

        public List<ModbusRTUGroup> GroupList { get; set; } = new List<ModbusRTUGroup>();

        private modbus.CommunicationLibl.Library.ModbusRTU modbusRTU = new();

        // 定义字典存储每个组的最后重试时间
        private Dictionary<ModbusRTUGroup, DateTime> _retrySchedule = new();

        /// <summary>
        /// 执行周期（MS）
        /// </summary>
        public long CommPeriod { get; set; }
        /// <summary>
        /// 开启一个串口读取
        /// </summary>
        public void Start()
        {
            cts = new CancellationTokenSource();
            Task.Run(() =>
            {
                GetModbusRTUValue();
            }, cts.Token);
        }

        private void GetModbusRTUValue()
        {
            while (!cts.IsCancellationRequested)
            {
                if (IsConnected)
                {
                    cts.Cancel();

                    Stopwatch StopWatch = Stopwatch.StartNew();
                    foreach (var gp in GroupList)
                    {
                        gp.IsOK = GetGroupValue(gp);
                    }
                    var now = DateTime.Now;
                    // 处理失败组：只有到时间才重试
                    foreach (var ngGroup in GroupList.Where(g => !g.IsOK))
                    {
                        if (!_retrySchedule.TryGetValue(ngGroup, out var nextRetryTime) || now >= nextRetryTime)
                        {
                            // 到了重试时间，再试一次
                            GetGroupValue(ngGroup);
                            // 设置下一次重试时间为5秒后
                            _retrySchedule[ngGroup] = now.AddSeconds(5);
                        }
                    }

                    // 清理已恢复的组的重试计划
                    foreach (var gp in GroupList.Where(g => g.IsOK))
                    {
                        _retrySchedule.Remove(gp);
                    }

                    // 如果所有的组数据读取失败并且端口号不存在断线重连
                    if (GroupList.Where(c => c.IsOK).Count() == GroupList.Count())
                    {
                        if (!SerialPort.GetPortNames().Contains(PortName))
                        {
                            IsConnected = false;
                        }
                    }
                    CommPeriod = StopWatch.ElapsedMilliseconds;
                }
                else
                {
                    if (!FirstConnectSign) Thread.Sleep(ReConnectTime);
                    IsConnected = modbusRTU.Connect(PortName, BaudRate, DataBits, Parity, StopBits);
                    if (IsConnected) FirstConnectSign = false;
                }
            }
        }

        /// <summary>
        /// 单个通讯组读取（sheet名称）
        /// </summary>
        /// <param name="mpg"></param>
        private bool GetGroupValue(ModbusRTUGroup mpg)
        {
            if (mpg == null) return false;
            // 按存储区读（带 ReadTimes 次重试，成功就退出循环）
            for (int i = 0; i < mpg.ReadTimes; i++)
            {
                OperateResult<bool[]> cResult = null;
                OperateResult<byte[]> rResult = null;

                switch (mpg.StoreArea)
                {
                    case ModbusStore.Coil:
                        cResult = modbusRTU.ReadCoils(mpg.Start, mpg.Length, mpg.GroupId);
                        break;
                    case ModbusStore.DiscreteInput:
                        cResult = modbusRTU.ReadInputs(mpg.Start, mpg.Length, mpg.GroupId);
                        break;
                    case ModbusStore.InputRegister:
                        rResult = modbusRTU.ReadInputRegisters(mpg.Start, mpg.Length, mpg.GroupId);
                        break;
                    case ModbusStore.HoldingRegister:
                        rResult = modbusRTU.ReadHoldingRegisters(mpg.Start, mpg.Length, mpg.GroupId);
                        break;
                    default:
                        return false;
                }

                // 校验：线圈类看 cResult，寄存器类看 rResult
                bool ok = (cResult != null && cResult.IsSuccess && cResult.Content.Length == mpg.Length)
                       || (rResult != null && rResult.IsSuccess && rResult.Content.Length == mpg.Length * 2);
                // 读失败，读取下一轮
                if (!ok) continue;  

                // 遍历变量解析（单个变量解析失败跳过，不影响其他变量）
                foreach (var item in mpg.VariableList)
                {
                    try
                    {
                        var add = AnalysisAddress(item.Address);
                        if (!add.IsSuccess) continue;

                        int regAddr = add.Content1;        // 变量首寄存器/线圈号
                        int bitOffset = add.Content2;      // 寄存器内 bit 位（0~15）

                        //工作表名称 `A01_保持寄存器_1_100_10` 代表起始读取寄存器地址 100，读取长度 10，
                        //对应寄存器范围 100~109；Excel 点位配置的变量地址 regAddr = 102，在本次读取返回的寄存器数组中，
                        //下标为 `102 - 100 = 2`，即该点位对应返回数组中下标 2 的元素。
                        int offset = regAddr - mpg.Start;
                        int regCount = item.DataType switch
                        {
                            DataType.Float or DataType.Int or DataType.UInt => 2,
                            DataType.Double or DataType.Long or DataType.ULong => 4,
                            _ => 1
                        };
                        int lastRegIdx = offset + regCount - 1;
                        if (offset < 0 || lastRegIdx >= mpg.Length)
                        {
                            // 配置错误：变量地址区间 [regAddr, regAddr+regCount-1] 超出了 Sheet 组读取范围
                            continue;
                        }

                        if (cResult != null)
                        {
                            // 线圈/离散输入：每个地址 1 个 bool，小数位无意义，直接按下标取
                            item.VarValue = cResult.Content[offset];
                        }
                        else
                        {
                            // 寄存器类：字节下标 = 寄存器偏移 × 2（每寄存器 2 字节）
                            int byteIndex = offset * 2;
                            switch (item.DataType)
                            {
                                case DataType.Bool:
                                    // 寄存器内取单 bit（与 WORD_ORDER 字节序一致，和其他数据类型同规则）：参数=字节数组 / 起始字节下标 / bit偏移(0~15) / 是否需要高低字节交换(BADC/DCBA 传 true)
                                    item.VarValue = BitLib.GetBitFrom2BytesArray(rResult.Content, byteIndex,
                                        bitOffset, DataFormat == DataFormat.BADC || DataFormat == DataFormat.DCBA);
                                    break;
                                case DataType.Byte:
                                    item.VarValue = ByteLib.GetByteFromByteArray(rResult.Content, byteIndex);
                                    break;
                                case DataType.UShort:
                                    item.VarValue = UShortLib.GetUShortFromByteArray(rResult.Content, byteIndex, DataFormat.ABCD);
                                    break;
                                case DataType.Short:
                                    item.VarValue = ShortLib.GetShortFromByteArray(rResult.Content, byteIndex, DataFormat);
                                    break;
                                case DataType.UInt:
                                    item.VarValue = UIntLib.GetUIntFromByteArray(rResult.Content, byteIndex, DataFormat);
                                    break;
                                case DataType.Int:
                                    item.VarValue = IntLib.GetIntFromByteArray(rResult.Content, byteIndex, DataFormat);
                                    break;
                                case DataType.ULong:
                                    item.VarValue = ULongLib.GetULongFromByteArray(rResult.Content, byteIndex, DataFormat);
                                    break;
                                case DataType.Long:
                                    item.VarValue = LongLib.GetLongFromByteArray(rResult.Content, byteIndex, DataFormat);
                                    break;
                                case DataType.Float:
                                    item.VarValue = FloatLib.GetFloatFromByteArray(rResult.Content, byteIndex, DataFormat);
                                    break;
                                case DataType.Double:
                                    item.VarValue = DoubleLib.GetDoubleFromByteArray(rResult.Content, byteIndex, DataFormat);
                                    break;
                                case DataType.String:
                                    byte[] bytes = ByteArrayLib.GetByteArrayFromByteArray(rResult.Content, byteIndex, mpg.Length * 2);
                                    // 小端
                                    if (DataFormat == DataFormat.BADC || DataFormat == DataFormat.DCBA)
                                    {
                                        item.VarValue = StringLib.GetStringFromByteArrayByEncoding(bytes, 0, bytes.Length, Encoding.ASCII).Replace("\0", "");
                                    }
                                    else
                                    {
                                        List<byte> data = new();
                                        for (int j = 0; j < bytes.Length; j += 2)
                                        {
                                            data.Add(bytes[j + 1]);
                                            data.Add(bytes[j]);
                                        }
                                        item.VarValue = StringLib.GetStringFromByteArrayByEncoding(data.ToArray(), 0, data.Count, Encoding.ASCII).Replace("\0", "");
                                    }
                                    break;
                                case DataType.ByteArray:
                                    item.VarValue = ByteArrayLib.GetByteArrayFromByteArray(rResult.Content, byteIndex, mpg.Length * 2);
                                    break;
                            }
                        }
                        item.VarValue = GetMigrationValue(item.VarValue, item.Scale, item.Offset).Content;
                        // 变量解析换算完成 → 写入全局实时值缓存（供 UI 层直接取数显示）
                        UpdateVariable(item);
                    }
                    catch
                    {
                        // 单个变量解析异常 → 跳过，继续其他变量
                    }
                }
                return true;   // 读成功并解析完成
            }
            return false;   // 重试耗尽也没读成功
        }

        /// <summary>
        /// 关不一个串口读取
        /// </summary>
        public void Stop()
        {

        }

        #region======通用方法========
        /// <summary>
        /// 获取线性转换结果
        /// </summary>
        /// <param name="value">原始值</param>
        /// <param name="scale">线性系数</param>
        /// <param name="offset">线性偏移</param>
        /// <returns>带操作结果的转换结果</returns>
        [Description("获取线性转换结果")]
        public static OperateResult<object> GetMigrationValue(object value, float scale, float offset)
        {
            object val;
            try
            {
                string type = value.GetType().Name;
                switch (type.ToLower())
                {
                    case "byte":
                    case "int16":
                    case "uint16":
                    case "int32":
                    case "uint32":
                    case "single":
                        val = Convert.ToSingle(Convert.ToSingle(value) * scale + offset);
                        break;
                    case "int64":
                    case "uint64":
                    case "double":
                        val = Convert.ToDouble(Convert.ToDouble(value) * scale + offset);
                        break;
                    default:
                        val = value;
                        break;
                }

                if (val is float fVal)
                {
                    //保留 1 位小数
                    fVal = (float)Math.Round(fVal, 1);
                    //温度下钳位（示例：-40℃）
                    if (fVal < -40f) fVal = -40f;
                    //温度上钳位（示例：125℃）
                    if (fVal > 125f) fVal = 125f;
                    val = fVal;
                }
                else if (val is double dVal)
                {
                    //保留 1 位小数
                    dVal = Math.Round(dVal, 1);
                    //温度下钳位
                    if (dVal < -40.0) dVal = -40.0;
                    // 温度上钳位
                    if (dVal > 125.0) dVal = 125.0;
                    val = dVal;
                }
                else if (val is IConvertible)
                {
                    // 整数类型（Byte/UShort/Int…）：
                    // 如果加了偏移后超出范围，也顺手做基础钳位（湿度不允许 >100 这种需求以后也加在这里）
                    double tmp = Convert.ToDouble(val);
                    //整数型（比如湿度百分比 0~100）不允许负数
                    if (tmp < 0.0) tmp = 0.0;
                    // UInt16 存湿度时上限 100
                    if (tmp > 100.0 && type.ToLower().Contains("int16")) tmp = 100.0;
                    val = Convert.ChangeType(tmp, val.GetType());
                }

                return OperateResult.CreateSuccessResult(val);
            }
            catch (Exception ex)
            {
                return new OperateResult<object>("转换出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 地址解析
        /// 变量地址写 10005.11 →  意思是「保持寄存器 10005 的第 11 个 bit 位」；
        /// 设备里会把 一组布尔量（报警标志位、设备运行状态位）打包塞进一个 16 位寄存器里，
        /// 这样一次 Modbus 读 10 个寄存器就能拿到 160 个布尔状态，效率比单独读线圈高得多
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private OperateResult<ushort, ushort> AnalysisAddress(string address)
        {
            ushort start = 0;
            ushort bitOffset = 0;

            if (address.Contains('.'))
            {
                string[] result = address.Split('.');
                if (result.Length == 2
                    && ushort.TryParse(result[0], out start)
                    && ushort.TryParse(result[1], out bitOffset))
                {
                    // 寄存器内 bit 位必须在 0~15 之间（16 位寄存器）
                    if (bitOffset > 15)
                        return OperateResult.CreateFailResult<ushort, ushort>($"bit位偏移[{bitOffset}]超范围(0~15): {address}");

                    return OperateResult.CreateSuccessResult(start, bitOffset);
                }

                return OperateResult.CreateFailResult<ushort, ushort>("地址格式不正确: " + address);
            }

            if (ushort.TryParse(address, out start))
            {
                return OperateResult.CreateSuccessResult(start, bitOffset);
            }

            return OperateResult.CreateFailResult<ushort, ushort>("地址格式不正确: " + address);
        }
        #endregion 
    }
}