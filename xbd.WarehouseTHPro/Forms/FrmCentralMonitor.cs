using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using thinger.DataConvertLib;
using xbd.NodeSetting.Common;
using xbd.NodeSetting.ModbusRTU;
using xbd.WarehouseTHDAL;

namespace xbd.WarehouseTHPro
{
    public partial class FrmCentralMonitor : Form
    {
        public List<ModbusRTUDevice> _devices { get; private set; } = new();
        private bool _loadedOk = false;
        private readonly THReadingRepository _readingRepository = new();

        /// <summary>上一次保存数据的时间（秒）。</summary>
        private int _lastSavedSecond = -1;

        /// <summary>
        /// 当前最新的一批温湿度数据。
        /// 每次刷新监控界面时都会更新，一秒只保存一次到数据库。
        /// </summary>
        private List<THReading> _latestReadings = new();

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
            _readingRepository.Initialize();
            // 窗体关闭时释放所有设备的串口资源，避免串口被占用下次打开失败
            FormClosing += (o, e) => StopAllDevices();
        }
        private void monitor1_Load(object sender, EventArgs e)
        {
            // 配置文件随程序部署到输出目录的 Config 子目录，避免依赖开发机绝对路径
            string configPath = Path.Combine(AppContext.BaseDirectory, "Config");

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

                var readings = new List<THReading>();
                DateTime recordedAt = DateTime.Now;

                foreach (var device in _devices)
                {
                    // 遍历所有 Monitor 分区控件，按 GroupName 和设备里的 Group 做匹配
                    foreach (var monitor in Controls.OfType<Monitor>())
                    {
                        var group = device.GroupList
                            .FirstOrDefault(c => c.GroupName == monitor.GroupName);
                        // 这个分区在当前设备里没配置就跳过
                        if (group == null) continue; 

                        // 更新设备状态
                        // 设备断线时即使组状态还没来得及刷新，也必须立即显示为异常。
                        monitor.IsAvailable = device.IsConnected && group.IsOK;

                        string tempKey = $"{monitor.GroupName}{KEY_SPLIT}{KEY_TEMP_SUFFIX}";
                        string humidityKey = $"{monitor.GroupName}{KEY_SPLIT}{KEY_HUMIDITY_SUFFIX}";

                        // 先给默认值（没读到时显示 --）
                        float tempVal = DEFAULT_TEMP;
                        int humidityVal = DEFAULT_HUMIDITY;

                        object? temperature = null;
                        object? humidity = null;

                        if (monitor.IsAvailable && device.CurrentValue.TryGetValue(tempKey, out var tempObj))
                        {
                            temperature = tempObj;
                            tempVal = Convert.ToSingle(tempObj);
                        }

                        if (monitor.IsAvailable && device.CurrentValue.TryGetValue(humidityKey, out var humObj))
                        {
                            humidity = humObj;
                            humidityVal = Convert.ToInt32(humObj);
                        }

                        monitor.TempValue = tempVal;
                        monitor.HumidityValue = humidityVal;

                        readings.Add(new THReading
                        {
                            RecordedAt = recordedAt,
                            DeviceName = device.DeviceName,
                            ZoneName = monitor.GroupName,
                            Temperature = temperature == null ? null : Convert.ToDouble(temperature),
                            Humidity = humidity == null ? null : Convert.ToDouble(humidity),
                            IsAvailable = monitor.IsAvailable
                        });
                    }
                }

                // 保存本次刷新得到的最新温湿度数据。
                _latestReadings = readings;

                // 获取当前秒数，判断是否已经保存过本秒的数据，避免重复保存。
                int currentSecond = recordedAt.Second;
                bool hasSavedThisSecond = currentSecond == _lastSavedSecond;

                if (!hasSavedThisSecond && _latestReadings.Count > 0)
                {
                    _lastSavedSecond = currentSecond;

                    // 当前项目每秒最多保存 6 条数据，直接批量插入即可。
                    _readingRepository.InsertMany(_latestReadings);
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
