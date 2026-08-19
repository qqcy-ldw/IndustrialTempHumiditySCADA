namespace xbd.WarehouseTHPro
{
    partial class Monitor
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlZoneCard = new xbd.ControlLib.xbdHeadPanel();
            this.humidityGauge = new xbd.ControlLib.xbdWave();
            this.lblHumidityUnit = new System.Windows.Forms.Label();
            this.lblTempUnit = new System.Windows.Forms.Label();
            this.lblHumidityValue = new System.Windows.Forms.Label();
            this.lblTempValue = new System.Windows.Forms.Label();
            this.lblHumidityCaption = new System.Windows.Forms.Label();
            this.lblTempCaption = new System.Windows.Forms.Label();
            this.thermometer = new xbd.ControlLib.xbdThermometer();
            this.pnlZoneCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlZoneCard
            // 
            this.pnlZoneCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.pnlZoneCard.BorderColor = System.Drawing.Color.White;
            this.pnlZoneCard.Controls.Add(this.humidityGauge);
            this.pnlZoneCard.Controls.Add(this.lblHumidityUnit);
            this.pnlZoneCard.Controls.Add(this.lblTempUnit);
            this.pnlZoneCard.Controls.Add(this.lblHumidityValue);
            this.pnlZoneCard.Controls.Add(this.lblTempValue);
            this.pnlZoneCard.Controls.Add(this.lblHumidityCaption);
            this.pnlZoneCard.Controls.Add(this.lblTempCaption);
            this.pnlZoneCard.Controls.Add(this.thermometer);
            this.pnlZoneCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlZoneCard.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlZoneCard.HeadHeight = 35;
            this.pnlZoneCard.LinearGradientRate = 0.4F;
            this.pnlZoneCard.Location = new System.Drawing.Point(0, 0);
            this.pnlZoneCard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlZoneCard.Name = "pnlZoneCard";
            this.pnlZoneCard.Size = new System.Drawing.Size(446, 298);
            this.pnlZoneCard.TabIndex = 0;
            this.pnlZoneCard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pnlZoneCard.ThemeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(106)))), ((int)(((byte)(189)))));
            this.pnlZoneCard.ThemeForeColor = System.Drawing.Color.White;
            this.pnlZoneCard.TitleText = "仓库分区：A区-10";
            // 
            // humidityGauge
            // 
            this.humidityGauge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(231)))), ((int)(((byte)(237)))));
            this.humidityGauge.ConerRadius = 10;
            this.humidityGauge.FillColor = System.Drawing.Color.Transparent;
            this.humidityGauge.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.humidityGauge.ForeColor = System.Drawing.Color.Black;
            this.humidityGauge.IsRadius = true;
            this.humidityGauge.IsRectangle = true;
            this.humidityGauge.IsShowRect = false;
            this.humidityGauge.Location = new System.Drawing.Point(318, 71);
            this.humidityGauge.MaxValue = 100;
            this.humidityGauge.Name = "humidityGauge";
            this.humidityGauge.RectColor = System.Drawing.Color.White;
            this.humidityGauge.RectWidth = 4;
            this.humidityGauge.Size = new System.Drawing.Size(90, 198);
            this.humidityGauge.TabIndex = 3;
            this.humidityGauge.Value = 0;
            this.humidityGauge.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(106)))), ((int)(((byte)(189)))));
            // 
            // lblHumidityUnit
            // 
            this.lblHumidityUnit.AutoSize = true;
            this.lblHumidityUnit.ForeColor = System.Drawing.Color.White;
            this.lblHumidityUnit.Location = new System.Drawing.Point(252, 227);
            this.lblHumidityUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHumidityUnit.Name = "lblHumidityUnit";
            this.lblHumidityUnit.Size = new System.Drawing.Size(36, 28);
            this.lblHumidityUnit.TabIndex = 1;
            this.lblHumidityUnit.Text = "℃";
            // 
            // lblTempUnit
            // 
            this.lblTempUnit.AutoSize = true;
            this.lblTempUnit.ForeColor = System.Drawing.Color.White;
            this.lblTempUnit.Location = new System.Drawing.Point(253, 119);
            this.lblTempUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTempUnit.Name = "lblTempUnit";
            this.lblTempUnit.Size = new System.Drawing.Size(36, 28);
            this.lblTempUnit.TabIndex = 1;
            this.lblTempUnit.Text = "℃";
            // 
            // lblHumidityValue
            // 
            this.lblHumidityValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHumidityValue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblHumidityValue.ForeColor = System.Drawing.Color.White;
            this.lblHumidityValue.Location = new System.Drawing.Point(162, 225);
            this.lblHumidityValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHumidityValue.Name = "lblHumidityValue";
            this.lblHumidityValue.Size = new System.Drawing.Size(82, 28);
            this.lblHumidityValue.TabIndex = 1;
            this.lblHumidityValue.Text = "0.0";
            this.lblHumidityValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTempValue
            // 
            this.lblTempValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTempValue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblTempValue.ForeColor = System.Drawing.Color.White;
            this.lblTempValue.Location = new System.Drawing.Point(163, 116);
            this.lblTempValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTempValue.Name = "lblTempValue";
            this.lblTempValue.Size = new System.Drawing.Size(82, 28);
            this.lblTempValue.TabIndex = 1;
            this.lblTempValue.Text = "0.0";
            this.lblTempValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHumidityCaption
            // 
            this.lblHumidityCaption.AutoSize = true;
            this.lblHumidityCaption.ForeColor = System.Drawing.Color.White;
            this.lblHumidityCaption.Location = new System.Drawing.Point(162, 181);
            this.lblHumidityCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHumidityCaption.Name = "lblHumidityCaption";
            this.lblHumidityCaption.Size = new System.Drawing.Size(82, 28);
            this.lblHumidityCaption.TabIndex = 1;
            this.lblHumidityCaption.Text = "湿度值";
            // 
            // lblTempCaption
            // 
            this.lblTempCaption.AutoSize = true;
            this.lblTempCaption.ForeColor = System.Drawing.Color.White;
            this.lblTempCaption.Location = new System.Drawing.Point(163, 72);
            this.lblTempCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTempCaption.Name = "lblTempCaption";
            this.lblTempCaption.Size = new System.Drawing.Size(82, 28);
            this.lblTempCaption.TabIndex = 1;
            this.lblTempCaption.Text = "温度值";
            // 
            // thermometer
            // 
            this.thermometer.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.thermometer.ForeColor = System.Drawing.Color.White;
            this.thermometer.GlassTubeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.thermometer.IsUnitVisiable = false;
            this.thermometer.LeftTemperatureUnit = xbd.ControlLib.TemperatureUnit.C;
            this.thermometer.Location = new System.Drawing.Point(30, 52);
            this.thermometer.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.thermometer.MaxValue = 100F;
            this.thermometer.MercuryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(106)))), ((int)(((byte)(189)))));
            this.thermometer.MinValue = 0F;
            this.thermometer.Name = "thermometer";
            this.thermometer.RightTemperatureUnit = xbd.ControlLib.TemperatureUnit.C;
            this.thermometer.Size = new System.Drawing.Size(111, 216);
            this.thermometer.SplitCount = 1;
            this.thermometer.TabIndex = 0;
            this.thermometer.Value = 10F;
            // 
            // Monitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlZoneCard);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Monitor";
            this.Size = new System.Drawing.Size(446, 298);
            this.pnlZoneCard.ResumeLayout(false);
            this.pnlZoneCard.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private xbd.ControlLib.xbdHeadPanel pnlZoneCard;
        private System.Windows.Forms.Label lblTempCaption;
        private xbd.ControlLib.xbdThermometer thermometer;
        private xbd.ControlLib.xbdWave humidityGauge;
        private System.Windows.Forms.Label lblHumidityUnit;
        private System.Windows.Forms.Label lblTempUnit;
        private System.Windows.Forms.Label lblHumidityValue;
        private System.Windows.Forms.Label lblTempValue;
        private System.Windows.Forms.Label lblHumidityCaption;
    }
}
