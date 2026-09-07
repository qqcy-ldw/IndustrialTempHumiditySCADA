using ScottPlot;

namespace xbd.WarehouseTHPro
{
    partial class FrmHistoryTrend
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            queryPanel = new FlowLayoutPanel();
            lblCapFrom = new Label();
            dtpFrom = new DateTimePicker();
            lblCapTo = new Label();
            dtpTo = new DateTimePicker();
            lblCapZone = new Label();
            cboZone = new ComboBox();
            btnQuery = new Button();
            formsPlot1 = new FormsPlot();
            legendPanel = new FlowLayoutPanel();
            lblLegendTemp = new Label();
            lblLegendHum = new Label();
            lblLegendA01Temp = new Label();
            lblLegendA01Hum = new Label();
            lblLegendA02Temp = new Label();
            lblLegendA02Hum = new Label();
            lblLegendA03Temp = new Label();
            lblLegendA03Hum = new Label();
            lblLegendB01Temp = new Label();
            lblLegendB01Hum = new Label();
            lblLegendB02Temp = new Label();
            lblLegendB02Hum = new Label();
            lblLegendB03Temp = new Label();
            lblLegendB03Hum = new Label();
            dataGridView1 = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            DeviceName = new DataGridViewTextBoxColumn();
            ZoneName = new DataGridViewTextBoxColumn();
            Temperature = new DataGridViewTextBoxColumn();
            Humidity = new DataGridViewTextBoxColumn();
            RecordedAT = new DataGridViewTextBoxColumn();
            IsAvailable = new DataGridViewTextBoxColumn();
            queryPanel.SuspendLayout();
            legendPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // queryPanel
            // 
            queryPanel.BackColor = Color.FromArgb(43, 50, 120);
            queryPanel.Controls.Add(lblCapFrom);
            queryPanel.Controls.Add(dtpFrom);
            queryPanel.Controls.Add(lblCapTo);
            queryPanel.Controls.Add(dtpTo);
            queryPanel.Controls.Add(lblCapZone);
            queryPanel.Controls.Add(cboZone);
            queryPanel.Controls.Add(btnQuery);
            queryPanel.Dock = DockStyle.Top;
            queryPanel.Location = new Point(0, 0);
            queryPanel.Name = "queryPanel";
            queryPanel.Padding = new Padding(10, 8, 10, 5);
            queryPanel.Size = new Size(1253, 48);
            queryPanel.TabIndex = 3;
            queryPanel.WrapContents = false;
            // 
            // lblCapFrom
            // 
            lblCapFrom.AutoSize = true;
            lblCapFrom.Font = new Font("微软雅黑", 10F);
            lblCapFrom.ForeColor = Color.White;
            lblCapFrom.Location = new Point(22, 15);
            lblCapFrom.Margin = new Padding(12, 7, 5, 0);
            lblCapFrom.Name = "lblCapFrom";
            lblCapFrom.Size = new Size(78, 23);
            lblCapFrom.TabIndex = 0;
            lblCapFrom.Text = "开始时间";
            // 
            // dtpFrom
            // 
            dtpFrom.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpFrom.Font = new Font("微软雅黑", 9F);
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.Location = new Point(108, 11);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(207, 27);
            dtpFrom.TabIndex = 1;
            dtpFrom.Value = new DateTime(2026, 9, 2, 0, 0, 0, 0);
            // 
            // lblCapTo
            // 
            lblCapTo.AutoSize = true;
            lblCapTo.Font = new Font("微软雅黑", 10F);
            lblCapTo.ForeColor = Color.White;
            lblCapTo.Location = new Point(330, 15);
            lblCapTo.Margin = new Padding(12, 7, 5, 0);
            lblCapTo.Name = "lblCapTo";
            lblCapTo.Size = new Size(78, 23);
            lblCapTo.TabIndex = 2;
            lblCapTo.Text = "结束时间";
            // 
            // dtpTo
            // 
            dtpTo.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpTo.Font = new Font("微软雅黑", 9F);
            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.Location = new Point(416, 11);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(205, 27);
            dtpTo.TabIndex = 3;
            dtpTo.Value = new DateTime(2026, 9, 2, 22, 15, 48, 318);
            // 
            // lblCapZone
            // 
            lblCapZone.AutoSize = true;
            lblCapZone.Font = new Font("微软雅黑", 10F);
            lblCapZone.ForeColor = Color.White;
            lblCapZone.Location = new Point(636, 15);
            lblCapZone.Margin = new Padding(12, 7, 5, 0);
            lblCapZone.Name = "lblCapZone";
            lblCapZone.Size = new Size(44, 23);
            lblCapZone.TabIndex = 4;
            lblCapZone.Text = "区域";
            // 
            // cboZone
            // 
            cboZone.DropDownStyle = ComboBoxStyle.DropDownList;
            cboZone.Font = new Font("微软雅黑", 9F);
            cboZone.Items.AddRange(new object[] { "全部区域", "A01", "A02", "A03", "B01", "B02", "B03" });
            cboZone.Location = new Point(688, 11);
            cboZone.Name = "cboZone";
            cboZone.Size = new Size(100, 28);
            cboZone.TabIndex = 5;
            // 
            // btnQuery
            // 
            btnQuery.BackColor = Color.White;
            btnQuery.FlatAppearance.BorderColor = Color.FromArgb(150, 170, 255);
            btnQuery.FlatStyle = FlatStyle.Flat;
            btnQuery.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnQuery.ForeColor = Color.FromArgb(43, 50, 120);
            btnQuery.Location = new Point(803, 8);
            btnQuery.Margin = new Padding(12, 0, 0, 0);
            btnQuery.Name = "btnQuery";
            btnQuery.Size = new Size(78, 30);
            btnQuery.TabIndex = 6;
            btnQuery.Text = "查询";
            btnQuery.UseVisualStyleBackColor = false;
            btnQuery.Click += btnQuery_Click;
            // 
            // formsPlot1
            // 
            formsPlot1.BackColor = Color.FromArgb(43, 50, 120);
            formsPlot1.Dock = DockStyle.Top;
            formsPlot1.ForeColor = Color.White;
            formsPlot1.Location = new Point(0, 48);
            formsPlot1.Margin = new Padding(0);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(1128, 530);
            formsPlot1.TabIndex = 0;
            formsPlot1.Load += formsPlot1_Load;
            // 
            // legendPanel
            // 
            legendPanel.BackColor = Color.FromArgb(43, 50, 120);
            legendPanel.Controls.Add(lblLegendTemp);
            legendPanel.Controls.Add(lblLegendHum);
            legendPanel.Controls.Add(lblLegendA01Temp);
            legendPanel.Controls.Add(lblLegendA01Hum);
            legendPanel.Controls.Add(lblLegendA02Temp);
            legendPanel.Controls.Add(lblLegendA02Hum);
            legendPanel.Controls.Add(lblLegendA03Temp);
            legendPanel.Controls.Add(lblLegendA03Hum);
            legendPanel.Controls.Add(lblLegendB01Temp);
            legendPanel.Controls.Add(lblLegendB01Hum);
            legendPanel.Controls.Add(lblLegendB02Temp);
            legendPanel.Controls.Add(lblLegendB02Hum);
            legendPanel.Controls.Add(lblLegendB03Temp);
            legendPanel.Controls.Add(lblLegendB03Hum);
            legendPanel.Dock = DockStyle.Right;
            legendPanel.FlowDirection = FlowDirection.TopDown;
            legendPanel.Location = new Point(1128, 48);
            legendPanel.Name = "legendPanel";
            legendPanel.Padding = new Padding(8, 10, 4, 4);
            legendPanel.Size = new Size(125, 659);
            legendPanel.TabIndex = 1;
            legendPanel.WrapContents = false;
            // 
            // lblLegendTemp
            // 
            lblLegendTemp.AutoSize = true;
            lblLegendTemp.Font = new Font("微软雅黑", 9F);
            lblLegendTemp.ForeColor = Color.FromArgb(255, 159, 67);
            lblLegendTemp.Location = new Point(8, 12);
            lblLegendTemp.Margin = new Padding(0, 2, 0, 2);
            lblLegendTemp.Name = "lblLegendTemp";
            lblLegendTemp.Size = new Size(52, 20);
            lblLegendTemp.TabIndex = 0;
            lblLegendTemp.Text = "━ 温度";
            // 
            // lblLegendHum
            // 
            lblLegendHum.AutoSize = true;
            lblLegendHum.Font = new Font("微软雅黑", 9F);
            lblLegendHum.ForeColor = Color.FromArgb(54, 162, 235);
            lblLegendHum.Location = new Point(8, 36);
            lblLegendHum.Margin = new Padding(0, 2, 0, 2);
            lblLegendHum.Name = "lblLegendHum";
            lblLegendHum.Size = new Size(52, 20);
            lblLegendHum.TabIndex = 1;
            lblLegendHum.Text = "┄ 湿度";
            // 
            // lblLegendA01Temp
            // 
            lblLegendA01Temp.AutoSize = true;
            lblLegendA01Temp.Font = new Font("微软雅黑", 9F);
            lblLegendA01Temp.ForeColor = Color.FromArgb(255, 159, 67);
            lblLegendA01Temp.Location = new Point(8, 60);
            lblLegendA01Temp.Margin = new Padding(0, 2, 0, 2);
            lblLegendA01Temp.Name = "lblLegendA01Temp";
            lblLegendA01Temp.Size = new Size(85, 20);
            lblLegendA01Temp.TabIndex = 2;
            lblLegendA01Temp.Text = "━ A01 温度";
            // 
            // lblLegendA01Hum
            // 
            lblLegendA01Hum.AutoSize = true;
            lblLegendA01Hum.Font = new Font("微软雅黑", 9F);
            lblLegendA01Hum.ForeColor = Color.FromArgb(54, 162, 235);
            lblLegendA01Hum.Location = new Point(8, 84);
            lblLegendA01Hum.Margin = new Padding(0, 2, 0, 2);
            lblLegendA01Hum.Name = "lblLegendA01Hum";
            lblLegendA01Hum.Size = new Size(85, 20);
            lblLegendA01Hum.TabIndex = 3;
            lblLegendA01Hum.Text = "┄ A01 湿度";
            // 
            // lblLegendA02Temp
            // 
            lblLegendA02Temp.AutoSize = true;
            lblLegendA02Temp.Font = new Font("微软雅黑", 9F);
            lblLegendA02Temp.ForeColor = Color.FromArgb(255, 159, 67);
            lblLegendA02Temp.Location = new Point(8, 108);
            lblLegendA02Temp.Margin = new Padding(0, 2, 0, 2);
            lblLegendA02Temp.Name = "lblLegendA02Temp";
            lblLegendA02Temp.Size = new Size(85, 20);
            lblLegendA02Temp.TabIndex = 4;
            lblLegendA02Temp.Text = "━ A02 温度";
            // 
            // lblLegendA02Hum
            // 
            lblLegendA02Hum.AutoSize = true;
            lblLegendA02Hum.Font = new Font("微软雅黑", 9F);
            lblLegendA02Hum.ForeColor = Color.FromArgb(54, 162, 235);
            lblLegendA02Hum.Location = new Point(8, 132);
            lblLegendA02Hum.Margin = new Padding(0, 2, 0, 2);
            lblLegendA02Hum.Name = "lblLegendA02Hum";
            lblLegendA02Hum.Size = new Size(85, 20);
            lblLegendA02Hum.TabIndex = 5;
            lblLegendA02Hum.Text = "┄ A02 湿度";
            // 
            // lblLegendA03Temp
            // 
            lblLegendA03Temp.AutoSize = true;
            lblLegendA03Temp.Font = new Font("微软雅黑", 9F);
            lblLegendA03Temp.ForeColor = Color.FromArgb(255, 159, 67);
            lblLegendA03Temp.Location = new Point(8, 156);
            lblLegendA03Temp.Margin = new Padding(0, 2, 0, 2);
            lblLegendA03Temp.Name = "lblLegendA03Temp";
            lblLegendA03Temp.Size = new Size(85, 20);
            lblLegendA03Temp.TabIndex = 6;
            lblLegendA03Temp.Text = "━ A03 温度";
            // 
            // lblLegendA03Hum
            // 
            lblLegendA03Hum.AutoSize = true;
            lblLegendA03Hum.Font = new Font("微软雅黑", 9F);
            lblLegendA03Hum.ForeColor = Color.FromArgb(54, 162, 235);
            lblLegendA03Hum.Location = new Point(8, 180);
            lblLegendA03Hum.Margin = new Padding(0, 2, 0, 2);
            lblLegendA03Hum.Name = "lblLegendA03Hum";
            lblLegendA03Hum.Size = new Size(85, 20);
            lblLegendA03Hum.TabIndex = 7;
            lblLegendA03Hum.Text = "┄ A03 湿度";
            // 
            // lblLegendB01Temp
            // 
            lblLegendB01Temp.AutoSize = true;
            lblLegendB01Temp.Font = new Font("微软雅黑", 9F);
            lblLegendB01Temp.ForeColor = Color.FromArgb(255, 159, 67);
            lblLegendB01Temp.Location = new Point(8, 204);
            lblLegendB01Temp.Margin = new Padding(0, 2, 0, 2);
            lblLegendB01Temp.Name = "lblLegendB01Temp";
            lblLegendB01Temp.Size = new Size(83, 20);
            lblLegendB01Temp.TabIndex = 8;
            lblLegendB01Temp.Text = "━ B01 温度";
            // 
            // lblLegendB01Hum
            // 
            lblLegendB01Hum.AutoSize = true;
            lblLegendB01Hum.Font = new Font("微软雅黑", 9F);
            lblLegendB01Hum.ForeColor = Color.FromArgb(54, 162, 235);
            lblLegendB01Hum.Location = new Point(8, 228);
            lblLegendB01Hum.Margin = new Padding(0, 2, 0, 2);
            lblLegendB01Hum.Name = "lblLegendB01Hum";
            lblLegendB01Hum.Size = new Size(83, 20);
            lblLegendB01Hum.TabIndex = 9;
            lblLegendB01Hum.Text = "┄ B01 湿度";
            // 
            // lblLegendB02Temp
            // 
            lblLegendB02Temp.AutoSize = true;
            lblLegendB02Temp.Font = new Font("微软雅黑", 9F);
            lblLegendB02Temp.ForeColor = Color.FromArgb(255, 159, 67);
            lblLegendB02Temp.Location = new Point(8, 252);
            lblLegendB02Temp.Margin = new Padding(0, 2, 0, 2);
            lblLegendB02Temp.Name = "lblLegendB02Temp";
            lblLegendB02Temp.Size = new Size(83, 20);
            lblLegendB02Temp.TabIndex = 10;
            lblLegendB02Temp.Text = "━ B02 温度";
            // 
            // lblLegendB02Hum
            // 
            lblLegendB02Hum.AutoSize = true;
            lblLegendB02Hum.Font = new Font("微软雅黑", 9F);
            lblLegendB02Hum.ForeColor = Color.FromArgb(54, 162, 235);
            lblLegendB02Hum.Location = new Point(8, 276);
            lblLegendB02Hum.Margin = new Padding(0, 2, 0, 2);
            lblLegendB02Hum.Name = "lblLegendB02Hum";
            lblLegendB02Hum.Size = new Size(83, 20);
            lblLegendB02Hum.TabIndex = 11;
            lblLegendB02Hum.Text = "┄ B02 湿度";
            // 
            // lblLegendB03Temp
            // 
            lblLegendB03Temp.AutoSize = true;
            lblLegendB03Temp.Font = new Font("微软雅黑", 9F);
            lblLegendB03Temp.ForeColor = Color.FromArgb(255, 159, 67);
            lblLegendB03Temp.Location = new Point(8, 300);
            lblLegendB03Temp.Margin = new Padding(0, 2, 0, 2);
            lblLegendB03Temp.Name = "lblLegendB03Temp";
            lblLegendB03Temp.Size = new Size(83, 20);
            lblLegendB03Temp.TabIndex = 12;
            lblLegendB03Temp.Text = "━ B03 温度";
            // 
            // lblLegendB03Hum
            // 
            lblLegendB03Hum.AutoSize = true;
            lblLegendB03Hum.Font = new Font("微软雅黑", 9F);
            lblLegendB03Hum.ForeColor = Color.FromArgb(54, 162, 235);
            lblLegendB03Hum.Location = new Point(8, 324);
            lblLegendB03Hum.Margin = new Padding(0, 2, 0, 2);
            lblLegendB03Hum.Name = "lblLegendB03Hum";
            lblLegendB03Hum.Size = new Size(83, 20);
            lblLegendB03Hum.TabIndex = 13;
            lblLegendB03Hum.Text = "┄ B03 湿度";
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(246, 248, 252);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(43, 50, 120);
            dataGridViewCellStyle2.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Id, DeviceName, ZoneName, Temperature, Humidity, RecordedAT, IsAvailable });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = SystemColors.Window;
            dataGridViewCellStyle6.Font = new Font("微软雅黑", 9F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(45, 48, 55);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(218, 230, 255);
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(30, 45, 80);
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            dataGridView1.Dock = DockStyle.Bottom;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.FromArgb(220, 224, 235);
            dataGridView1.Location = new Point(0, 519);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1128, 188);
            dataGridView1.TabIndex = 4;
            // 
            // Id
            // 
            Id.DataPropertyName = "Id";
            Id.HeaderText = "编号";
            Id.MinimumWidth = 6;
            Id.Name = "Id";
            Id.ReadOnly = true;
            // 
            // DeviceName
            // 
            DeviceName.DataPropertyName = "DeviceName";
            DeviceName.HeaderText = "设备名称";
            DeviceName.MinimumWidth = 6;
            DeviceName.Name = "DeviceName";
            DeviceName.ReadOnly = true;
            // 
            // ZoneName
            // 
            ZoneName.DataPropertyName = "ZoneName";
            ZoneName.HeaderText = "区域名称";
            ZoneName.MinimumWidth = 6;
            ZoneName.Name = "ZoneName";
            ZoneName.ReadOnly = true;
            // 
            // Temperature
            // 
            Temperature.DataPropertyName = "Temperature";
            dataGridViewCellStyle3.Format = "F1";
            Temperature.DefaultCellStyle = dataGridViewCellStyle3;
            Temperature.HeaderText = "温度";
            Temperature.MinimumWidth = 6;
            Temperature.Name = "Temperature";
            Temperature.ReadOnly = true;
            // 
            // Humidity
            // 
            Humidity.DataPropertyName = "Humidity";
            dataGridViewCellStyle4.Format = "F1";
            Humidity.DefaultCellStyle = dataGridViewCellStyle4;
            Humidity.HeaderText = "湿度";
            Humidity.MinimumWidth = 6;
            Humidity.Name = "Humidity";
            Humidity.ReadOnly = true;
            // 
            // RecordedAT
            // 
            RecordedAT.DataPropertyName = "RecordedAt";
            dataGridViewCellStyle5.Format = "yyyy-MM-dd HH:mm:ss";
            RecordedAT.DefaultCellStyle = dataGridViewCellStyle5;
            RecordedAT.HeaderText = "采集时间";
            RecordedAT.MinimumWidth = 6;
            RecordedAT.Name = "RecordedAT";
            RecordedAT.ReadOnly = true;
            // 
            // IsAvailable
            // 
            IsAvailable.DataPropertyName = "IsAvailable";
            IsAvailable.HeaderText = "设备状态";
            IsAvailable.MinimumWidth = 6;
            IsAvailable.Name = "IsAvailable";
            IsAvailable.ReadOnly = true;
            // 
            // FrmHistoryTrend
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(56, 59, 138);
            ClientSize = new Size(1253, 707);
            Controls.Add(dataGridView1);
            Controls.Add(formsPlot1);
            Controls.Add(legendPanel);
            Controls.Add(queryPanel);
            Font = new Font("微软雅黑", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmHistoryTrend";
            Text = "历史趋势";
            queryPanel.ResumeLayout(false);
            queryPanel.PerformLayout();
            legendPanel.ResumeLayout(false);
            legendPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private FlowLayoutPanel queryPanel;
        private Label lblCapFrom;
        private DateTimePicker dtpFrom;
        private Label lblCapTo;
        private DateTimePicker dtpTo;
        private Label lblCapZone;
        private ComboBox cboZone;
        private Button btnQuery;
        private FormsPlot formsPlot1;
        private FlowLayoutPanel legendPanel;
        private Label lblLegendTemp;
        private Label lblLegendHum;
        private Label lblLegendA01Temp;
        private Label lblLegendA01Hum;
        private Label lblLegendA02Temp;
        private Label lblLegendA02Hum;
        private Label lblLegendA03Temp;
        private Label lblLegendA03Hum;
        private Label lblLegendB01Temp;
        private Label lblLegendB01Hum;
        private Label lblLegendB02Temp;
        private Label lblLegendB02Hum;
        private Label lblLegendB03Temp;
        private Label lblLegendB03Hum;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn DeviceName;
        private DataGridViewTextBoxColumn ZoneName;
        private DataGridViewTextBoxColumn Temperature;
        private DataGridViewTextBoxColumn Humidity;
        private DataGridViewTextBoxColumn RecordedAT;
        private DataGridViewTextBoxColumn IsAvailable;
    }
}
