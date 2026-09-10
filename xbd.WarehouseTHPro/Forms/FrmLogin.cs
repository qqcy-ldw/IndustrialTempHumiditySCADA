using System.ComponentModel;
using xbd.WarehouseTHDAL;

namespace xbd.WarehouseTHPro
{
    /// <summary>
    /// 系统登录窗体。
    /// </summary>
    public partial class FrmLogin : Form
    {
        private readonly UserAccountRepository _repository = new();

        /// <summary>
        /// 登录成功的用户信息，供主窗体显示当前登录状态。
        /// </summary>
        public UserAccount? LoginUser { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();

            // 设计器只加载控件外观，不能访问 SQLite 数据库。
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            _repository.Initialize();
        }

        /// <summary>
        /// 窗体加载后将输入焦点放到账号输入框。
        /// </summary>
        private void FrmLogin_Load(object? sender, EventArgs e)
        {
            txtUserName.Focus();
        }

        /// <summary>
        /// 验证账号密码，成功后关闭登录窗体并返回登录用户。
        /// </summary>
        private void btnLogin_Click(object? sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("请输入账号和密码。", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            UserAccount? loginUser = _repository.ValidateLogin(userName, password);
            if (loginUser is null)
            {
                MessageBox.Show("账号、密码错误，或该账号已被停用。", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.SelectAll();
                txtPassword.Focus();
                return;
            }

            LoginUser = loginUser;
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 取消登录并退出程序。
        /// </summary>
        private void btnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
