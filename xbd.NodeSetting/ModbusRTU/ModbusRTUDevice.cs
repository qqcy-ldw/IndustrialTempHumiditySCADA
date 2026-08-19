using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.NodeSetting.Base;

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

        /// <summary>
        /// 开启一个串口读取
        /// </summary>
        public void Start()
        {
            cts = new CancellationTokenSource();
            Task.Run(() => {
                if (IsConnected)
                {

                }
                else
                {
                    // 重连
                }
            },cts.Token);
        }

        /// <summary>
        /// 关不一个串口读取
        /// </summary>
        public void Stop()
        {

        }
    }
}
