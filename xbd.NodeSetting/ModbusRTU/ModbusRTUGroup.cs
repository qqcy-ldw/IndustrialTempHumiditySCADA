using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.NodeSetting.Base;
using xbd.NodeSetting.Enums;

namespace xbd.NodeSetting.ModbusRTU
{
    public class ModbusRTUGroup : GroupBase
    {
        /// <summary>
        /// 存储区
        /// </summary>
        public ModbusStore StoreArea { get; set; }

        /// <summary>
        /// 通信组ID
        /// </summary>
        public byte GroupId { get; set; }

        /// <summary>
        /// 开始地址
        /// </summary>
        public ushort Start { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public ushort Length { get; set; }

        /// <summary>
        /// 属性集合
        /// </summary>
        public List<ModbusRTUVariable> VariableList { get; set; } = new List<ModbusRTUVariable>();
    }
}
