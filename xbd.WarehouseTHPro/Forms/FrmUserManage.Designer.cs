namespace xbd.WarehouseTHPro
{
    partial class FrmUserManage
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblCount = new System.Windows.Forms.Label();
            this.lblDebugTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // timer1
            //
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            //
            // lblCount
            //
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.lblCount.ForeColor = System.Drawing.Color.White;
            this.lblCount.Location = new System.Drawing.Point(565, 20);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(215, 31);
            this.lblCount.TabIndex = 1;
            this.lblCount.Text = "已运行: 0 秒";
            //
            // lblDebugTitle
            //
            this.lblDebugTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDebugTitle.Font = new System.Drawing.Font("微软雅黑", 48F, System.Drawing.FontStyle.Bold);
            this.lblDebugTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.lblDebugTitle.Name = "lblDebugTitle";
            this.lblDebugTitle.Size = new System.Drawing.Size(800, 450);
            this.lblDebugTitle.TabIndex = 0;
            this.lblDebugTitle.Text = "用户管理";
            this.lblDebugTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // FrmUserManage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(59)))), ((int)(((byte)(138)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblDebugTitle);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmUserManage";
            this.Text = "用户管理";
            this.TopLevel = false;
            this.Visible = true;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblDebugTitle;
    }
}
