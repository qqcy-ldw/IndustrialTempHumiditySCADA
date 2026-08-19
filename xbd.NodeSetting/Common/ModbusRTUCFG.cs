using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using thinger.DataConvertLib;
using xbd.NodeSetting.Base;
using xbd.NodeSetting.Enums;
using xbd.NodeSetting.ModbusRTU;

namespace xbd.NodeSetting.Common
{
    public class ModbusRTUCFG
    {
        /// <summary>
        /// 解析文件夹里面的电子表格
        /// </summary>
        /// <param name="PathFolder">目标文件夹</param>
        /// <returns></returns>
        public static OperateResult<List<ModbusRTUDevice>> LoadDevice(String PathFolder)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(PathFolder);
                List<ModbusRTUDevice> modbusRTUs = new();
                if (!dirInfo.Exists)
                {
                    return OperateResult.CreateFailResult<List<ModbusRTUDevice>>("文件夹不存在");
                }

                //获取该目录下所有的xlsx文件信息
                foreach (var file in dirInfo.GetFiles("*.xlsx"))
                {
                    // 验证文件名称
                    var device = GetDevice(file.Name.Replace(".xlsx", ""));
                    if (!device.IsSuccess) return OperateResult.CreateFailResult<List<ModbusRTUDevice>>($"{file.Name}：{device.Message}");

                    List<string> sheets = MiniExcel.GetSheetNames(file.FullName);
                    foreach (var sheet in sheets)
                    {
                        // 验证文件sheet名称
                        var group = GetGroup(sheet);
                        if (!group.IsSuccess) return OperateResult.CreateFailResult<List<ModbusRTUDevice>>($"{sheet}：{group.Message}");
                        // 获取sheet页面的字段
                        try
                        {
                            group.Content.VariableList = MiniExcel.Query<ModbusRTUVariable>(file.FullName, sheet).ToList();
                            device.Content.GroupList.Add(group.Content);
                        }
                        catch (Exception ex)
                        {
                            return OperateResult.CreateFailResult<List<ModbusRTUDevice>>($"解析变量错误：{ex.Message}");
                        }
                    }
                    modbusRTUs.Add(device.Content);
                }
                return OperateResult.CreateSuccessResult(modbusRTUs);
            }
            catch (IOException ex)
            {
                return OperateResult.CreateFailResult<List<ModbusRTUDevice>>($"读取Excel失败：文件被占用，请关闭Excel表格！{ex.Message}");
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult<List<ModbusRTUDevice>>($"解析异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 解析文件名称
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public static OperateResult<ModbusRTUDevice> GetDevice(string fileName)
        {
            if (!fileName.Contains('_'))
            {
                return OperateResult.CreateFailResult<ModbusRTUDevice>("文件名称不符合要求");
            }
            string[] info = fileName.Split('_');

            if (info.Length == 6)
            {
                if (!int.TryParse(info[2], out int baudRate)) return OperateResult.CreateFailResult<ModbusRTUDevice>("波特率解析失败");
                if (!Parity.TryParse(info[3], out Parity parse)) return OperateResult.CreateFailResult<ModbusRTUDevice>("校验位解析失败");
                if (!int.TryParse(info[4], out int dataBits)) return OperateResult.CreateFailResult<ModbusRTUDevice>("数据位解析失败");
                if (!StopBits.TryParse(info[5], out StopBits stopBits)) return OperateResult.CreateFailResult<ModbusRTUDevice>("停止位解析失败");
                return OperateResult.CreateSuccessResult
                    (
                        new ModbusRTUDevice()
                        {
                            DeviceName = info[0],
                            PortName = info[1],
                            BaudRate = baudRate,
                            Parity = parse,
                            DataBits = dataBits,
                            StopBits = stopBits
                        }
                    );
            }
            else
            {
                return OperateResult.CreateFailResult<ModbusRTUDevice>("解析失败目标长度不等于6");
            }
        }

        public static OperateResult<ModbusRTUGroup> GetGroup(string sheetName)
        {
            if (!sheetName.Contains('_'))
            {
                return OperateResult.CreateFailResult<ModbusRTUGroup>("文件名称不符合要求");
            }
            string[] info = sheetName.Split('_');
            if (info.Length == 5)
            {
                string storeName = info[1].Trim();
                ModbusStore storeArea;
                if (!ModbusStore.TryParse(storeName, ignoreCase: true, out storeArea))
                {
                    // 地址前缀区（行业传统：0/1/3/4）：0区=线圈 / 1区=离散输入 / 3区=输入寄存器 / 4区=保持寄存器
                    // 功能码区（按 Modbus 功能码编号 01~04）：功能码1区=线圈 / 功能码2区=离散输入 / 功能码3区=保持寄存器 / 功能码4区=输入寄存器
                    switch (storeName)
                    {
                        case "输出线圈":
                        case "线圈":
                        case "线圈存储区":
                        case "0区":
                        case "功能码1区":
                            storeArea = ModbusStore.Coil;
                            break;

                        case "输入线圈":
                        case "离散输入":
                        case "离散量输入":
                        case "离散输入寄存器":
                        case "1区":
                        case "功能码2区":
                            storeArea = ModbusStore.DiscreteInput;
                            break;

                        case "输入寄存器":
                        case "只读寄存器":
                        case "3区":
                        case "功能码4区":
                            storeArea = ModbusStore.InputRegister;
                            break;
                        case "保持寄存器":
                        case "输出寄存器":
                        case "读写寄存器":
                        case "保持":
                        case "4区":
                        case "功能码3区":
                            storeArea = ModbusStore.HoldingRegister;
                            break;
                        default:
                            return OperateResult.CreateFailResult<ModbusRTUGroup>(
                                $"存储区解析失败：不支持的存储区名称 \"{storeName}\"。{Environment.NewLine}" +
                                $"支持写法：{Environment.NewLine}" +
                                $" 中文：保持寄存器 / 输入寄存器 / 线圈 / 离散输入{Environment.NewLine}" +
                                $" 编号简写：0区 /1区 /3区 /4区{Environment.NewLine}" +
                                $" 英文枚举：HoldingRegister / InputRegister / Coil / DiscreteInput");
                    }
                }
                if (!byte.TryParse(info[2], out byte groupId)) return OperateResult.CreateFailResult<ModbusRTUGroup>("通讯组解析失败");
                if (!ushort.TryParse(info[3], out ushort start)) return OperateResult.CreateFailResult<ModbusRTUGroup>("开始地址解析失败");
                if (!ushort.TryParse(info[4], out ushort length)) return OperateResult.CreateFailResult<ModbusRTUGroup>("长度解析失败");
                return OperateResult.CreateSuccessResult
                    (
                        new ModbusRTUGroup()
                        {
                            GroupName = info[0],
                            StoreArea = storeArea,
                            GroupId = groupId,
                            Start = start,
                            Length = length
                        }
                    );
            }
            else
            {
                return OperateResult.CreateFailResult<ModbusRTUGroup>("解析失败目标长度不等于5");
            }
        }
    }
}
