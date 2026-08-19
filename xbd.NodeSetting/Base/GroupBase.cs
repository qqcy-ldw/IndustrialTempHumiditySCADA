using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.NodeSetting.Base
{
    public class GroupBase
    {
        /// <summary>
        /// 通信组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 通信组状态
        /// </summary>
        public bool IsOK { get; set; } = false;

        /// <summary>
        /// 读取次数
        /// </summary>
        public int ReadTimes { get; set; } = 1;

        /// <summary>
        /// 延时时间
        /// </summary>
        public int DelayTime { get; set; } = 100;
    }
}
