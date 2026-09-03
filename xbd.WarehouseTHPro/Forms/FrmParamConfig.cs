using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using xbd.NodeSetting.Common;
using xbd.NodeSetting.ModbusRTU;
using xbd.WarehouseTHPro.Models;

namespace xbd.WarehouseTHPro
{
    public partial class FrmParamConfig : Form
    {
        private int _tickCount = 0;

        public FrmParamConfig()
        {
            InitializeComponent();
            btnCancel.Click += btnCancel_Click;
            Load += FrmParamConfig_Load;
        }

        private void FrmParamConfig_Load(object? sender, EventArgs e)
        {
            LoadAll();
        }

        /// <summary>
        /// Excel 是唯一配置源，窗体不再把参数保存到 JSON 文件。
        /// </summary>
        private void SaveAll()
        {
            MessageBox.Show(
                "当前参数直接读取 Config 文件夹中的 Excel 配置。\r\n请修改对应 Excel 文件后，重新打开本页面或点击取消重新加载。",
                "提示",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void LoadAll()
        {
            string configPath = Path.Combine(AppContext.BaseDirectory, "Config");
            var result = ModbusRTUCFG.LoadDevice(configPath);
            if (!result.IsSuccess || result.Content == null)
            {
                MessageBox.Show($"Excel 配置加载失败：{result.Message}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ApplyExcelConfig(result.Content);
        }

        /// <summary>
        /// 把 Excel 文件名和 Sheet 变量中的配置加载到参数界面。
        /// 文件名提供串口参数，变量行提供各区域温湿度报警阈值。
        /// </summary>
        private void ApplyExcelConfig(List<ModbusRTUDevice> devices)
        {
            foreach (var device in devices)
            {
                SerialPortParams serial = new SerialPortParams
                {
                    PortName = device.PortName,
                    BaudRate = device.BaudRate,
                    Parity = device.Parity.ToString(),
                    DataBits = device.DataBits,
                    StopBits = device.StopBits.ToString()
                };

                if (device.DeviceName.Contains("A区"))
                {
                    ApplySerialA(serial);
                }
                else if (device.DeviceName.Contains("B区"))
                {
                    ApplySerialB(serial);
                }

                foreach (var group in device.GroupList)
                {
                    foreach (var variable in group.VariableList)
                    {
                        ApplyVariableThreshold(group.GroupName, variable);
                    }
                }
            }
        }

        private void ApplySerialA(SerialPortParams serial)
        {
            txtAPort.Text = serial.PortName;
            SetCombo(cboABaud, serial.BaudRate.ToString(), "9600");
            SetCombo(cboAParity, serial.Parity, "None");
            SetCombo(cboADataBits, serial.DataBits.ToString(), "8");
            SetCombo(cboAStopBits, serial.StopBits, "One");
        }

        private void ApplySerialB(SerialPortParams serial)
        {
            txtBPort.Text = serial.PortName;
            SetCombo(cboBBaud, serial.BaudRate.ToString(), "9600");
            SetCombo(cboBParity, serial.Parity, "None");
            SetCombo(cboBDataBits, serial.DataBits.ToString(), "8");
            SetCombo(cboBStopBits, serial.StopBits, "One");
        }

        private void ApplyVariableThreshold(string zoneName, ModbusRTUVariable variable)
        {
            bool isTemperature = variable.VarName?.Contains("温度") == true;
            bool isHumidity = variable.VarName?.Contains("湿度") == true;
            if (!isTemperature && !isHumidity) return;

            TextBox? highTextBox = FindThresholdTextBox(zoneName, isTemperature, true);
            TextBox? lowTextBox = FindThresholdTextBox(zoneName, isTemperature, false);
            if (highTextBox == null || lowTextBox == null) return;

            highTextBox.Text = variable.HAlarmValue.ToString("F1");
            lowTextBox.Text = variable.LAlarmValue.ToString("F1");
        }

        private TextBox? FindThresholdTextBox(string zoneName, bool isTemperature, bool isHigh)
        {
            string suffix = isTemperature ? "Temp" : "Hum";
            string limit = isHigh ? "High" : "Low";
            string controlName = $"txt{suffix}{zoneName}{limit}";
            return Controls.Find(controlName, true).FirstOrDefault() as TextBox;
        }


        private void btnOK_Click(object sender, EventArgs e) => SaveAll();

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            LoadAll();
            MessageBox.Show("已重置为保存前的配置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _tickCount++;
        }

        /// <summary>
        /// 给 DropDownList 风格的 ComboBox 选中指定文本；找不到就选 fallbackValue
        /// </summary>
        private static void SetCombo(ComboBox cbo, string? wantText, string fallbackValue)
        {
            string target = string.IsNullOrWhiteSpace(wantText) ? fallbackValue : wantText.Trim();
            // 优先按 Items 精确匹配（DropDownList 必须在 Items 里才会显示）
            for (int i = 0; i < cbo.Items.Count; i++)
            {
                if (string.Equals(cbo.Items[i]?.ToString(), target, StringComparison.OrdinalIgnoreCase))
                {
                    cbo.SelectedIndex = i;
                    return;
                }
            }
            // 找不到匹配项：选中 fallbackValue
            for (int i = 0; i < cbo.Items.Count; i++)
            {
                if (string.Equals(cbo.Items[i]?.ToString(), fallbackValue, StringComparison.OrdinalIgnoreCase))
                {
                    cbo.SelectedIndex = i;
                    return;
                }
            }
            // 兜底：至少选第一项，别空着
            if (cbo.Items.Count > 0 && cbo.SelectedIndex < 0) cbo.SelectedIndex = 0;
        }
    }
}
