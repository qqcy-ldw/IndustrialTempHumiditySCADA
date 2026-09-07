using System;
using System.Linq;
using System.Windows.Forms;
using xbd.WarehouseTHDAL;

namespace xbd.WarehouseTHPro
{
    public partial class FrmAlarmRecord : Form
    {
        private readonly AlarmRecordRepository _repository = new();

        public FrmAlarmRecord()
        {
            InitializeComponent();
            _repository.Initialize();
            Load += FrmAlarmRecord_Load;
        }

        private void FrmAlarmRecord_Load(object? sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Now;
            QueryRecords();
        }

        private void btnQuery_Click(object? sender, EventArgs e)
        {
            if (dtpFrom.Value > dtpTo.Value)
            {
                MessageBox.Show("开始时间不能晚于结束时间。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            QueryRecords();
        }

        private void QueryRecords()
        {
            string? zoneName = cboZone.SelectedIndex <= 0 ? null : cboZone.Text;
            bool? isActive = cboStatus.SelectedIndex switch
            {
                1 => true,
                2 => false,
                _ => null
            };

            var records = _repository.Query(dtpFrom.Value, dtpTo.Value, zoneName, isActive).ToList();
            dataGridView1.DataSource = records;
        }

        private static string FormatValue(object? value)
        {
            return value is double number ? number.ToString("F1") : value?.ToString() ?? "";
        }
    }
}
