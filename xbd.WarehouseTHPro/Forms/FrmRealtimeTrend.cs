using ScottPlot;
using System;
using System.Drawing;
using xbd.NodeSetting.ModbusRTU;

namespace xbd.WarehouseTHPro
{
    public partial class FrmRealtimeTrend : Form
    {
        private readonly List<SeriesInfo> _series = new();
        // 所有曲线共享的时间轴（点序号）
        private readonly List<double> _xAxis = new();   
        // 最多保留 600 个点（1秒1点 = 10分钟窗口）
        private const int MAX_POINTS = 600;            

        private int _lastSampleTick = 0;                // 上次取点的时刻（节流用）

        public FrmRealtimeTrend()
        {
            InitializeComponent();

            #region 折线图样式初始化
            Color bg = Color.FromArgb(43, 50, 120);
            Color white = Color.White;
            //设置背景色

            formsPlot1.Plot.Style(figureBackground: bg, dataBackground: bg);

            // X/Y 轴刻度样式
            formsPlot1.Plot.XAxis.Color(white);
            formsPlot1.Plot.YAxis.Color(white);
            formsPlot1.Plot.XAxis.TickLabelStyle(fontSize: 14);
            formsPlot1.Plot.YAxis.TickLabelStyle(fontSize: 16);

            // 轴标题
            formsPlot1.Plot.Title("实时趋势", color: white, fontName: "Microsoft YaHei", size: 24);
            formsPlot1.Plot.YAxis.Label("温度℃", color: white, fontName: "Microsoft YaHei", size: 18);

            // X 轴显示真实采集时间（ScottPlot 用 OADate 表示时间，再按格式显示）
            formsPlot1.Plot.XAxis.DateTimeFormat(true);
            formsPlot1.Plot.XAxis.TickLabelFormat("HH:mm:ss", true);

            // ScottPlot 4 必须手动 Render() 一次，否则首次显示是默认白色，点击才刷新
            formsPlot1.Render();
            #endregion

            // 初始化 12 条曲线：温度暖色系，湿度冷色系
            AddSeries(chkA01Temp, "A01温度", Color.FromArgb(255, 99, 99));
            AddSeries(chkA02Temp, "A02温度", Color.FromArgb(255, 159, 67));
            AddSeries(chkA03Temp, "A03温度", Color.FromArgb(255, 205, 86));
            AddSeries(chkB01Temp, "B01温度", Color.FromArgb(240, 100, 180));
            AddSeries(chkB02Temp, "B02温度", Color.FromArgb(200, 80, 255));
            AddSeries(chkB03Temp, "B03温度", Color.FromArgb(150, 120, 255));
            AddSeries(chkA01Hum, "A01湿度", Color.FromArgb(54, 162, 235));
            AddSeries(chkA02Hum, "A02湿度", Color.FromArgb(75, 192, 192));
            AddSeries(chkA03Hum, "A03湿度", Color.FromArgb(0, 200, 150));
            AddSeries(chkB01Hum, "B01湿度", Color.FromArgb(100, 220, 255));
            AddSeries(chkB02Hum, "B02湿度", Color.FromArgb(60, 180, 220));
            AddSeries(chkB03Hum, "B03湿度", Color.FromArgb(40, 140, 200));
        }

        /// <summary>注册一条曲线：颜色同时赋给复选框文字，勾选变化立刻重绘</summary>
        private void AddSeries(CheckBox chk, string key, Color color)
        {
            chk.ForeColor = color;                     // 复选框文字颜色 = 折线颜色
            chk.CheckedChanged += Chk_CheckedChanged;  // 勾选变化立刻重绘
            _series.Add(new SeriesInfo { Chk = chk, Key = key, Color = color });
        }

        /// <summary>主窗体定时器直接调用：从设备列表取数，给每条勾选的曲线追加一个点</summary>
        public void AppendPoint(List<ModbusRTUDevice> devices)
        {
            if (devices.Count == 0) return;

            // 节流：距上次取点不足 1 秒就跳过（保证 1 秒 1 点）
            if (Environment.TickCount - _lastSampleTick < 1000) return;
            _lastSampleTick = Environment.TickCount;

            // ① 时间轴追加当前采集时刻（所有曲线共享；ToOADate = ScottPlot 的时间格式）
            _xAxis.Add(DateTime.Now.ToOADate());
            if (_xAxis.Count > MAX_POINTS) _xAxis.RemoveAt(0);

            // ② 每条曲线追加一个点：勾选且读到 → 真实值；否则 NaN（NaN = 断点，不画线）
            foreach (var s in _series)
            {
                double y = double.NaN;
                if (s.Chk.Checked)
                {
                    double? v = TryGetValue(devices, s.Key);
                    if (v != null) y = v.Value;
                }
                s.Ys.Add(y);
                if (s.Ys.Count > MAX_POINTS) s.Ys.RemoveAt(0);
            }

            // ③ 刷新右侧数值 Label（pnlRight 里只有 lblVal 的 Tag 非空，所以直接遍历）
            foreach (Control c in pnlRight.Controls)
            {
                if (c is Label lbl && lbl.Tag is string key && !string.IsNullOrEmpty(key))
                {
                    double? v = TryGetValue(devices, key);
                    lbl.Text = v != null ? v.Value.ToString("F1") : "--";
                }
            }

            // ④ 重绘折线
            RedrawChart();
        }

        /// <summary>从所有设备里按 Key 找值（和集中监控页的 Key 规则一致）</summary>
        private static double? TryGetValue(List<ModbusRTUDevice> devices, string key)
        {
            foreach (var dev in devices)
            {
                if (dev.CurrentValue.TryGetValue(key, out var obj) && obj != null)
                    return Convert.ToDouble(obj);
            }
            return null;
        }

        /// <summary>重绘：只画"勾选"的曲线</summary>
        private void RedrawChart()
        {
            formsPlot1.Plot.Clear();
            foreach (var s in _series)
            {
                if (!s.Chk.Checked || s.Ys.Count == 0) continue;   // 没勾选 → 不画

                // 过滤掉 NaN 点：ScottPlot 4.1 的散点图不允许 NaN（会自动缩放坐标轴时抛异常）
                // 缺失的点不画，前后点直接连线
                List<double> xs = new();
                List<double> ys = new();
                for (int i = 0; i < s.Ys.Count; i++)
                {
                    if (double.IsNaN(s.Ys[i])) continue;
                    xs.Add(_xAxis[i]);
                    ys.Add(s.Ys[i]);
                }
                if (ys.Count == 0) continue;   // 全是 NaN → 这条线暂时不画

                formsPlot1.Plot.AddScatter(
                    xs.ToArray(), ys.ToArray(),
                    color: s.Color, label: s.Chk.Text);
            }
            formsPlot1.Refresh();
        }

        /// <summary>勾选/取消勾选 → 立刻重绘（不用等下一轮数据）</summary>
        private void Chk_CheckedChanged(object? sender, EventArgs e)
        {
            RedrawChart();
        }

        /// <summary>已改为事件驱动取数（见 OnDataRefreshed），定时器保留备用</summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        /// <summary>一条曲线：复选框 + 数据键 + 颜色 + 数据缓存</summary>
        private class SeriesInfo
        {
            public CheckBox Chk;           // 对应的复选框
            public string Key;             // CurrentValue 里的键（如 "A01温度"）
            public Color Color;            // 折线颜色（= 复选框文字颜色）
            public List<double> Ys = new(); // 该曲线的数据
        }
    }
}
