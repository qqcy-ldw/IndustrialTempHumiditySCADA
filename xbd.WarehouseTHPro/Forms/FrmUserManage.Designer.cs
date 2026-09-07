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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            pnlFilter = new Panel();
            lblKeyword = new Label();
            txtKeyword = new TextBox();
            btnSearch = new Button();
            pnlEditor = new Panel();
            lblUserName = new Label();
            txtUserName = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblRole = new Label();
            cboRole = new ComboBox();
            chkEnabled = new CheckBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            dataGridView1 = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colUserName = new DataGridViewTextBoxColumn();
            colRoleName = new DataGridViewTextBoxColumn();
            colIsEnabled = new DataGridViewCheckBoxColumn();
            colCreatedAt = new DataGridViewTextBoxColumn();
            colUpdatedAt = new DataGridViewTextBoxColumn();
            pnlFilter.SuspendLayout();
            pnlEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // pnlFilter
            // 
            pnlFilter.BackColor = Color.FromArgb(56, 59, 138);
            pnlFilter.Controls.Add(lblKeyword);
            pnlFilter.Controls.Add(txtKeyword);
            pnlFilter.Controls.Add(btnSearch);
            pnlFilter.Dock = DockStyle.Top;
            pnlFilter.Location = new Point(0, 0);
            pnlFilter.Name = "pnlFilter";
            pnlFilter.Size = new Size(1160, 58);
            pnlFilter.TabIndex = 0;
            // 
            // lblKeyword
            // 
            lblKeyword.AutoSize = true;
            lblKeyword.Font = new Font("微软雅黑", 10F);
            lblKeyword.ForeColor = Color.White;
            lblKeyword.Location = new Point(20, 17);
            lblKeyword.Name = "lblKeyword";
            lblKeyword.Size = new Size(78, 23);
            lblKeyword.TabIndex = 0;
            lblKeyword.Text = "账号查询";
            // 
            // txtKeyword
            // 
            txtKeyword.Font = new Font("微软雅黑", 9F);
            txtKeyword.Location = new Point(105, 14);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(220, 27);
            txtKeyword.TabIndex = 1;
            txtKeyword.KeyDown += txtKeyword_KeyDown;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnSearch.ForeColor = Color.FromArgb(43, 50, 120);
            btnSearch.Location = new Point(340, 11);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(88, 32);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "查询";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // pnlEditor
            // 
            pnlEditor.BackColor = Color.White;
            pnlEditor.Controls.Add(lblUserName);
            pnlEditor.Controls.Add(txtUserName);
            pnlEditor.Controls.Add(lblPassword);
            pnlEditor.Controls.Add(txtPassword);
            pnlEditor.Controls.Add(lblRole);
            pnlEditor.Controls.Add(cboRole);
            pnlEditor.Controls.Add(chkEnabled);
            pnlEditor.Controls.Add(btnAdd);
            pnlEditor.Controls.Add(btnUpdate);
            pnlEditor.Controls.Add(btnDelete);
            pnlEditor.Controls.Add(btnClear);
            pnlEditor.Dock = DockStyle.Top;
            pnlEditor.Location = new Point(0, 58);
            pnlEditor.Name = "pnlEditor";
            pnlEditor.Size = new Size(1160, 112);
            pnlEditor.TabIndex = 1;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("微软雅黑", 10F);
            lblUserName.Location = new Point(20, 19);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(44, 23);
            lblUserName.TabIndex = 0;
            lblUserName.Text = "账号";
            // 
            // txtUserName
            // 
            txtUserName.Font = new Font("微软雅黑", 9F);
            txtUserName.Location = new Point(70, 16);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(180, 27);
            txtUserName.TabIndex = 1;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("微软雅黑", 10F);
            lblPassword.Location = new Point(278, 19);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(44, 23);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "密码";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("微软雅黑", 9F);
            txtPassword.Location = new Point(328, 16);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(180, 27);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font("微软雅黑", 10F);
            lblRole.Location = new Point(536, 19);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(44, 23);
            lblRole.TabIndex = 4;
            lblRole.Text = "角色";
            // 
            // cboRole
            // 
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.Font = new Font("微软雅黑", 9F);
            cboRole.FormattingEnabled = true;
            cboRole.Items.AddRange(new object[] { "管理员", "操作员" });
            cboRole.Location = new Point(586, 16);
            cboRole.Name = "cboRole";
            cboRole.Size = new Size(125, 28);
            cboRole.TabIndex = 5;
            // 
            // chkEnabled
            // 
            chkEnabled.AutoSize = true;
            chkEnabled.Checked = true;
            chkEnabled.CheckState = CheckState.Checked;
            chkEnabled.Font = new Font("微软雅黑", 10F);
            chkEnabled.Location = new Point(742, 17);
            chkEnabled.Name = "chkEnabled";
            chkEnabled.Size = new Size(89, 27);
            chkEnabled.TabIndex = 6;
            chkEnabled.Text = "启用账号";
            chkEnabled.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(43, 50, 120);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(20, 63);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(88, 32);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "新增";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(43, 50, 120);
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(120, 63);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(88, 32);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "修改";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(201, 72, 72);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(220, 63);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(88, 32);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "删除";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(110, 118, 150);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(320, 63);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(88, 32);
            btnClear.TabIndex = 10;
            btnClear.Text = "清空";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 240, 252);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.AutoGenerateColumns = false;
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
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { colId, colUserName, colRoleName, colIsEnabled, colCreatedAt, colUpdatedAt });
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
            dataGridView1.Location = new Point(0, 170);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1160, 480);
            dataGridView1.TabIndex = 2;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.FillWeight = 45F;
            colId.HeaderText = "编号";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colUserName
            // 
            colUserName.DataPropertyName = "UserName";
            colUserName.HeaderText = "账号";
            colUserName.MinimumWidth = 6;
            colUserName.Name = "colUserName";
            colUserName.ReadOnly = true;
            colUserName.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colRoleName
            // 
            colRoleName.DataPropertyName = "RoleName";
            colRoleName.HeaderText = "角色";
            colRoleName.MinimumWidth = 6;
            colRoleName.Name = "colRoleName";
            colRoleName.ReadOnly = true;
            colRoleName.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colIsEnabled
            // 
            colIsEnabled.DataPropertyName = "IsEnabled";
            colIsEnabled.FillWeight = 75F;
            colIsEnabled.HeaderText = "已启用";
            colIsEnabled.MinimumWidth = 6;
            colIsEnabled.Name = "colIsEnabled";
            colIsEnabled.ReadOnly = true;
            // 
            // colCreatedAt
            // 
            colCreatedAt.DataPropertyName = "CreatedAt";
            colCreatedAt.HeaderText = "创建时间";
            colCreatedAt.MinimumWidth = 6;
            colCreatedAt.Name = "colCreatedAt";
            colCreatedAt.ReadOnly = true;
            colCreatedAt.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colUpdatedAt
            // 
            colUpdatedAt.DataPropertyName = "UpdatedAt";
            colUpdatedAt.HeaderText = "修改时间";
            colUpdatedAt.MinimumWidth = 6;
            colUpdatedAt.Name = "colUpdatedAt";
            colUpdatedAt.ReadOnly = true;
            colUpdatedAt.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmUserManage
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(56, 59, 138);
            ClientSize = new Size(1160, 650);
            Controls.Add(dataGridView1);
            Controls.Add(pnlEditor);
            Controls.Add(pnlFilter);
            Font = new Font("微软雅黑", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmUserManage";
            Text = "用户管理";
            Load += FrmUserManage_Load;
            pnlFilter.ResumeLayout(false);
            pnlFilter.PerformLayout();
            pnlEditor.ResumeLayout(false);
            pnlEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlFilter;
        private Label lblKeyword;
        private TextBox txtKeyword;
        private Button btnSearch;
        private Panel pnlEditor;
        private Label lblUserName;
        private TextBox txtUserName;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblRole;
        private ComboBox cboRole;
        private CheckBox chkEnabled;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colUserName;
        private DataGridViewTextBoxColumn colRoleName;
        private DataGridViewCheckBoxColumn colIsEnabled;
        private DataGridViewTextBoxColumn colCreatedAt;
        private DataGridViewTextBoxColumn colUpdatedAt;
    }
}
