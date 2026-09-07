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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            queryPanel = new Panel();
            lblFrom = new Label();
            dtpFrom = new DateTimePicker();
            lblTo = new Label();
            dtpTo = new DateTimePicker();
            lblZone = new Label();
            cboZone = new ComboBox();
            lblStatus = new Label();
            cboStatus = new ComboBox();
            btnQuery = new Button();
            dataGridView1 = new DataGridView();
            colOccurredAt = new DataGridViewTextBoxColumn();
            colRecoveredAt = new DataGridViewTextBoxColumn();
            colDeviceName = new DataGridViewTextBoxColumn();
            colZoneName = new DataGridViewTextBoxColumn();
            colVariableName = new DataGridViewTextBoxColumn();
            colAlarmType = new DataGridViewTextBoxColumn();
            colCurrentValue = new DataGridViewTextBoxColumn();
            colLimitValue = new DataGridViewTextBoxColumn();
            colAlarmNote = new DataGridViewTextBoxColumn();
            colIsActive = new DataGridViewTextBoxColumn();
            queryPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // queryPanel
            // 
            queryPanel.BackColor = Color.FromArgb(56, 59, 138);
            queryPanel.Controls.Add(lblFrom);
            queryPanel.Controls.Add(dtpFrom);
            queryPanel.Controls.Add(lblTo);
            queryPanel.Controls.Add(dtpTo);
            queryPanel.Controls.Add(lblZone);
            queryPanel.Controls.Add(cboZone);
            queryPanel.Controls.Add(lblStatus);
            queryPanel.Controls.Add(cboStatus);
            queryPanel.Controls.Add(btnQuery);
            queryPanel.Dock = DockStyle.Top;
            queryPanel.Location = new Point(0, 0);
            queryPanel.Name = "queryPanel";
            queryPanel.Size = new Size(1160, 58);
            queryPanel.TabIndex = 1;
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Font = new Font("微软雅黑", 10F);
            lblFrom.ForeColor = Color.White;
            lblFrom.Location = new Point(10, 17);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(78, 23);
            lblFrom.TabIndex = 0;
            lblFrom.Text = "开始时间";
            // 
            // dtpFrom
            // 
            dtpFrom.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpFrom.Font = new Font("微软雅黑", 9F);
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.Location = new Point(95, 14);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(198, 27);
            dtpFrom.TabIndex = 1;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Font = new Font("微软雅黑", 10F);
            lblTo.ForeColor = Color.White;
            lblTo.Location = new Point(312, 17);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(78, 23);
            lblTo.TabIndex = 2;
            lblTo.Text = "结束时间";
            // 
            // dtpTo
            // 
            dtpTo.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpTo.Font = new Font("微软雅黑", 9F);
            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.Location = new Point(400, 14);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(206, 27);
            dtpTo.TabIndex = 3;
            // 
            // lblZone
            // 
            lblZone.AutoSize = true;
            lblZone.Font = new Font("微软雅黑", 10F);
            lblZone.ForeColor = Color.White;
            lblZone.Location = new Point(612, 17);
            lblZone.Name = "lblZone";
            lblZone.Size = new Size(44, 23);
            lblZone.TabIndex = 4;
            lblZone.Text = "区域";
            // 
            // cboZone
            // 
            cboZone.DropDownStyle = ComboBoxStyle.DropDownList;
            cboZone.Font = new Font("微软雅黑", 9F);
            cboZone.Items.AddRange(new object[] { "全部区域", "A01", "A02", "A03", "B01", "B02", "B03" });
            cboZone.Location = new Point(657, 14);
            cboZone.Name = "cboZone";
            cboZone.Size = new Size(100, 28);
            cboZone.TabIndex = 5;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("微软雅黑", 10F);
            lblStatus.ForeColor = Color.White;
            lblStatus.Location = new Point(777, 17);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(44, 23);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "状态";
            // 
            // cboStatus
            // 
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.Font = new Font("微软雅黑", 9F);
            cboStatus.Items.AddRange(new object[] { "全部状态", "当前报警", "已恢复" });
            cboStatus.Location = new Point(827, 14);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(100, 28);
            cboStatus.TabIndex = 7;
            // 
            // btnQuery
            // 
            btnQuery.BackColor = Color.White;
            btnQuery.FlatStyle = FlatStyle.Flat;
            btnQuery.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnQuery.ForeColor = Color.FromArgb(43, 50, 120);
            btnQuery.Location = new Point(943, 11);
            btnQuery.Name = "btnQuery";
            btnQuery.Size = new Size(88, 32);
            btnQuery.TabIndex = 8;
            btnQuery.Text = "查询";
            btnQuery.UseVisualStyleBackColor = false;
            btnQuery.Click += btnQuery_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 240, 252);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(43, 50, 120);
            dataGridViewCellStyle2.Font = new Font("Microsoft YaHei UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeight = 34;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { colOccurredAt, colRecoveredAt, colDeviceName, colZoneName, colVariableName, colAlarmType, colCurrentValue, colLimitValue, colAlarmNote, colIsActive });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("微软雅黑", 10F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.Location = new Point(0, 58);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1160, 592);
            dataGridView1.TabIndex = 0;
            // 
            // colOccurredAt
            // 
            colOccurredAt.DataPropertyName = "OccurredAt";
            colOccurredAt.HeaderText = "发生时间";
            colOccurredAt.MinimumWidth = 6;
            colOccurredAt.Name = "colOccurredAt";
            colOccurredAt.ReadOnly = true;
            colOccurredAt.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colRecoveredAt
            // 
            colRecoveredAt.DataPropertyName = "RecoveredAt";
            colRecoveredAt.HeaderText = "恢复时间";
            colRecoveredAt.MinimumWidth = 6;
            colRecoveredAt.Name = "colRecoveredAt";
            colRecoveredAt.ReadOnly = true;
            colRecoveredAt.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colDeviceName
            // 
            colDeviceName.DataPropertyName = "DeviceName";
            colDeviceName.HeaderText = "设备名称";
            colDeviceName.MinimumWidth = 6;
            colDeviceName.Name = "colDeviceName";
            colDeviceName.ReadOnly = true;
            colDeviceName.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colZoneName
            // 
            colZoneName.DataPropertyName = "ZoneName";
            colZoneName.HeaderText = "区域";
            colZoneName.MinimumWidth = 6;
            colZoneName.Name = "colZoneName";
            colZoneName.ReadOnly = true;
            colZoneName.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colVariableName
            // 
            colVariableName.DataPropertyName = "VariableName";
            colVariableName.HeaderText = "变量名称";
            colVariableName.MinimumWidth = 6;
            colVariableName.Name = "colVariableName";
            colVariableName.ReadOnly = true;
            colVariableName.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colAlarmType
            // 
            colAlarmType.DataPropertyName = "AlarmType";
            colAlarmType.HeaderText = "报警类型";
            colAlarmType.MinimumWidth = 6;
            colAlarmType.Name = "colAlarmType";
            colAlarmType.ReadOnly = true;
            colAlarmType.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colCurrentValue
            // 
            colCurrentValue.DataPropertyName = "CurrentValue";
            colCurrentValue.HeaderText = "当前值";
            colCurrentValue.MinimumWidth = 6;
            colCurrentValue.Name = "colCurrentValue";
            colCurrentValue.ReadOnly = true;
            colCurrentValue.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colLimitValue
            // 
            colLimitValue.DataPropertyName = "LimitValue";
            colLimitValue.HeaderText = "限值";
            colLimitValue.MinimumWidth = 6;
            colLimitValue.Name = "colLimitValue";
            colLimitValue.ReadOnly = true;
            colLimitValue.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colAlarmNote
            // 
            colAlarmNote.DataPropertyName = "AlarmNote";
            colAlarmNote.HeaderText = "报警说明";
            colAlarmNote.MinimumWidth = 6;
            colAlarmNote.Name = "colAlarmNote";
            colAlarmNote.ReadOnly = true;
            colAlarmNote.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colIsActive
            // 
            colIsActive.DataPropertyName = "IsActive";
            colIsActive.HeaderText = "状态";
            colIsActive.MinimumWidth = 6;
            colIsActive.Name = "colIsActive";
            colIsActive.ReadOnly = true;
            colIsActive.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmAlarmRecord
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(56, 59, 138);
            ClientSize = new Size(1160, 650);
            Controls.Add(dataGridView1);
            Controls.Add(queryPanel);
            Font = new Font("微软雅黑", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmAlarmRecord";
            Text = "报警记录";
            queryPanel.ResumeLayout(false);
            queryPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel queryPanel;
        private Label lblFrom;
        private DateTimePicker dtpFrom;
        private Label lblTo;
        private DateTimePicker dtpTo;
        private Label lblZone;
        private ComboBox cboZone;
        private Label lblStatus;
        private ComboBox cboStatus;
        private Button btnQuery;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn colOccurredAt;
        private DataGridViewTextBoxColumn colRecoveredAt;
        private DataGridViewTextBoxColumn colDeviceName;
        private DataGridViewTextBoxColumn colZoneName;
        private DataGridViewTextBoxColumn colVariableName;
        private DataGridViewTextBoxColumn colAlarmType;
        private DataGridViewTextBoxColumn colCurrentValue;
        private DataGridViewTextBoxColumn colLimitValue;
        private DataGridViewTextBoxColumn colAlarmNote;
        private DataGridViewTextBoxColumn colIsActive;
    }
}
