namespace xbd.WarehouseTHPro
{
    partial class FrmCentralMonitor
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            monitor1 = new Monitor();
            monitor2 = new Monitor();
            monitor3 = new Monitor();
            monitor4 = new Monitor();
            monitor5 = new Monitor();
            monitor6 = new Monitor();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            // 
            // monitor1
            // 
            monitor1.GroupName = "";
            monitor1.HumidityValue = 0;
            monitor1.Location = new Point(15, 13);
            monitor1.Margin = new Padding(4);
            monitor1.Name = "monitor1";
            monitor1.Size = new Size(435, 292);
            monitor1.TabIndex = 0;
            monitor1.TempValue = 10F;
            monitor1.ZoneName = "仓库分区：A区-01";
            // 
            // monitor2
            // 
            monitor2.GroupName = "";
            monitor2.HumidityValue = 0;
            monitor2.Location = new Point(483, 13);
            monitor2.Margin = new Padding(4);
            monitor2.Name = "monitor2";
            monitor2.Size = new Size(435, 292);
            monitor2.TabIndex = 0;
            monitor2.TempValue = 10F;
            monitor2.ZoneName = "仓库分区：A区-02";
            // 
            // monitor3
            // 
            monitor3.GroupName = "";
            monitor3.HumidityValue = 0;
            monitor3.Location = new Point(951, 13);
            monitor3.Margin = new Padding(4);
            monitor3.Name = "monitor3";
            monitor3.Size = new Size(435, 292);
            monitor3.TabIndex = 0;
            monitor3.TempValue = 10F;
            monitor3.ZoneName = "仓库分区：A区-03";
            // 
            // monitor4
            // 
            monitor4.GroupName = "仓库分区：A区-01";
            monitor4.HumidityValue = 0;
            monitor4.Location = new Point(15, 348);
            monitor4.Margin = new Padding(4);
            monitor4.Name = "monitor4";
            monitor4.Size = new Size(435, 292);
            monitor4.TabIndex = 0;
            monitor4.TempValue = 10F;
            monitor4.ZoneName = "仓库分区：B区-01";
            // 
            // monitor5
            // 
            monitor5.GroupName = "仓库分区：A区-01";
            monitor5.HumidityValue = 0;
            monitor5.Location = new Point(483, 348);
            monitor5.Margin = new Padding(4);
            monitor5.Name = "monitor5";
            monitor5.Size = new Size(435, 292);
            monitor5.TabIndex = 0;
            monitor5.TempValue = 10F;
            monitor5.ZoneName = "仓库分区：B区-02";
            // 
            // monitor6
            // 
            monitor6.GroupName = "仓库分区：A区-01";
            monitor6.HumidityValue = 0;
            monitor6.Location = new Point(951, 348);
            monitor6.Margin = new Padding(4);
            monitor6.Name = "monitor6";
            monitor6.Size = new Size(435, 292);
            monitor6.TabIndex = 0;
            monitor6.TempValue = 10F;
            monitor6.ZoneName = "仓库分区：B区-03";
            // 
            // FrmCentralMonitor
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(56, 59, 138);
            ClientSize = new Size(1160, 739);
            Controls.Add(monitor6);
            Controls.Add(monitor3);
            Controls.Add(monitor5);
            Controls.Add(monitor2);
            Controls.Add(monitor4);
            Controls.Add(monitor1);
            Font = new Font("微软雅黑", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmCentralMonitor";
            Text = "集中监控";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private Monitor monitor1;
        private Monitor monitor2;
        private Monitor monitor3;
        private Monitor monitor4;
        private Monitor monitor5;
        private Monitor monitor6;
    }
}
