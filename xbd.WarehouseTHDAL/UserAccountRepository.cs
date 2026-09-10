using System.Security.Cryptography;
using System.Text;

namespace xbd.WarehouseTHDAL
{
    /// <summary>
    /// 系统用户数据访问类。
    /// </summary>
    public sealed class UserAccountRepository
    {
        /// <summary>
        /// 创建用户表，并保证系统至少存在一个管理员账号。
        /// 初始账号：admin；初始密码：123456。
        /// </summary>
        public void Initialize()
        {
            Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, "Data"));

            SQLiteHelper.ExecuteNonQuery(@"
CREATE TABLE IF NOT EXISTS sys_users (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    user_name TEXT NOT NULL COLLATE NOCASE UNIQUE,
    password_hash TEXT NOT NULL,
    role_name TEXT NOT NULL,
    is_enabled INTEGER NOT NULL DEFAULT 1,
    created_at TEXT NOT NULL,
    updated_at TEXT NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_sys_users_name ON sys_users(user_name);");

            DateTime now = DateTime.Now;
            SQLiteHelper.ExecuteNonQuery(@"
INSERT OR IGNORE INTO sys_users
(user_name, password_hash, role_name, is_enabled, created_at, updated_at)
VALUES (@UserName, @PasswordHash, @RoleName, 1, @CreatedAt, @UpdatedAt);",
                new
                {
                    UserName = "admin",
                    PasswordHash = HashPassword("123456"),
                    RoleName = "管理员",
                    CreatedAt = now.ToString("yyyy-MM-dd HH:mm:ss"),
                    UpdatedAt = now.ToString("yyyy-MM-dd HH:mm:ss")
                });
        }

        /// <summary>
        /// 按账号关键字查询用户；关键字为空时返回全部用户。
        /// </summary>
        public List<UserAccount> Query(string? keyword)
        {
            string sql = @"
SELECT id Id, user_name UserName, password_hash PasswordHash,
       role_name RoleName, is_enabled IsEnabled,
       created_at CreatedAt, updated_at UpdatedAt
FROM sys_users
WHERE @Keyword = '' OR user_name LIKE @Keyword
ORDER BY id;";

            string searchText = string.IsNullOrWhiteSpace(keyword) ? "" : $"%{keyword.Trim()}%";
            return SQLiteHelper.Query<UserAccount>(sql, new { Keyword = searchText });
        }

        /// <summary>
        /// 验证登录账号和密码。
        /// 只有启用状态的账号可以登录，验证失败时返回 null。
        /// </summary>
        public UserAccount? ValidateLogin(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            UserAccount? user = SQLiteHelper.QueryFirst<UserAccount>(@"
SELECT id Id, user_name UserName, password_hash PasswordHash,
       role_name RoleName, is_enabled IsEnabled,
       created_at CreatedAt, updated_at UpdatedAt
FROM sys_users
WHERE user_name = @UserName AND is_enabled = 1
LIMIT 1;",
                new { UserName = userName.Trim() });

            if (user is null)
            {
                return null;
            }

            string passwordHash = HashPassword(password);
            return string.Equals(user.PasswordHash, passwordHash, StringComparison.OrdinalIgnoreCase)
                ? user
                : null;
        }

        /// <summary>
        /// 新增用户。
        /// </summary>
        public void Insert(UserAccount user, string password)
        {
            DateTime now = DateTime.Now;
            SQLiteHelper.ExecuteNonQuery(@"
INSERT INTO sys_users
(user_name, password_hash, role_name, is_enabled, created_at, updated_at)
VALUES (@UserName, @PasswordHash, @RoleName, @IsEnabled, @CreatedAt, @UpdatedAt);",
                new
                {
                    user.UserName,
                    PasswordHash = HashPassword(password),
                    user.RoleName,
                    user.IsEnabled,
                    CreatedAt = now.ToString("yyyy-MM-dd HH:mm:ss"),
                    UpdatedAt = now.ToString("yyyy-MM-dd HH:mm:ss")
                });
        }

        /// <summary>
        /// 修改用户。密码为空时保留原密码。
        /// </summary>
        public void Update(UserAccount user, string? password)
        {
            DateTime now = DateTime.Now;
            string updatedAt = now.ToString("yyyy-MM-dd HH:mm:ss");

            if (string.IsNullOrWhiteSpace(password))
            {
                SQLiteHelper.ExecuteNonQuery(@"
UPDATE sys_users
SET user_name = @UserName, role_name = @RoleName,
    is_enabled = @IsEnabled, updated_at = @UpdatedAt
WHERE id = @Id;",
                    new { user.Id, user.UserName, user.RoleName, user.IsEnabled, UpdatedAt = updatedAt });
                return;
            }

            SQLiteHelper.ExecuteNonQuery(@"
UPDATE sys_users
SET user_name = @UserName, password_hash = @PasswordHash,
    role_name = @RoleName, is_enabled = @IsEnabled, updated_at = @UpdatedAt
WHERE id = @Id;",
                new
                {
                    user.Id,
                    user.UserName,
                    PasswordHash = HashPassword(password),
                    user.RoleName,
                    user.IsEnabled,
                    UpdatedAt = updatedAt
                });
        }

        /// <summary>
        /// 删除指定用户。
        /// </summary>
        public void Delete(long id)
        {
            SQLiteHelper.ExecuteNonQuery("DELETE FROM sys_users WHERE id = @Id;", new { Id = id });
        }

        /// <summary>
        /// 判断账号是否已存在。修改时传入自身编号可排除当前记录。
        /// </summary>
        public bool ExistsUserName(string userName, long? excludeId = null)
        {
            object? value = SQLiteHelper.ExecuteScalar(@"
SELECT COUNT(1) FROM sys_users
WHERE user_name = @UserName AND (@ExcludeId IS NULL OR id <> @ExcludeId);",
                new { UserName = userName.Trim(), ExcludeId = excludeId });

            return Convert.ToInt32(value) > 0;
        }

        /// <summary>
        /// 将明文密码转换为 SHA-256 摘要，用于保存或后续登录校验。
        /// </summary>
        public static string HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = SHA256.HashData(passwordBytes);
            return Convert.ToHexString(hashBytes);
        }
    }
}
