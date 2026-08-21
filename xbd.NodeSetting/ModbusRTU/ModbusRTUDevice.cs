﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using thinger.DataConvertLib;
using xbd.NodeSetting.Base;
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

        private Dictionary<string, ModbusRTUVariable> _variableList = new();

        public void info()
        {
            foreach (var groupList in GroupList)
            {
                foreach (var variable in groupList.VariableList)
                {
                    if (_variableList.ContainsKey(variable.VarName))
                    {
                        _variableList[variable.VarName] = variable;
                    }
                    else
                    {
                        _variableList.Add(variable.VarName, variable);
                    }
                }
            }
        }

        private modbus.CommunicationLibl.Library.ModbusRTU modbusRTU = new();
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
            while (true)
            {
                if (!cts.IsCancellationRequested)
                {
                    foreach (var gp in GroupList)
                    {
                        GetGroupValue(gp);
                    }
                }
                else
                {
                    if (!FirstConnectSign) Thread.Sleep(ReConnectTime);
                    IsConnected = modbusRTU.Connect(PortName, BaudRate, DataBits, Parity, StopBits);
                    if (IsConnected) IsConnected = false;
                }
            }
        }

        /// <summary>
        /// 当个通讯组读取（sheet名称）
        /// </summary>
        /// <param name="mpg"></param>
        private bool GetGroupValue(ModbusRTUGroup mpg)
        {
            if (mpg == null) return false;

            // 寄存器字序：国内 Modbus 温湿度采集模块 / PLC 90% 是 BADC（寄存器内大端，双寄存器字序反）；
            // 如果读出的 Float/Double 是乱值/负数，把下面这一行改成 DataFormat.BADC 再试。

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
                if (!ok) continue;   // 读失败，重试下一轮

                // 遍历变量解析（单个变量解析失败跳过，不影响其他变量）
                foreach (var item in mpg.VariableList)
                {
                    try
                    {
                        var add = AnalysisAddress(item.Address);
                        if (!add.IsSuccess) continue;

                        int regAddr = add.Content1;        // 变量首寄存器/线圈号
                        int bitOffset = add.Content2;      // 寄存器内 bit 位（0~15）

                        //- Sheet 名： A01_保持寄存器_1_**100**_10 → mpg.Start=100 、Length=10（读设备 地址 100~109 这 10 个寄存器）
                        //-温度变量 Excel 里写的变量地址 regAddr = 102 （就是设备上的第 102 号寄存器） 
                        //简单来说这个就是放回读取数据数组的下标
                        int offset = regAddr - mpg.Start;  // 相对组起始的偏移

                        // —— 区间+占用校验：单寄存器/多寄存器（Float/Double 等）尾部都要在范围内 ——
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
                        UpdateValue(item);
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

        /// <summary>
        /// 更新单个变量的实时值到全局缓存 CurrentValue
        /// UI 层通过 CurrentValue[变量名] 直接取最近一次有效的值，不用遍历 Modbus 内部的 Group/VariableList
        /// </summary>
        /// <param name="variable">变量对象（必须有 VarName 作为 Key、VarValue 为最终换算后的值）</param>
        private void UpdateValue(VariableBase variable)
        {
            // VarName 空值直接跳过，避免 KeyNotFoundException
            if (string.IsNullOrWhiteSpace(variable.VarName)) return;

            // 线程安全写入：有 Key 就更新，没有就新增，ConcurrentDictionary 内部保证原子性
            CurrentValue.AddOrUpdate(
                key: variable.VarName,
                addValue: variable.VarValue,
                updateValueFactory: (_, _) => variable.VarValue);
        }

        #region
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
            if (scale == 1.0 && offset == 0.0)
            {
                return OperateResult.CreateSuccessResult(value);
            }
            else
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
                            val = Convert.ToSingle((Convert.ToSingle(value) * scale + offset).ToString("N4"));
                            break;
                        case "int64":
                        case "uint64":
                        case "double":
                            val = Convert.ToDouble((Convert.ToDouble(value) * scale + offset).ToString("N4"));
                            break;
                        default:
                            val = value;
                            break;
                    }
                    return OperateResult.CreateSuccessResult(val);
                }
                catch (Exception ex)
                {
                    return new OperateResult<object>("转换出错：" + ex.Message);
                }

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