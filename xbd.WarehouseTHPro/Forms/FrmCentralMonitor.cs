using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using thinger.DataConvertLib;
using xbd.NodeSetting.Common;
using xbd.NodeSetting.ModbusRTU;

namespace xbd.WarehouseTHPro
{
    public partial class FrmCentralMonitor : Form
    {
        public List<ModbusRTUDevice> _devices { get; private set; } = new();
        private bool _loadedOk = false;

        // 【可按需修改】CurrentValue 的 Key 拼接规则（必须和 Excel 里 VarName 列写法完全一致）
        // 比如 Excel 里写的是「A区_温度」就把下面的 "" 改成 "_"，写的是「A 区 温度」就改空格
        private const string KEY_SPLIT = "";
        private const string KEY_TEMP_SUFFIX = "温度";
        private const string KEY_HUMIDITY_SUFFIX = "湿度";
        private const float DEFAULT_TEMP = float.NaN; // 没读到时显示默认值（Monitor 控件会显示 --）
        private const int DEFAULT_HUMIDITY = -1;      // 没读到时显示默认值

        public FrmCentralMonitor()
        {
            InitializeComponent();
            // 窗体关闭时释放所有设备的串口资源，避免串口被占用下次打开失败
            FormClosing += (o, e) => StopAllDevices();
        }
        private void monitor1_Load(object sender, EventArgs e)
        {
            string configPath = Path.Combine(
                @"D:\.netStudy\winforms\温湿度监控系统\xbd.WarehouseTHPro\xbd.WarehouseTHPro",
                "Config");

            var result = ModbusRTUCFG.LoadDevice(configPath);
            if (!result.IsSuccess)
            {
                MessageBox.Show($"配置加载失败：{result.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _loadedOk = false;
                return;
            }

            _devices = result.Content ?? new List<ModbusRTUDevice>();
            if (_devices.Count == 0)
            {
                MessageBox.Show("配置加载成功，但未找到任何设备！", "警告",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _loadedOk = false;
                return;
            }

            foreach (var device in _devices)
            {
                device.Start();
            }
            _loadedOk = true;
        }

        public void UpdateMonitor()
        {
            try
            {
                // 前置判断：没加载成功/没有设备就直接返回，不做任何无意义遍历
                if (!_loadedOk || _devices.Count == 0) return;

                foreach (var device in _devices)
                {
                    // 遍历所有 Monitor 分区控件，按 GroupName 和设备里的 Group 做匹配
                    foreach (var monitor in Controls.OfType<Monitor>())
                    {
                        var group = device.GroupList
                            .FirstOrDefault(c => c.GroupName == monitor.GroupName);
                        // 这个分区在当前设备里没配置就跳过
                        if (group == null) continue; 

                        // ① 先更新通讯状态（绿/红图标）
                        monitor.IsAvailable = group.IsOK;

                        // ② 通讯正常时，才从 device.CurrentValue 读温度湿度
                        //    ⚠️ 注意：读的是「device.CurrentValue」（就是上面启动的那个设备对象，
                        //       它的后台线程一直在写这个字典），不是另外 new 的空对象！
                        if (!monitor.IsAvailable) continue;

                        string tempKey = $"{monitor.GroupName}{KEY_SPLIT}{KEY_TEMP_SUFFIX}";
                        string humidityKey = $"{monitor.GroupName}{KEY_SPLIT}{KEY_HUMIDITY_SUFFIX}";

                        // 先给默认值（没读到时显示 --）
                        float tempVal = DEFAULT_TEMP;
                        int humidityVal = DEFAULT_HUMIDITY;

                        // ✅ 用 TryGetValue + Convert.ToXXX：
                        // 1. Key 不存在不抛异常（设备刚启动还没读完第一轮、组这次读失败了都正常）
                        // 2. 不做强转 float/int，避免 CurrentValue 里存的是 double/ushort/int 任意类型时类型不匹配
                        if (device.CurrentValue.TryGetValue(tempKey, out var tempObj))
                            tempVal = Convert.ToSingle(tempObj);
                        if (device.CurrentValue.TryGetValue(humidityKey, out var humObj))
                            humidityVal = Convert.ToInt32(humObj);

                        monitor.TempValue = tempVal;
                        monitor.HumidityValue = humidityVal;
                    }
                }
            }
            catch (Exception ex)
            {
                // 调试期可以在这里打日志，线上不弹框（弹框会卡死刷新线程）
#if DEBUG
                System.Diagnostics.Debug.WriteLine($"刷新监控界面异常：{ex}");
#endif
            }
        }

        // =========================================================
        // 窗体关闭时停止所有设备（释放串口资源，下次打开不报错「串口已占用」）
        // =========================================================
        private void StopAllDevices()
        {
            foreach (var device in _devices)
            {
                try { device.Stop(); } catch { /* 停止异常忽略，保证关窗不卡 */ }
            }
        }
    }
}
