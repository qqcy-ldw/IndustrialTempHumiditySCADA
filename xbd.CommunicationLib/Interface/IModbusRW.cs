using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thinger.DataConvertLib;

namespace modbus.CommunicationLibl.Interface
{
    public interface IModbusRW
    {
        /// <summary>
        /// 读输出线圈 功能码01
        /// </summary>
        OperateResult<bool[]> ReadCoils(ushort start, ushort length, byte slaveId = 1);

        /// <summary>
        /// 读输入线圈 功能码02
        /// </summary>
        OperateResult<bool[]> ReadInputs(ushort start, ushort length, byte slaveId = 1);

        /// <summary>
        /// 读保持寄存器 功能码03，返回byte数组（标准大端）
        /// </summary>
        OperateResult<byte[]> ReadHoldingRegisters(ushort start, ushort length, byte slaveId = 1);

        /// <summary>
        /// 读输入寄存器 功能码04
        /// </summary>
        OperateResult<byte[]> ReadInputRegisters(ushort start, ushort length, byte slaveId = 1);

        /// <summary>
        /// 写单个线圈 功能码05
        /// </summary>
        OperateResult WriteSingleCoil(ushort start, bool value, byte slaveId = 1);

        /// <summary>
        /// 写单个寄存器 功能码06
        /// </summary>
        OperateResult WriteSingleRegister(ushort start, ushort value, byte slaveId = 1);

        /// <summary>
        /// 写多个线圈 功能码15
        /// </summary>
        OperateResult WriteMultipleCoils(ushort start, bool[] values, byte slaveId = 1);

        /// <summary>
        /// 批量写寄存器 功能码16
        /// </summary>
        OperateResult WriteMultipleRegisters(ushort start, ushort[] values, byte slaveId = 1);


        // 异步版本
        Task<OperateResult<bool[]>> ReadCoilsAsync(ushort start, ushort length, byte slaveId = 1);
        Task<OperateResult<bool[]>> ReadInputsAsync(ushort start, ushort length, byte slaveId = 1);
        Task<OperateResult<byte[]>> ReadHoldingRegistersAsync(ushort start, ushort length, byte slaveId = 1);
        Task<OperateResult<byte[]>> ReadInputRegistersAsync(ushort start, ushort length, byte slaveId = 1);

        Task<OperateResult> WriteSingleCoilAsync(ushort start, bool value, byte slaveId = 1);
        Task<OperateResult> WriteSingleRegisterAsync(ushort start, ushort value, byte slaveId = 1);
        Task<OperateResult> WriteMultipleCoilsAsync(ushort start, bool[] values, byte slaveId = 1);
        Task<OperateResult> WriteMultipleRegistersAsync(ushort start, ushort[] values, byte slaveId = 1);
    }
}
