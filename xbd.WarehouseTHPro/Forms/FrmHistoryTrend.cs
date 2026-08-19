using System;

namespace xbd.WarehouseTHPro
{
    public partial class FrmHistoryTrend : Form
    {
        private int _tickCount = 0;

        public FrmHistoryTrend()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _tickCount++;
            lblCount.Text = $"已运行: {_tickCount} 秒";
        }
    }
}
