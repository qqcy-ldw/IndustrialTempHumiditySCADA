using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thinger.DataConvertLib;

namespace xbd.NodeSetting.Base
{
    public class DeviceBase
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 大小端
        /// </summary>
        public DataFormat DataFormat { get; set; } = DataFormat.ABCD;

        /// <summary>
        /// 连接状态
        /// </summary>
        public bool IsConnected { get; set; } = false;

        /// <summary>
        /// 重连时间
        /// </summary>
        public int ReConnectTime { get; set; } = 2000;

        /// <summary>
        /// 取消线程标识位
        /// </summary>
        public CancellationTokenSource cts;
    }
}
