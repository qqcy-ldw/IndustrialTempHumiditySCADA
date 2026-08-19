namespace xbd.WarehouseTHPro
{
    partial class FrmAlarmRecord
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
            lblCount = new Label();
            lblDebugTitle = new Label();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // lblCount
            // 
            lblCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCount.AutoSize = true;
            lblCount.Font = new Font("微软雅黑", 14F, FontStyle.Bold);
            lblCount.ForeColor = Color.White;
            lblCount.Location = new Point(925, 20);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(146, 31);
            lblCount.TabIndex = 1;
            lblCount.Text = "已运行: 0 秒";
            // 
            // lblDebugTitle
            // 
            lblDebugTitle.Dock = DockStyle.Fill;
            lblDebugTitle.Font = new Font("微软雅黑", 48F, FontStyle.Bold);
            lblDebugTitle.ForeColor = Color.FromArgb(200, 210, 255);
            lblDebugTitle.Location = new Point(0, 0);
            lblDebugTitle.Name = "lblDebugTitle";
            lblDebugTitle.Size = new Size(1160, 532);
            lblDebugTitle.TabIndex = 0;
            lblDebugTitle.Text = "报警记录";
            lblDebugTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FrmAlarmRecord
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(56, 59, 138);
            ClientSize = new Size(1160, 532);
            Controls.Add(lblCount);
            Controls.Add(lblDebugTitle);
            Font = new Font("微软雅黑", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmAlarmRecord";
            Text = "报警记录";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblDebugTitle;
    }
}
