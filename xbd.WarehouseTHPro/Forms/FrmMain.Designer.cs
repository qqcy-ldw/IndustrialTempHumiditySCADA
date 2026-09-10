namespace xbd.WarehouseTHPro
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            pnlHeader = new Panel();
            lblTitle = new Label();
            pbLogo = new PictureBox();
            pnlMenu = new Panel();
            btnUserManage = new Button();
            btnAlarmRecord = new Button();
            btnHistoryTrend = new Button();
            btnParamConfig = new Button();
            btnRealtimeTrend = new Button();
            btnCentralMonitor = new Button();
            pnlContent = new Panel();
            pnlFooter = new Panel();
            lblSystemTime = new Label();
            lblLoginUser = new Label();
            lblSeparator2 = new Label();
            lblAreaB = new Label();
            lblAreaA = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            button1 = new Button();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            pnlMenu.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(43, 50, 120);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(pbLogo);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1622, 81);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("微软雅黑", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(96, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(428, 45);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "信必达仓储温湿度监控系统";
            // 
            // pbLogo
            // 
            pbLogo.Image = (Image)resources.GetObject("pbLogo.Image");
            pbLogo.Location = new Point(26, 14);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(58, 53);
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLogo.TabIndex = 0;
            pbLogo.TabStop = false;
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.FromArgb(43, 50, 120);
            pnlMenu.Controls.Add(btnUserManage);
            pnlMenu.Controls.Add(btnAlarmRecord);
            pnlMenu.Controls.Add(btnHistoryTrend);
            pnlMenu.Controls.Add(btnParamConfig);
            pnlMenu.Controls.Add(btnRealtimeTrend);
            pnlMenu.Controls.Add(btnCentralMonitor);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 81);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(215, 691);
            pnlMenu.TabIndex = 1;
            // 
            // btnUserManage
            // 
            btnUserManage.BackColor = Color.FromArgb(43, 50, 120);
            btnUserManage.Dock = DockStyle.Top;
            btnUserManage.FlatAppearance.BorderSize = 0;
            btnUserManage.FlatStyle = FlatStyle.Flat;
            btnUserManage.Font = new Font("微软雅黑", 12F);
            btnUserManage.ForeColor = Color.White;
            btnUserManage.Image = (Image)resources.GetObject("btnUserManage.Image");
            btnUserManage.ImageAlign = ContentAlignment.MiddleLeft;
            btnUserManage.Location = new Point(0, 470);
            btnUserManage.Name = "btnUserManage";
            btnUserManage.Padding = new Padding(32, 0, 0, 0);
            btnUserManage.Size = new Size(215, 94);
            btnUserManage.TabIndex = 6;
            btnUserManage.Text = "   用户管理";
            btnUserManage.TextAlign = ContentAlignment.MiddleLeft;
            btnUserManage.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnUserManage.UseVisualStyleBackColor = false;
            // 
            // btnAlarmRecord
            // 
            btnAlarmRecord.BackColor = Color.FromArgb(43, 50, 120);
            btnAlarmRecord.Dock = DockStyle.Top;
            btnAlarmRecord.FlatAppearance.BorderSize = 0;
            btnAlarmRecord.FlatStyle = FlatStyle.Flat;
            btnAlarmRecord.Font = new Font("微软雅黑", 12F);
            btnAlarmRecord.ForeColor = Color.White;
            btnAlarmRecord.Image = (Image)resources.GetObject("btnAlarmRecord.Image");
            btnAlarmRecord.ImageAlign = ContentAlignment.MiddleLeft;
            btnAlarmRecord.Location = new Point(0, 376);
            btnAlarmRecord.Name = "btnAlarmRecord";
            btnAlarmRecord.Padding = new Padding(32, 0, 0, 0);
            btnAlarmRecord.Size = new Size(215, 94);
            btnAlarmRecord.TabIndex = 4;
            btnAlarmRecord.Text = "   报警记录";
            btnAlarmRecord.TextAlign = ContentAlignment.MiddleLeft;
            btnAlarmRecord.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAlarmRecord.UseVisualStyleBackColor = false;
            // 
            // btnHistoryTrend
            // 
            btnHistoryTrend.BackColor = Color.FromArgb(43, 50, 120);
            btnHistoryTrend.Dock = DockStyle.Top;
            btnHistoryTrend.FlatAppearance.BorderSize = 0;
            btnHistoryTrend.FlatStyle = FlatStyle.Flat;
            btnHistoryTrend.Font = new Font("微软雅黑", 12F);
            btnHistoryTrend.ForeColor = Color.White;
            btnHistoryTrend.Image = (Image)resources.GetObject("btnHistoryTrend.Image");
            btnHistoryTrend.ImageAlign = ContentAlignment.MiddleLeft;
            btnHistoryTrend.Location = new Point(0, 282);
            btnHistoryTrend.Name = "btnHistoryTrend";
            btnHistoryTrend.Padding = new Padding(32, 0, 0, 0);
            btnHistoryTrend.Size = new Size(215, 94);
            btnHistoryTrend.TabIndex = 3;
            btnHistoryTrend.Text = "   历史趋势";
            btnHistoryTrend.TextAlign = ContentAlignment.MiddleLeft;
            btnHistoryTrend.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnHistoryTrend.UseVisualStyleBackColor = false;
            // 
            // btnParamConfig
            // 
            btnParamConfig.BackColor = Color.FromArgb(43, 50, 120);
            btnParamConfig.Dock = DockStyle.Top;
            btnParamConfig.FlatAppearance.BorderSize = 0;
            btnParamConfig.FlatStyle = FlatStyle.Flat;
            btnParamConfig.Font = new Font("微软雅黑", 12F);
            btnParamConfig.ForeColor = Color.White;
            btnParamConfig.Image = (Image)resources.GetObject("btnParamConfig.Image");
            btnParamConfig.ImageAlign = ContentAlignment.MiddleLeft;
            btnParamConfig.Location = new Point(0, 188);
            btnParamConfig.Name = "btnParamConfig";
            btnParamConfig.Padding = new Padding(32, 0, 0, 0);
            btnParamConfig.Size = new Size(215, 94);
            btnParamConfig.TabIndex = 2;
            btnParamConfig.Text = "   参数配置";
            btnParamConfig.TextAlign = ContentAlignment.MiddleLeft;
            btnParamConfig.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnParamConfig.UseVisualStyleBackColor = false;
            // 
            // btnRealtimeTrend
            // 
            btnRealtimeTrend.BackColor = Color.FromArgb(43, 50, 120);
            btnRealtimeTrend.Dock = DockStyle.Top;
            btnRealtimeTrend.FlatAppearance.BorderSize = 0;
            btnRealtimeTrend.FlatStyle = FlatStyle.Flat;
            btnRealtimeTrend.Font = new Font("微软雅黑", 12F);
            btnRealtimeTrend.ForeColor = Color.White;
            btnRealtimeTrend.Image = (Image)resources.GetObject("btnRealtimeTrend.Image");
            btnRealtimeTrend.ImageAlign = ContentAlignment.MiddleLeft;
            btnRealtimeTrend.Location = new Point(0, 94);
            btnRealtimeTrend.Name = "btnRealtimeTrend";
            btnRealtimeTrend.Padding = new Padding(32, 0, 0, 0);
            btnRealtimeTrend.Size = new Size(215, 94);
            btnRealtimeTrend.TabIndex = 1;
            btnRealtimeTrend.Text = "   实时趋势";
            btnRealtimeTrend.TextAlign = ContentAlignment.MiddleLeft;
            btnRealtimeTrend.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRealtimeTrend.UseVisualStyleBackColor = false;
            // 
            // btnCentralMonitor
            // 
            btnCentralMonitor.BackColor = Color.FromArgb(43, 50, 120);
            btnCentralMonitor.Dock = DockStyle.Top;
            btnCentralMonitor.FlatAppearance.BorderSize = 0;
            btnCentralMonitor.FlatStyle = FlatStyle.Flat;
            btnCentralMonitor.Font = new Font("微软雅黑", 12F);
            btnCentralMonitor.ForeColor = Color.White;
            btnCentralMonitor.Image = (Image)resources.GetObject("btnCentralMonitor.Image");
            btnCentralMonitor.ImageAlign = ContentAlignment.MiddleLeft;
            btnCentralMonitor.Location = new Point(0, 0);
            btnCentralMonitor.Name = "btnCentralMonitor";
            btnCentralMonitor.Padding = new Padding(32, 0, 0, 0);
            btnCentralMonitor.Size = new Size(215, 94);
            btnCentralMonitor.TabIndex = 0;
            btnCentralMonitor.Text = "   集中监控";
            btnCentralMonitor.TextAlign = ContentAlignment.MiddleLeft;
            btnCentralMonitor.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCentralMonitor.UseVisualStyleBackColor = false;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(56, 59, 138);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(215, 81);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1407, 691);
            pnlContent.TabIndex = 2;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(43, 50, 120);
            pnlFooter.Controls.Add(button1);
            pnlFooter.Controls.Add(lblSystemTime);
            pnlFooter.Controls.Add(lblLoginUser);
            pnlFooter.Controls.Add(lblSeparator2);
            pnlFooter.Controls.Add(lblAreaB);
            pnlFooter.Controls.Add(lblAreaA);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 772);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(1622, 47);
            pnlFooter.TabIndex = 3;
            // 
            // lblSystemTime
            // 
            lblSystemTime.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSystemTime.AutoSize = true;
            lblSystemTime.Font = new Font("微软雅黑", 10F);
            lblSystemTime.ForeColor = Color.White;
            lblSystemTime.Location = new Point(1273, 13);
            lblSystemTime.Name = "lblSystemTime";
            lblSystemTime.Size = new Size(291, 23);
            lblSystemTime.TabIndex = 4;
            lblSystemTime.Text = "系统时间: 2024年09月17日 20:00:00";
            // 
            // lblLoginUser
            // 
            lblLoginUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblLoginUser.AutoSize = true;
            lblLoginUser.Font = new Font("微软雅黑", 10F);
            lblLoginUser.ForeColor = Color.White;
            lblLoginUser.Location = new Point(1105, 13);
            lblLoginUser.Name = "lblLoginUser";
            lblLoginUser.Size = new Size(138, 23);
            lblLoginUser.TabIndex = 3;
            lblLoginUser.Text = "登录用户: 未登录";
            // 
            // lblSeparator2
            // 
            lblSeparator2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSeparator2.AutoSize = true;
            lblSeparator2.Font = new Font("微软雅黑", 10F);
            lblSeparator2.ForeColor = Color.White;
            lblSeparator2.Location = new Point(1239, 13);
            lblSeparator2.Name = "lblSeparator2";
            lblSeparator2.Size = new Size(25, 23);
            lblSeparator2.TabIndex = 2;
            lblSeparator2.Text = " | ";
            // 
            // lblAreaB
            // 
            lblAreaB.AutoSize = true;
            lblAreaB.Font = new Font("微软雅黑", 10F);
            lblAreaB.ForeColor = Color.White;
            lblAreaB.Location = new Point(270, 13);
            lblAreaB.Name = "lblAreaB";
            lblAreaB.Size = new Size(204, 23);
            lblAreaB.TabIndex = 1;
            lblAreaB.Text = "B区:  串口关闭  |  000 ms";
            // 
            // lblAreaA
            // 
            lblAreaA.AutoSize = true;
            lblAreaA.Font = new Font("微软雅黑", 10F);
            lblAreaA.ForeColor = Color.White;
            lblAreaA.Location = new Point(26, 13);
            lblAreaA.Name = "lblAreaA";
            lblAreaA.Size = new Size(205, 23);
            lblAreaA.TabIndex = 0;
            lblAreaA.Text = "A区:  串口关闭  |  000 ms";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 200;
            timer1.Tick += timer1_Tick;
            // 
            // button1
            //
            button1.FlatStyle = FlatStyle.System;
            button1.Location = new Point(985, 11);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 5;
            button1.Text = "退出登录";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            //
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1622, 819);
            Controls.Add(pnlContent);
            Controls.Add(pnlMenu);
            Controls.Add(pnlFooter);
            Controls.Add(pnlHeader);
            Font = new Font("微软雅黑", 9F);
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "信必达仓储温湿度监控系统";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            pnlMenu.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Button btnCentralMonitor;
        private System.Windows.Forms.Button btnRealtimeTrend;
        private System.Windows.Forms.Button btnParamConfig;
        private System.Windows.Forms.Button btnHistoryTrend;
        private System.Windows.Forms.Button btnAlarmRecord;
        private System.Windows.Forms.Button btnUserManage;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblAreaA;
        private System.Windows.Forms.Label lblAreaB;
        private System.Windows.Forms.Label lblLoginUser;
        private System.Windows.Forms.Label lblSeparator2;
        private System.Windows.Forms.Label lblSystemTime;
        private System.Windows.Forms.Timer timer1;
        private Button button1;
    }
}
