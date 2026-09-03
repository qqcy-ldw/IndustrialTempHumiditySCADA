using ScottPlot;
using xbd.WarehouseTHDAL;

namespace xbd.WarehouseTHPro
{
    public partial class FrmHistoryTrend : Form
    {
        private readonly THReadingRepository _repository = new();
        private List<THReading> _currentRows = new();

        /// <summary>六个区域的曲线颜色。同一区域的温度和湿度共用颜色，线型区分变量。</summary>
        private readonly Dictionary<string, Color> _zoneColors = new()
        {
            ["A01"] = Color.FromArgb(255, 159, 67),
            ["A02"] = Color.FromArgb(255, 99, 99),
            ["A03"] = Color.FromArgb(150, 120, 255),
            ["B01"] = Color.FromArgb(240, 100, 180),
            ["B02"] = Color.FromArgb(200, 200, 40),
            ["B03"] = Color.FromArgb(40, 190, 220)
        };

        public FrmHistoryTrend()
        {
            InitializeComponent();
            _repository.Initialize();
            ApplyLegendColors();
        }
        private void formsPlot1_Load(object sender, EventArgs e)
        {
            // ScottPlot 默认使用白色绘图区，这里统一设置为系统深色背景。
            formsPlot1.Plot.Style(
                figureBackground: Color.FromArgb(43, 50, 120),
                dataBackground: Color.FromArgb(43, 50, 120));

            // 坐标轴、刻度文字统一使用白色，并放大刻度字号。
            formsPlot1.Plot.XAxis.Color(Color.White);
            formsPlot1.Plot.YAxis.Color(Color.White);
            formsPlot1.Plot.XAxis.TickLabelStyle(fontSize: 14);
            formsPlot1.Plot.YAxis.TickLabelStyle(fontSize: 14);
            formsPlot1.Plot.Title(
                "历史温湿度趋势",
                color: Color.White,
                fontName: "Microsoft YaHei",
                size: 20);

            dtpTo.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dtpFrom.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
            cboZone.SelectedIndex = 0;
            QueryDataGridView();
            QueryHistory();
        }

        /// <summary>
        /// 查询历史数据并绘制趋势图
        /// </summary>
        private void QueryHistory()
        {
            if (dtpTo.Value < dtpFrom.Value)
            {
                MessageBox.Show("结束时间不能早于开始时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DrawTrendLines();
        }

        private void QueryDataGridView()
        {
            string? zone = cboZone.SelectedIndex <= 0 ? null : cboZone.Text;
            _currentRows = _repository.Query(dtpFrom.Value, dtpTo.Value, zone).ToList();
            dataGridView1.DataSource = _currentRows;
        }

        private void DrawTrendLines()
        {
            formsPlot1.Plot.Clear();
            foreach (var group in _currentRows.GroupBy(r => r.ZoneName.Trim()))
            {
                Color lineColor = GetZoneColor(group.Key);
                var temp = group.Where(r => r.Temperature.HasValue).ToList();
                if (temp.Count > 0)
                {
                    formsPlot1.Plot.AddScatter(
                        temp.Select(r => r.RecordedAt.ToOADate()).ToArray(),
                        temp.Select(r => r.Temperature!.Value).ToArray(),
                        color: lineColor,
                        lineStyle: LineStyle.Dash,
                        markerSize: 4,
                        label: $"{group.Key} 温度");
                }

                var humidity = group.Where(r => r.Humidity.HasValue).ToList();
                if (humidity.Count > 0)
                {
                    formsPlot1.Plot.AddScatter(
                        humidity.Select(r => r.RecordedAt.ToOADate()).ToArray(),
                        humidity.Select(r => r.Humidity!.Value).ToArray(),
                        color: lineColor,
                        lineStyle: LineStyle.Dot,
                        markerSize: 4,
                        label: $"{group.Key} 湿度");
                }
            }

            formsPlot1.Refresh();
        }

        private Color GetZoneColor(string zoneName)
        {
            if (_zoneColors.TryGetValue(zoneName, out Color color))
            {
                return color;
            }

            return Color.White;
        }

        /// <summary>让设计器中的右侧标签与图表曲线使用同一套区域颜色。</summary>
        private void ApplyLegendColors()
        {
            lblLegendTemp.ForeColor = Color.White;
            lblLegendHum.ForeColor = Color.White;

            lblLegendA01Temp.ForeColor = GetZoneColor("A01");
            lblLegendA01Hum.ForeColor = GetZoneColor("A01");
            lblLegendA02Temp.ForeColor = GetZoneColor("A02");
            lblLegendA02Hum.ForeColor = GetZoneColor("A02");
            lblLegendA03Temp.ForeColor = GetZoneColor("A03");
            lblLegendA03Hum.ForeColor = GetZoneColor("A03");
            lblLegendB01Temp.ForeColor = GetZoneColor("B01");
            lblLegendB01Hum.ForeColor = GetZoneColor("B01");
            lblLegendB02Temp.ForeColor = GetZoneColor("B02");
            lblLegendB02Hum.ForeColor = GetZoneColor("B02");
            lblLegendB03Temp.ForeColor = GetZoneColor("B03");
            lblLegendB03Hum.ForeColor = GetZoneColor("B03");
        }

        private void btnQuery_Click(object? sender, EventArgs e)
        {
            if (dtpTo.Value < dtpFrom.Value)
            {
                MessageBox.Show("结束时间不能早于开始时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            QueryDataGridView();
            QueryHistory();
        }
    }
}
