namespace xbd.WarehouseTHPro
{
    partial class FrmRealtimeTrend
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
            pnlTop = new Panel();
            flpCheckBoxes = new FlowLayoutPanel();
            chkA01Temp = new CheckBox();
            chkA02Temp = new CheckBox();
            chkA03Temp = new CheckBox();
            chkB01Temp = new CheckBox();
            chkB02Temp = new CheckBox();
            chkB03Temp = new CheckBox();
            chkA01Hum = new CheckBox();
            chkA02Hum = new CheckBox();
            chkA03Hum = new CheckBox();
            chkB01Hum = new CheckBox();
            chkB02Hum = new CheckBox();
            chkB03Hum = new CheckBox();
            pnlMain = new Panel();
            formsPlot1 = new ScottPlot.FormsPlot();
            pnlRight = new Panel();
            lblValA03Hum = new Label();
            lblValA03Temp = new Label();
            lblNameA03Hum = new Label();
            lblNameA03Temp = new Label();
            lblValA02Hum = new Label();
            lblValA02Temp = new Label();
            lblNameA02Hum = new Label();
            lblNameA02Temp = new Label();
            lblValA01Hum = new Label();
            lblValA01Temp = new Label();
            lblNameA01Hum = new Label();
            lblNameA01Temp = new Label();
            lblValB03Hum = new Label();
            lblValB03Temp = new Label();
            lblNameB03Hum = new Label();
            lblNameB03Temp = new Label();
            lblValB02Hum = new Label();
            lblValB02Temp = new Label();
            lblNameB02Hum = new Label();
            lblNameB02Temp = new Label();
            lblValB01Hum = new Label();
            lblValB01Temp = new Label();
            lblNameB01Hum = new Label();
            lblNameB01Temp = new Label();
            pnlTop.SuspendLayout();
            flpCheckBoxes.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlRight.SuspendLayout();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(43, 50, 120);
            pnlTop.Controls.Add(flpCheckBoxes);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(10, 6, 10, 6);
            pnlTop.Size = new Size(1324, 72);
            pnlTop.TabIndex = 0;
            // 
            // flpCheckBoxes
            // 
            flpCheckBoxes.BackColor = Color.Transparent;
            flpCheckBoxes.Controls.Add(chkA01Temp);
            flpCheckBoxes.Controls.Add(chkA02Temp);
            flpCheckBoxes.Controls.Add(chkA03Temp);
            flpCheckBoxes.Controls.Add(chkB01Temp);
            flpCheckBoxes.Controls.Add(chkB02Temp);
            flpCheckBoxes.Controls.Add(chkB03Temp);
            flpCheckBoxes.Controls.Add(chkA01Hum);
            flpCheckBoxes.Controls.Add(chkA02Hum);
            flpCheckBoxes.Controls.Add(chkA03Hum);
            flpCheckBoxes.Controls.Add(chkB01Hum);
            flpCheckBoxes.Controls.Add(chkB02Hum);
            flpCheckBoxes.Controls.Add(chkB03Hum);
            flpCheckBoxes.Dock = DockStyle.Fill;
            flpCheckBoxes.Location = new Point(10, 6);
            flpCheckBoxes.Name = "flpCheckBoxes";
            flpCheckBoxes.Size = new Size(1304, 60);
            flpCheckBoxes.TabIndex = 0;
            // 
            // chkA01Temp
            // 
            chkA01Temp.AutoSize = true;
            chkA01Temp.Checked = true;
            chkA01Temp.CheckState = CheckState.Checked;
            chkA01Temp.Font = new Font("微软雅黑", 9F);
            chkA01Temp.ForeColor = Color.Tomato;
            chkA01Temp.Location = new Point(15, 6);
            chkA01Temp.Margin = new Padding(15, 6, 15, 6);
            chkA01Temp.Name = "chkA01Temp";
            chkA01Temp.Size = new Size(90, 24);
            chkA01Temp.TabIndex = 0;
            chkA01Temp.Text = "A01温度";
            chkA01Temp.UseVisualStyleBackColor = true;
            // 
            // chkA02Temp
            // 
            chkA02Temp.AutoSize = true;
            chkA02Temp.Checked = true;
            chkA02Temp.CheckState = CheckState.Checked;
            chkA02Temp.Font = new Font("微软雅黑", 9F);
            chkA02Temp.ForeColor = Color.Gold;
            chkA02Temp.Location = new Point(135, 6);
            chkA02Temp.Margin = new Padding(15, 6, 15, 6);
            chkA02Temp.Name = "chkA02Temp";
            chkA02Temp.Size = new Size(90, 24);
            chkA02Temp.TabIndex = 1;
            chkA02Temp.Text = "A02温度";
            chkA02Temp.UseVisualStyleBackColor = true;
            // 
            // chkA03Temp
            // 
            chkA03Temp.AutoSize = true;
            chkA03Temp.Checked = true;
            chkA03Temp.CheckState = CheckState.Checked;
            chkA03Temp.Font = new Font("微软雅黑", 9F);
            chkA03Temp.ForeColor = Color.LimeGreen;
            chkA03Temp.Location = new Point(255, 6);
            chkA03Temp.Margin = new Padding(15, 6, 15, 6);
            chkA03Temp.Name = "chkA03Temp";
            chkA03Temp.Size = new Size(90, 24);
            chkA03Temp.TabIndex = 2;
            chkA03Temp.Text = "A03温度";
            chkA03Temp.UseVisualStyleBackColor = true;
            // 
            // chkB01Temp
            // 
            chkB01Temp.AutoSize = true;
            chkB01Temp.Checked = true;
            chkB01Temp.CheckState = CheckState.Checked;
            chkB01Temp.Font = new Font("微软雅黑", 9F);
            chkB01Temp.ForeColor = Color.Coral;
            chkB01Temp.Location = new Point(375, 6);
            chkB01Temp.Margin = new Padding(15, 6, 15, 6);
            chkB01Temp.Name = "chkB01Temp";
            chkB01Temp.Size = new Size(88, 24);
            chkB01Temp.TabIndex = 3;
            chkB01Temp.Text = "B01温度";
            chkB01Temp.UseVisualStyleBackColor = true;
            // 
            // chkB02Temp
            // 
            chkB02Temp.AutoSize = true;
            chkB02Temp.Checked = true;
            chkB02Temp.CheckState = CheckState.Checked;
            chkB02Temp.Font = new Font("微软雅黑", 9F);
            chkB02Temp.ForeColor = Color.Aquamarine;
            chkB02Temp.Location = new Point(493, 6);
            chkB02Temp.Margin = new Padding(15, 6, 15, 6);
            chkB02Temp.Name = "chkB02Temp";
            chkB02Temp.Size = new Size(88, 24);
            chkB02Temp.TabIndex = 4;
            chkB02Temp.Text = "B02温度";
            chkB02Temp.UseVisualStyleBackColor = true;
            // 
            // chkB03Temp
            // 
            chkB03Temp.AutoSize = true;
            chkB03Temp.Checked = true;
            chkB03Temp.CheckState = CheckState.Checked;
            chkB03Temp.Font = new Font("微软雅黑", 9F);
            chkB03Temp.ForeColor = Color.Plum;
            chkB03Temp.Location = new Point(611, 6);
            chkB03Temp.Margin = new Padding(15, 6, 15, 6);
            chkB03Temp.Name = "chkB03Temp";
            chkB03Temp.Size = new Size(88, 24);
            chkB03Temp.TabIndex = 5;
            chkB03Temp.Text = "B03温度";
            chkB03Temp.UseVisualStyleBackColor = true;
            // 
            // chkA01Hum
            // 
            chkA01Hum.AutoSize = true;
            chkA01Hum.Checked = true;
            chkA01Hum.CheckState = CheckState.Checked;
            chkA01Hum.Font = new Font("微软雅黑", 9F);
            chkA01Hum.ForeColor = Color.DodgerBlue;
            chkA01Hum.Location = new Point(729, 6);
            chkA01Hum.Margin = new Padding(15, 6, 15, 6);
            chkA01Hum.Name = "chkA01Hum";
            chkA01Hum.Size = new Size(90, 24);
            chkA01Hum.TabIndex = 6;
            chkA01Hum.Text = "A01湿度";
            chkA01Hum.UseVisualStyleBackColor = true;
            // 
            // chkA02Hum
            // 
            chkA02Hum.AutoSize = true;
            chkA02Hum.Checked = true;
            chkA02Hum.CheckState = CheckState.Checked;
            chkA02Hum.Font = new Font("微软雅黑", 9F);
            chkA02Hum.ForeColor = Color.Cyan;
            chkA02Hum.Location = new Point(849, 6);
            chkA02Hum.Margin = new Padding(15, 6, 15, 6);
            chkA02Hum.Name = "chkA02Hum";
            chkA02Hum.Size = new Size(90, 24);
            chkA02Hum.TabIndex = 7;
            chkA02Hum.Text = "A02湿度";
            chkA02Hum.UseVisualStyleBackColor = true;
            // 
            // chkA03Hum
            // 
            chkA03Hum.AutoSize = true;
            chkA03Hum.Checked = true;
            chkA03Hum.CheckState = CheckState.Checked;
            chkA03Hum.Font = new Font("微软雅黑", 9F);
            chkA03Hum.ForeColor = Color.MediumPurple;
            chkA03Hum.Location = new Point(969, 6);
            chkA03Hum.Margin = new Padding(15, 6, 15, 6);
            chkA03Hum.Name = "chkA03Hum";
            chkA03Hum.Size = new Size(90, 24);
            chkA03Hum.TabIndex = 8;
            chkA03Hum.Text = "A03湿度";
            chkA03Hum.UseVisualStyleBackColor = true;
            // 
            // chkB01Hum
            // 
            chkB01Hum.AutoSize = true;
            chkB01Hum.Checked = true;
            chkB01Hum.CheckState = CheckState.Checked;
            chkB01Hum.Font = new Font("微软雅黑", 9F);
            chkB01Hum.ForeColor = Color.Fuchsia;
            chkB01Hum.Location = new Point(1089, 6);
            chkB01Hum.Margin = new Padding(15, 6, 15, 6);
            chkB01Hum.Name = "chkB01Hum";
            chkB01Hum.Size = new Size(88, 24);
            chkB01Hum.TabIndex = 9;
            chkB01Hum.Text = "B01湿度";
            chkB01Hum.UseVisualStyleBackColor = true;
            // 
            // chkB02Hum
            // 
            chkB02Hum.AutoSize = true;
            chkB02Hum.Checked = true;
            chkB02Hum.CheckState = CheckState.Checked;
            chkB02Hum.Font = new Font("微软雅黑", 9F);
            chkB02Hum.ForeColor = Color.SteelBlue;
            chkB02Hum.Location = new Point(15, 42);
            chkB02Hum.Margin = new Padding(15, 6, 15, 6);
            chkB02Hum.Name = "chkB02Hum";
            chkB02Hum.Size = new Size(88, 24);
            chkB02Hum.TabIndex = 10;
            chkB02Hum.Text = "B02湿度";
            chkB02Hum.UseVisualStyleBackColor = true;
            // 
            // chkB03Hum
            // 
            chkB03Hum.AutoSize = true;
            chkB03Hum.Checked = true;
            chkB03Hum.CheckState = CheckState.Checked;
            chkB03Hum.Font = new Font("微软雅黑", 9F);
            chkB03Hum.ForeColor = Color.DeepPink;
            chkB03Hum.Location = new Point(133, 42);
            chkB03Hum.Margin = new Padding(15, 6, 15, 6);
            chkB03Hum.Name = "chkB03Hum";
            chkB03Hum.Size = new Size(88, 24);
            chkB03Hum.TabIndex = 11;
            chkB03Hum.Text = "B03湿度";
            chkB03Hum.UseVisualStyleBackColor = true;
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(43, 50, 120);
            pnlMain.Controls.Add(formsPlot1);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 72);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(10);
            pnlMain.Size = new Size(1038, 667);
            pnlMain.TabIndex = 1;
            // 
            // formsPlot1
            // 
            formsPlot1.BackColor = Color.Transparent;
            formsPlot1.ForeColor = Color.White;
            formsPlot1.Location = new Point(53, 9);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(990, 606);
            formsPlot1.TabIndex = 0;
            // 
            // pnlRight
            // 
            pnlRight.BackColor = Color.FromArgb(43, 50, 120);
            pnlRight.Controls.Add(lblValA03Hum);
            pnlRight.Controls.Add(lblValA03Temp);
            pnlRight.Controls.Add(lblNameA03Hum);
            pnlRight.Controls.Add(lblNameA03Temp);
            pnlRight.Controls.Add(lblValA02Hum);
            pnlRight.Controls.Add(lblValA02Temp);
            pnlRight.Controls.Add(lblNameA02Hum);
            pnlRight.Controls.Add(lblNameA02Temp);
            pnlRight.Controls.Add(lblValA01Hum);
            pnlRight.Controls.Add(lblValA01Temp);
            pnlRight.Controls.Add(lblNameA01Hum);
            pnlRight.Controls.Add(lblNameA01Temp);
            pnlRight.Controls.Add(lblValB03Hum);
            pnlRight.Controls.Add(lblValB03Temp);
            pnlRight.Controls.Add(lblNameB03Hum);
            pnlRight.Controls.Add(lblNameB03Temp);
            pnlRight.Controls.Add(lblValB02Hum);
            pnlRight.Controls.Add(lblValB02Temp);
            pnlRight.Controls.Add(lblNameB02Hum);
            pnlRight.Controls.Add(lblNameB02Temp);
            pnlRight.Controls.Add(lblValB01Hum);
            pnlRight.Controls.Add(lblValB01Temp);
            pnlRight.Controls.Add(lblNameB01Hum);
            pnlRight.Controls.Add(lblNameB01Temp);
            pnlRight.Dock = DockStyle.Right;
            pnlRight.Location = new Point(1038, 72);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(8);
            pnlRight.Size = new Size(286, 667);
            pnlRight.TabIndex = 2;
            // 
            // lblValA03Hum
            // 
            lblValA03Hum.AutoSize = true;
            lblValA03Hum.Font = new Font("Consolas", 14F, FontStyle.Bold);
            lblValA03Hum.ForeColor = Color.MediumPurple;
            lblValA03Hum.Location = new Point(145, 271);
            lblValA03Hum.Name = "lblValA03Hum";
            lblValA03Hum.Size = new Size(64, 28);
            lblValA03Hum.TabIndex = 11;
            lblValA03Hum.Tag = "A03湿度";
            lblValA03Hum.Text = "00.0";
            // 
            // lblValA03Temp
            // 
            lblValA03Temp.AutoSize = true;
            lblValA03Temp.Font = new Font("Consolas", 14F, FontStyle.Bold);
            lblValA03Temp.ForeColor = Color.LimeGreen;
            lblValA03Temp.Location = new Point(145, 220);
            lblValA03Temp.Name = "lblValA03Temp";
            lblValA03Temp.Size = new Size(64, 28);
            lblValA03Temp.TabIndex = 9;
            lblValA03Temp.Tag = "A03温度";
            lblValA03Temp.Text = "00.0";
            // 
            // lblNameA03Hum
            // 
            lblNameA03Hum.AutoSize = true;
            lblNameA03Hum.Font = new Font("微软雅黑", 10F);
            lblNameA03Hum.ForeColor = Color.White;
            lblNameA03Hum.Location = new Point(23, 275);
            lblNameA03Hum.Name = "lblNameA03Hum";
            lblNameA03Hum.Size = new Size(76, 23);
            lblNameA03Hum.TabIndex = 10;
            lblNameA03Hum.Text = "A03湿度";
            // 
            // lblNameA03Temp
            // 
            lblNameA03Temp.AutoSize = true;
            lblNameA03Temp.Font = new Font("微软雅黑", 10F);
            lblNameA03Temp.ForeColor = Color.White;
            lblNameA03Temp.Location = new Point(23, 224);
            lblNameA03Temp.Name = "lblNameA03Temp";
            lblNameA03Temp.Size = new Size(76, 23);
            lblNameA03Temp.TabIndex = 8;
            lblNameA03Temp.Text = "A03温度";
            // 
            // lblValA02Hum
            // 
            lblValA02Hum.AutoSize = true;
            lblValA02Hum.Font = new Font("Consolas", 14F, FontStyle.Bold);
            lblValA02Hum.ForeColor = Color.Cyan;
            lblValA02Hum.Location = new Point(145, 169);
            lblValA02Hum.Name = "lblValA02Hum";
            lblValA02Hum.Size = new Size(64, 28);
            lblValA02Hum.TabIndex = 7;
            lblValA02Hum.Tag = "A02湿度";
            lblValA02Hum.Text = "00.0";
            // 
            // lblValA02Temp
            // 
            lblValA02Temp.AutoSize = true;
            lblValA02Temp.Font = new Font("Consolas", 14F, FontStyle.Bold);
            lblValA02Temp.ForeColor = Color.Gold;
            lblValA02Temp.Location = new Point(145, 118);
            lblValA02Temp.Name = "lblValA02Temp";
            lblValA02Temp.Size = new Size(64, 28);
            lblValA02Temp.TabIndex = 5;
            lblValA02Temp.Tag = "A02温度";
            lblValA02Temp.Text = "00.0";
            // 
            // lblNameA02Hum
            // 
            lblNameA02Hum.AutoSize = true;
            lblNameA02Hum.Font = new Font("微软雅黑", 10F);
            lblNameA02Hum.ForeColor = Color.White;
            lblNameA02Hum.Location = new Point(23, 173);
            lblNameA02Hum.Name = "lblNameA02Hum";
            lblNameA02Hum.Size = new Size(76, 23);
            lblNameA02Hum.TabIndex = 6;
            lblNameA02Hum.Text = "A02湿度";
            // 
            // lblNameA02Temp
            // 
            lblNameA02Temp.AutoSize = true;
            lblNameA02Temp.Font = new Font("微软雅黑", 10F);
            lblNameA02Temp.ForeColor = Color.White;
            lblNameA02Temp.Location = new Point(23, 122);
            lblNameA02Temp.Name = "lblNameA02Temp";
            lblNameA02Temp.Size = new Size(76, 23);
            lblNameA02Temp.TabIndex = 4;
            lblNameA02Temp.Text = "A02温度";
            // 
            // lblValA01Hum
            // 
            lblValA01Hum.AutoSize = true;
            lblValA01Hum.Font = new Font("Consolas", 14F, FontStyle.Bold);
            lblValA01Hum.ForeColor = Color.DodgerBlue;
            lblValA01Hum.Location = new Point(145, 67);
            lblValA01Hum.Name = "lblValA01Hum";
            lblValA01Hum.Size = new Size(64, 28);
            lblValA01Hum.TabIndex = 3;
            lblValA01Hum.Tag = "A01湿度";
            lblValA01Hum.Text = "00.0";
            // 
            // lblValA01Temp
            // 
            lblValA01Temp.AutoSize = true;
            lblValA01Temp.Font = new Font("Consolas", 14F, FontStyle.Bold);
            lblValA01Temp.ForeColor = Color.Tomato;
            lblValA01Temp.Location = new Point(145, 16);
            lblValA01Temp.Name = "lblValA01Temp";
            lblValA01Temp.Size = new Size(64, 28);
            lblValA01Temp.TabIndex = 1;
            lblValA01Temp.Tag = "A01温度";
            lblValA01Temp.Text = "00.0";
            // 
            // lblNameA01Hum
            // 
            lblNameA01Hum.AutoSize = true;
            lblNameA01Hum.Font = new Font("微软雅黑", 10F);
            lblNameA01Hum.ForeColor = Color.White;
            lblNameA01Hum.Location = new Point(23, 71);
            lblNameA01Hum.Name = "lblNameA01Hum";
            lblNameA01Hum.Size = new Size(76, 23);
            lblNameA01Hum.TabIndex = 2;
            lblNameA01Hum.Text = "A01湿度";
            // 
            // lblNameA01Temp
            // 
            lblNameA01Temp.AutoSize = true;
            lblNameA01Temp.Font = new Font("微软雅黑", 10F);
            lblNameA01Temp.ForeColor = Color.White;
            lblNameA01Temp.Location = new Point(23, 20);
            lblNameA01Temp.Name = "lblNameA01Temp";
            lblNameA01Temp.Size = new Size(76, 23);
            lblNameA01Temp.TabIndex = 0;
            lblNameA01Temp.Text = "A01温度";
            // 
            // lblValB03Hum
            // 
            lblValB03Hum.AutoSize = true;
            lblValB03Hum.Font = new Font("Consolas", 14F, FontStyle.Bold);
            lblValB03Hum.ForeColor = Color.DeepPink;
            lblValB03Hum.Location = new Point(145, 577);
            lblValB03Hum.Name = "lblValB03Hum";
            lblValB03Hum.Size = new Size(64, 28);
            lblValB03Hum.TabIndex = 23;
            lblValB03Hum.Tag = "B03湿度";
            lblValB03Hum.Text = "00.0";
            // 
            // lblValB03Temp
            // 
            lblValB03Temp.AutoSize = true;
            lblValB03Temp.Font = new Font("Consolas", 14F, FontStyle.Bold);
            lblValB03Temp.ForeColor = Color.Plum;
            lblValB03Temp.Location = new Point(145, 526);
            lblValB03Temp.Name = "lblValB03Temp";
            lblValB03Temp.Size = new Size(64, 28);
            lblValB03Temp.TabIndex = 21;
            lblValB03Temp.Tag = "B03温度";
            lblValB03Temp.Text = "00.0";
            // 
            // lblNameB03Hum
            // 
            lblNameB03Hum.AutoSize = true;
            lblNameB03Hum.Font = new Font("微软雅黑", 10F);
            lblNameB03Hum.ForeColor = Color.White;
            lblNameB03Hum.Location = new Point(24, 581);
            lblNameB03Hum.Name = "lblNameB03Hum";
            lblNameB03Hum.Size = new Size(75, 23);
            lblNameB03Hum.TabIndex = 22;
            lblNameB03Hum.Text = "B03湿度";
            // 
            // lblNameB03Temp
            // 
            lblNameB03Temp.AutoSize = true;
            lblNameB03Temp.Font = new Font("微软雅黑", 10F);
            lblNameB03Temp.ForeColor = Color.White;
            lblNameB03Temp.Location = new Point(23, 530);
            lblNameB03Temp.Name = "lblNameB03Temp";
            lblNameB03Temp.Size = new Size(75, 23);
            lblNameB03Temp.TabIndex = 20;
            lblNameB03Temp.Text = "B03温度";
            // 
            // lblValB02Hum
            // 
            lblValB02Hum.AutoSize = true;
            lblValB02Hum.Font = new Font("Consolas", 14F, FontStyle.Bold);
            lblValB02Hum.ForeColor = Color.SteelBlue;
            lblValB02Hum.Location = new Point(145, 475);
            lblValB02Hum.Name = "lblValB02Hum";
            lblValB02Hum.Size = new Size(64, 28);
            lblValB02Hum.TabIndex = 19;
            lblValB02Hum.Tag = "B02湿度";
            lblValB02Hum.Text = "00.0";
            // 
            // lblValB02Temp
            // 
            lblValB02Temp.AutoSize = true;
            lblValB02Temp.Font = new Font("Consolas", 14F, FontStyle.Bold);
            lblValB02Temp.ForeColor = Color.Aquamarine;
            lblValB02Temp.Location = new Point(145, 424);
            lblValB02Temp.Name = "lblValB02Temp";
            lblValB02Temp.Size = new Size(64, 28);
            lblValB02Temp.TabIndex = 17;
            lblValB02Temp.Tag = "B02温度";
            lblValB02Temp.Text = "00.0";
            // 
            // lblNameB02Hum
            // 
            lblNameB02Hum.AutoSize = true;
            lblNameB02Hum.Font = new Font("微软雅黑", 10F);
            lblNameB02Hum.ForeColor = Color.White;
            lblNameB02Hum.Location = new Point(23, 479);
            lblNameB02Hum.Name = "lblNameB02Hum";
            lblNameB02Hum.Size = new Size(75, 23);
            lblNameB02Hum.TabIndex = 18;
            lblNameB02Hum.Text = "B02湿度";
            // 
            // lblNameB02Temp
            // 
            lblNameB02Temp.AutoSize = true;
            lblNameB02Temp.Font = new Font("微软雅黑", 10F);
            lblNameB02Temp.ForeColor = Color.White;
            lblNameB02Temp.Location = new Point(23, 428);
            lblNameB02Temp.Name = "lblNameB02Temp";
            lblNameB02Temp.Size = new Size(75, 23);
            lblNameB02Temp.TabIndex = 16;
            lblNameB02Temp.Text = "B02温度";
            // 
            // lblValB01Hum
            // 
            lblValB01Hum.AutoSize = true;
            lblValB01Hum.Font = new Font("Consolas", 14F, FontStyle.Bold);
            lblValB01Hum.ForeColor = Color.Fuchsia;
            lblValB01Hum.Location = new Point(145, 373);
            lblValB01Hum.Name = "lblValB01Hum";
            lblValB01Hum.Size = new Size(64, 28);
            lblValB01Hum.TabIndex = 15;
            lblValB01Hum.Tag = "B01湿度";
            lblValB01Hum.Text = "00.0";
            // 
            // lblValB01Temp
            // 
            lblValB01Temp.AutoSize = true;
            lblValB01Temp.Font = new Font("Consolas", 14F, FontStyle.Bold);
            lblValB01Temp.ForeColor = Color.Coral;
            lblValB01Temp.Location = new Point(145, 322);
            lblValB01Temp.Name = "lblValB01Temp";
            lblValB01Temp.Size = new Size(64, 28);
            lblValB01Temp.TabIndex = 13;
            lblValB01Temp.Tag = "B01温度";
            lblValB01Temp.Text = "00.0";
            // 
            // lblNameB01Hum
            // 
            lblNameB01Hum.AutoSize = true;
            lblNameB01Hum.Font = new Font("微软雅黑", 10F);
            lblNameB01Hum.ForeColor = Color.White;
            lblNameB01Hum.Location = new Point(23, 377);
            lblNameB01Hum.Name = "lblNameB01Hum";
            lblNameB01Hum.Size = new Size(75, 23);
            lblNameB01Hum.TabIndex = 14;
            lblNameB01Hum.Text = "B01湿度";
            // 
            // lblNameB01Temp
            // 
            lblNameB01Temp.AutoSize = true;
            lblNameB01Temp.Font = new Font("微软雅黑", 10F);
            lblNameB01Temp.ForeColor = Color.White;
            lblNameB01Temp.Location = new Point(23, 326);
            lblNameB01Temp.Name = "lblNameB01Temp";
            lblNameB01Temp.Size = new Size(75, 23);
            lblNameB01Temp.TabIndex = 12;
            lblNameB01Temp.Text = "B01温度";
            // 
            // FrmRealtimeTrend
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(56, 59, 138);
            ClientSize = new Size(1324, 739);
            Controls.Add(pnlMain);
            Controls.Add(pnlRight);
            Controls.Add(pnlTop);
            Font = new Font("微软雅黑", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmRealtimeTrend";
            Text = "实时趋势";
            pnlTop.ResumeLayout(false);
            flpCheckBoxes.ResumeLayout(false);
            flpCheckBoxes.PerformLayout();
            pnlMain.ResumeLayout(false);
            pnlRight.ResumeLayout(false);
            pnlRight.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private Panel pnlTop;
        private FlowLayoutPanel flpCheckBoxes;
        private CheckBox chkA01Temp;
        private CheckBox chkA02Temp;
        private CheckBox chkA03Temp;
        private CheckBox chkB01Temp;
        private CheckBox chkB02Temp;
        private CheckBox chkB03Temp;
        private CheckBox chkA01Hum;
        private CheckBox chkA02Hum;
        private CheckBox chkA03Hum;
        private CheckBox chkB01Hum;
        private CheckBox chkB02Hum;
        private CheckBox chkB03Hum;
        private Panel pnlMain;
        private Panel pnlRight;
        private Label lblNameA01Temp;
        private Label lblValA01Temp;
        private Label lblNameA01Hum;
        private Label lblValA01Hum;
        private Label lblNameA02Temp;
        private Label lblValA02Temp;
        private Label lblNameA02Hum;
        private Label lblValA02Hum;
        private Label lblNameA03Temp;
        private Label lblValA03Temp;
        private Label lblNameA03Hum;
        private Label lblValA03Hum;
        private Label lblNameB01Temp;
        private Label lblValB01Temp;
        private Label lblNameB01Hum;
        private Label lblValB01Hum;
        private Label lblNameB02Temp;
        private Label lblValB02Temp;
        private Label lblNameB02Hum;
        private Label lblValB02Hum;
        private Label lblNameB03Temp;
        private Label lblValB03Temp;
        private Label lblNameB03Hum;
        private Label lblValB03Hum;
        private ScottPlot.FormsPlot formsPlot1;
    }
}
