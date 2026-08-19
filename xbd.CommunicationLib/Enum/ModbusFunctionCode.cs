using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modbus.CommunicationLibl.Enum
{
    public enum ModbusFunctionCode
    {
        #region 读取功能
        /// <summary>01 读线圈状态</summary>
        ReadCoil = 0x01,

        /// <summary>02 读离散输入状态</summary>
        ReadDiscreteInput = 0x02,

        /// <summary>03 读保持寄存器</summary>
        ReadHoldingRegister = 0x03,

        /// <summary>04 读输入寄存器</summary>
        ReadInputRegister = 0x04,
        #endregion

        #region 写入单个
        /// <summary>05 强制单个线圈</summary>
        WriteSingleCoil = 0x05,

        /// <summary>06 预置单个保持寄存器</summary>
        WriteSingleRegister = 0x06,
        #endregion

        #region 写入多个
        /// <summary>0F 强制多个线圈</summary>
        WriteMultipleCoils = 0x0F,

        /// <summary>10 预置多个保持寄存器</summary>
        WriteMultipleRegisters = 0x10
        #endregion
    }
}
