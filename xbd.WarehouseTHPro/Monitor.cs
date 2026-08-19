using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace xbd.WarehouseTHPro
{
    public partial class Monitor : UserControl
    {

        #region 公开属性（对应业务语义字段，直接映射到界面控件）

        private string zoneName = "--";
        [Category("自定义属性")]
        [Description("设置区域名称")]
        public string ZoneName
        {
            get => zoneName;
            set
            {
                zoneName = value;
                pnlZoneCard.TitleText = zoneName;
            }
        }

        [Category("自定义属性")]
        [Description("通信组名称，例如 A区 / B区")]
        public string GroupName { get; set; } = string.Empty;

        /// <summary>温度值（同步刷新数值文本 + 温度计视觉控件）</summary>
        [Category("自定义属性")]
        [Description("当前温度，设置时同时更新文本和温度计视觉控件")]
        public float TempValue
        {
            get => thermometer.Value;
            set
            {
                thermometer.Value = value;
                lblTempValue.Text = value.ToString("F1");
            }
        }

        /// <summary>湿度值（同步刷新数值文本 + 湿度液柱视觉控件）</summary>
        [Category("自定义属性")]
        [Description("当前湿度 0~100，设置时同时更新文本和液柱视觉控件")]
        public int HumidityValue
        {
            get => humidityGauge.Value;
            set
            {
                int clamped = Math.Clamp(value, 0, humidityGauge.MaxValue);
                humidityGauge.Value = clamped;
                lblHumidityValue.Text = clamped.ToString("F1");
            }
        }

        private bool _isAvailable = true;
        /// <summary>设备/传感器是否在线（true=正常，用 OKColor；false=离线/异常，用 NGColor）</summary>
        [Category("自定义属性")]
        [Description("设备是否在线：true=正常(OKColor)，false=离线/异常(NGColor)")]
        [DefaultValue(true)]
        public bool IsAvailable  // 修正原注释拼写 IsAvaliable → IsAvailable
        {
            get => _isAvailable;
            set
            {
                _isAvailable = value;
                ApplyOnlineColor(value ? _okColor : _ngColor);
            }
        }

        private Color _okColor = Color.FromArgb(1, 106, 189);
        /// <summary>正常状态颜色（默认蓝 #016ABD）</summary>
        [Category("自定义属性")]
        [Description("正常/在线时的温度/湿度液柱主题色")]
        [DefaultValue(typeof(Color), "1, 106, 189")]
        public Color OKColor
        {
            get => _okColor;
            set
            {
                _okColor = value;
                if (_isAvailable) ApplyOnlineColor(value);
            }
        }

        private Color _ngColor = Color.Red;
        /// <summary>异常/离线状态颜色（默认红色）</summary>
        [Category("自定义属性")]
        [Description("异常/离线时的温度/湿度液柱主题色及数值前景色")]
        [DefaultValue(typeof(Color), "Red")]
        public Color NGColor
        {
            get => _ngColor;
            set
            {
                _ngColor = value;
                if (!_isAvailable) ApplyOnlineColor(value);
            }
        }

        #endregion

        public Monitor()
        {
            InitializeComponent();
            // 初始化：应用默认 OK 色
            ApplyOnlineColor(_okColor);
            // 默认湿度单位改成 %RH（原 ℃ 是复制时的占位，湿度不该用摄氏度）
            lblHumidityUnit.Text = "%RH";
        }

        /// <summary>根据在线状态，把主题色应用到温度计水银、液柱ValueColor、数值前景</summary>
        private void ApplyOnlineColor(Color themeColor)
        {
            thermometer.MercuryColor = themeColor;
            humidityGauge.ValueColor = themeColor;

            Color valueForeColor = _isAvailable ? Color.White : _ngColor;
            lblTempValue.ForeColor = valueForeColor;
            lblHumidityValue.ForeColor = valueForeColor;

            Color captionForeColor = _isAvailable ? Color.White : Color.LightGray;
            lblTempCaption.ForeColor = captionForeColor;
            lblHumidityCaption.ForeColor = captionForeColor;
            lblTempUnit.ForeColor = captionForeColor;
            lblHumidityUnit.ForeColor = captionForeColor;
        }
    }
}
