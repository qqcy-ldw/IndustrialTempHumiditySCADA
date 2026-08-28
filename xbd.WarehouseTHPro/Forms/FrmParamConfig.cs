using System;
using System.Windows.Forms;
using xbd.WarehouseTHPro.Models;
using xbd.WarehouseTHUtils;

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

        private SerialConfigBundle CollectSerial()
        {
            return new SerialConfigBundle
            {
                SerialA = new SerialPortParams
                {
                    PortName = txtAPort.Text.Trim(),
                    BaudRate = ParseIntCombo(cboABaud, 9600),
                    Parity = GetComboText(cboAParity, "None"),
                    DataBits = ParseIntCombo(cboADataBits, 8),
                    StopBits = GetComboText(cboAStopBits, "One")
                },
                SerialB = new SerialPortParams
                {
                    PortName = txtBPort.Text.Trim(),
                    BaudRate = ParseIntCombo(cboBBaud, 9600),
                    Parity = GetComboText(cboBParity, "None"),
                    DataBits = ParseIntCombo(cboBDataBits, 8),
                    StopBits = GetComboText(cboBStopBits, "One")
                }
            };
        }

        private ThresholdConfigBundle CollectThreshold()
        {
            return new ThresholdConfigBundle
            {
                ZoneA01 = BuildZone("A01", txtTempA01High, txtTempA01Low, txtHumA01High, txtHumA01Low),
                ZoneA02 = BuildZone("A02", txtTempA02High, txtTempA02Low, txtHumA02High, txtHumA02Low),
                ZoneA03 = BuildZone("A03", txtTempA03High, txtTempA03Low, txtHumA03High, txtHumA03Low),
                ZoneB01 = BuildZone("B01", txtTempB01High, txtTempB01Low, txtHumB01High, txtHumB01Low),
                ZoneB02 = BuildZone("B02", txtTempB02High, txtTempB02Low, txtHumB02High, txtHumB02Low),
                ZoneB03 = BuildZone("B03", txtTempB03High, txtTempB03Low, txtHumB03High, txtHumB03Low)
            };
        }

        private ZoneThreshold BuildZone(string zone, TextBox tHigh, TextBox tLow, TextBox hHigh, TextBox hLow)
        {
            return new ZoneThreshold
            {
                ZoneName = zone,
                TempHigh = ParseDouble(tHigh.Text, 30.0),
                TempLow = ParseDouble(tLow.Text, 10.0),
                HumHigh = ParseDouble(hHigh.Text, 80.0),
                HumLow = ParseDouble(hLow.Text, 30.0)
            };
        }

        /// <summary>
        /// 对象 → UI（把配置对象填回真实控件）
        /// 注意：ComboBox 都是 DropDownList，必须找匹配的 Index 或 Text 赋值
        /// </summary>
        /// <param name="cfg"></param>
        private void ApplySerial(SerialConfigBundle cfg)
        {
            if (cfg.SerialA != null)
            {
                txtAPort.Text = cfg.SerialA.PortName;
                SetCombo(cboABaud, cfg.SerialA.BaudRate.ToString(), "9600");
                SetCombo(cboAParity, cfg.SerialA.Parity, "None");
                SetCombo(cboADataBits, cfg.SerialA.DataBits.ToString(), "8");
                SetCombo(cboAStopBits, cfg.SerialA.StopBits, "One");
            }
            if (cfg.SerialB != null)
            {
                txtBPort.Text = cfg.SerialB.PortName;
                SetCombo(cboBBaud, cfg.SerialB.BaudRate.ToString(), "9600");
                SetCombo(cboBParity, cfg.SerialB.Parity, "None");
                SetCombo(cboBDataBits, cfg.SerialB.DataBits.ToString(), "8");
                SetCombo(cboBStopBits, cfg.SerialB.StopBits, "One");
            }
        }

        private void ApplyThreshold(ThresholdConfigBundle cfg)
        {
            ApplyZone(cfg.ZoneA01, txtTempA01High, txtTempA01Low, txtHumA01High, txtHumA01Low);
            ApplyZone(cfg.ZoneA02, txtTempA02High, txtTempA02Low, txtHumA02High, txtHumA02Low);
            ApplyZone(cfg.ZoneA03, txtTempA03High, txtTempA03Low, txtHumA03High, txtHumA03Low);
            ApplyZone(cfg.ZoneB01, txtTempB01High, txtTempB01Low, txtHumB01High, txtHumB01Low);
            ApplyZone(cfg.ZoneB02, txtTempB02High, txtTempB02Low, txtHumB02High, txtHumB02Low);
            ApplyZone(cfg.ZoneB03, txtTempB03High, txtTempB03Low, txtHumB03High, txtHumB03Low);
        }

        private void ApplyZone(ZoneThreshold? z, TextBox tHigh, TextBox tLow, TextBox hHigh, TextBox hLow)
        {
            if (z == null) return;
            tHigh.Text = z.TempHigh.ToString("F1");
            tLow.Text = z.TempLow.ToString("F1");
            hHigh.Text = z.HumHigh.ToString("F1");
            hLow.Text = z.HumLow.ToString("F1");
        }

        /// <summary>
        /// 文件保存
        /// </summary>
        private void SaveAll()
        {
            var serialCfg = CollectSerial();
            var s1 = ConfigHelper.Save(serialCfg, ConfigHelper.PlcCommConfigPath);
            if (!s1.IsSuccess)
            {
                MessageBox.Show($"串口配置保存失败：{s1.Message}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var thCfg = CollectThreshold();
            var s2 = ConfigHelper.Save(thCfg, ConfigHelper.SystemConfigPath);
            if (!s2.IsSuccess)
            {
                MessageBox.Show($"阈值配置保存失败：{s2.Message}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("参数配置保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadAll()
        {
            var s = ConfigHelper.Load<SerialConfigBundle>(ConfigHelper.PlcCommConfigPath);
            if (s.IsSuccess && s.Content != null) ApplySerial(s.Content);
            // 即使文件不存在，也保证下拉框有默认选中（默认 Index）
            else ApplySerial(new SerialConfigBundle());

            var t = ConfigHelper.Load<ThresholdConfigBundle>(ConfigHelper.SystemConfigPath);
            if (t.IsSuccess && t.Content != null) ApplyThreshold(t.Content);
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

        private static string GetComboText(ComboBox cbo, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(cbo.Text) ? defaultValue : cbo.Text.Trim();
        }

        private static int ParseIntCombo(ComboBox cbo, int defaultValue)
        {
            if (int.TryParse(cbo.Text?.Trim(), out var v)) return v;
            return defaultValue;
        }

        private static double ParseDouble(string? text, double defaultValue)
        {
            if (double.TryParse(text?.Trim(), out var v)) return v;
            return defaultValue;
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
