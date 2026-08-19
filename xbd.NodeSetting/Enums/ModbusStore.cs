using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.NodeSetting.Enums
{
    public enum ModbusStore
    {
        /// <summary>
        /// 线圈 00001 ~ 09999
        /// 读写 单个Bit 布尔值
        /// 对应功能码：01读线圈 / 05写单线圈 / 15写多线圈
        /// </summary>
        Coil = 1,

        /// <summary>
        /// 离散输入 10001 ~ 19999
        /// 只读 单个Bit
        /// 对应功能码：02读离散输入
        /// </summary>
        DiscreteInput = 2,

        /// <summary>
        /// 输入寄存器 30001 ~ 39999
        /// 只读 16位寄存器(ushort)
        /// 对应功能码：04读输入寄存器
        /// </summary>
        InputRegister = 3,

        /// <summary>
        /// 保持寄存器 40001 ~ 49999
        /// 读写 16位寄存器(ushort) 最常用
        /// 对应功能码：03读保持寄存器 / 06写单寄存器 / 16写多寄存器
        /// </summary>
        HoldingRegister = 4
    }
}
