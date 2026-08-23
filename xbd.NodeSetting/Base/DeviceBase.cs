using Microsoft.VisualBasic;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thinger.DataConvertLib;
using xbd.NodeSetting.Common;

namespace xbd.NodeSetting.Base
{
    public delegate void AlarmEventDelegate(object sender, AlarmEventArgs e);

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

        /// <summary>
        /// 第一次连接标识符
        /// </summary>
        public bool FirstConnectSign;
        /// <summary>
        /// 通知事件
        /// </summary>
        public event AlarmEventDelegate AlarmEvent;

        /// <summary>
        /// 变量实时值缓存（线程安全）：Key=变量名(VarName)，Value=最近一次通讯成功算出来的最终值
        /// 用于 UI 层直接取数显示，和通讯层解耦；后台通讯线程写、UI 主线程读都安全
        /// </summary>
        public ConcurrentDictionary<string, object> CurrentValue { get; } = new ConcurrentDictionary<string, object>();


        public void OnAlarmEvent(VariableBase variable, AlarmEventArgs eventArgs)
        {
            AlarmEvent?.Invoke(variable, eventArgs);
        }
        public void UpdateVariable(VariableBase variable)
        {
            UpdateValue(variable);
            UpdateAlarm(variable);
        }

        private void UpdateAlarm(VariableBase variable)
        {
            if (variable.HAlarm || variable.LAlarm)
            {
                float current = 0.0f;
                if (variable.DataType == DataType.Bool)
                {
                    current = (bool)variable.VarValue ? 1.0f : 0.0f;
                }
                else
                {
                    current = Convert.ToSingle(variable.VarValue);
                }

                int reuslt = 0;
                if (variable.HAlarm)
                {
                    // 高报警
                    variable.HCacheValue = current;
                    reuslt = Compare(current, variable.HAlarmValue, variable.HCacheValue, true);
                    if (reuslt != 1)
                    {
                        OnAlarmEvent(variable, new AlarmEventArgs
                        {
                            DeviceName = this.DeviceName,
                            VarName = variable.VarName,
                            CurrentValue = variable.VarValue.ToString(),
                            AlarmValue = variable.HAlarmValue.ToString(),
                            AlarmNote = variable.HAlarmNote.ToString(),
                            IsTriggered = reuslt == 1
                        });
                    }
                }
                if (variable.LAlarm)
                {
                    // 低报警
                    variable.LCacheValue = current;
                    reuslt = Compare(current, variable.LAlarmValue, variable.LCacheValue, true);
                    if (reuslt != 1)
                    {
                        OnAlarmEvent(variable, new AlarmEventArgs
                        {
                            DeviceName = this.DeviceName,
                            VarName = variable.VarName,
                            CurrentValue = variable.VarValue.ToString(),
                            AlarmValue = variable.LAlarmValue.ToString(),
                            AlarmNote = variable.LAlarmNote.ToString(),
                            IsTriggered = reuslt == 1
                        });
                    }
                }
                
            }
        }

        /// <summary>
        /// 比较返回结果
        /// </summary>
        /// <param name="varValue">当前值</param>
        /// <param name="HAlarmValue">设定值</param>
        /// <param name="cache">缓存值</param>
        /// <param name="isPositive">是否是高报警</param>
        /// <returns>1：表示触发、0：表示不变化、-1表示消除</returns>
        private int Compare(float varValue, float hAlarmValue, float cache, bool isPositive)
        {
            if (isPositive)
            {
                if (varValue >= hAlarmValue && cache < hAlarmValue)
                {
                    return 1;
                }
                if (varValue < hAlarmValue && cache >= hAlarmValue)
                {
                    return -1;
                }
                return 0;
            }
            else
            {
                if (varValue <= hAlarmValue && cache > hAlarmValue)
                {
                    return 1;
                }
                if (varValue > hAlarmValue && cache <= hAlarmValue)
                {
                    return -1;
                }
                return 0;
            }

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

        /// <summary>
        /// 索引器：方便字典通过【key】直接获取值 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Object this[string key]
        {
            get
            {
                if (CurrentValue.ContainsKey(key))
                {
                    return CurrentValue[key];
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
