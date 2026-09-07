using System.ComponentModel;
using xbd.WarehouseTHDAL;

namespace xbd.WarehouseTHPro
{
    public partial class FrmUserManage : Form
    {
        private readonly UserAccountRepository _repository = new();
        private UserAccount? _selectedUser;

        public FrmUserManage()
        {
            InitializeComponent();

            // 设计器仅加载布局，运行程序时才访问 SQLite 数据库。
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            _repository.Initialize();
        }

        /// <summary>
        /// 窗体首次显示时查询全部用户。
        /// </summary>
        private void FrmUserManage_Load(object? sender, EventArgs e)
        {
            QueryUsers();
            ClearEditor();
        }

        /// <summary>
        /// 根据账号关键字刷新列表。
        /// </summary>
        private void btnSearch_Click(object? sender, EventArgs e)
        {
            QueryUsers();
        }

        /// <summary>
        /// 新增用户。
        /// </summary>
        private void btnAdd_Click(object? sender, EventArgs e)
        {
            if (!TryGetEditorUser(requirePassword: true, out UserAccount? user, out string password))
            {
                return;
            }

            if (_repository.ExistsUserName(user.UserName))
            {
                MessageBox.Show("该账号已存在，请重新输入。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _repository.Insert(user, password);
            QueryUsers();
            ClearEditor();
        }

        /// <summary>
        /// 修改当前选中的用户。密码文本框为空时，不修改原密码。
        /// </summary>
        private void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (_selectedUser is null)
            {
                MessageBox.Show("请先在列表中选择需要修改的用户。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!TryGetEditorUser(requirePassword: false, out UserAccount? user, out string password))
            {
                return;
            }

            if (_repository.ExistsUserName(user.UserName, _selectedUser.Id))
            {
                MessageBox.Show("该账号已存在，请重新输入。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            user.Id = _selectedUser.Id;
            _repository.Update(user, password);
            QueryUsers();
            ClearEditor();
        }

        /// <summary>
        /// 删除当前选中的用户。
        /// </summary>
        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (_selectedUser is null)
            {
                MessageBox.Show("请先在列表中选择需要删除的用户。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"确定删除账号“{_selectedUser.UserName}”吗？",
                "删除确认",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                return;
            }

            _repository.Delete(_selectedUser.Id);
            QueryUsers();
            ClearEditor();
        }

        /// <summary>
        /// 清空编辑区，准备新增用户。
        /// </summary>
        private void btnClear_Click(object? sender, EventArgs e)
        {
            ClearEditor();
        }

        /// <summary>
        /// 选中表格行后，将该用户资料带入编辑区。
        /// </summary>
        private void dataGridView1_SelectionChanged(object? sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is not UserAccount user)
            {
                return;
            }

            _selectedUser = user;
            txtUserName.Text = user.UserName;
            txtPassword.Clear();
            cboRole.Text = user.RoleName;
            chkEnabled.Checked = user.IsEnabled;
        }

        /// <summary>
        /// 回车直接执行账号查询。
        /// </summary>
        private void txtKeyword_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            QueryUsers();
            e.SuppressKeyPress = true;
        }

        /// <summary>
        /// 从输入控件读取用户资料，并完成基础校验。
        /// </summary>
        private bool TryGetEditorUser(bool requirePassword, out UserAccount user, out string password)
        {
            user = new UserAccount();
            password = txtPassword.Text;
            string userName = txtUserName.Text.Trim();

            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show("请输入账号。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (userName.Length > 30)
            {
                MessageBox.Show("账号长度不能超过 30 个字符。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (requirePassword && string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("新增用户必须设置密码。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(password) && password.Length < 6)
            {
                MessageBox.Show("密码长度不能少于 6 个字符。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            user.UserName = userName;
            user.RoleName = cboRole.Text;
            user.IsEnabled = chkEnabled.Checked;
            return true;
        }

        /// <summary>
        /// 查询并绑定用户列表。
        /// </summary>
        private void QueryUsers()
        {
            dataGridView1.DataSource = _repository.Query(txtKeyword.Text);
        }

        /// <summary>
        /// 恢复新增用户的初始输入状态。
        /// </summary>
        private void ClearEditor()
        {
            _selectedUser = null;
            dataGridView1.ClearSelection();
            txtUserName.Clear();
            txtPassword.Clear();
            cboRole.SelectedIndex = 1;
            chkEnabled.Checked = true;
            txtUserName.Focus();
        }
    }
}
