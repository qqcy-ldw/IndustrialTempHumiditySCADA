namespace xbd.WarehouseTHDAL
{
    /// <summary>
    /// 系统用户。
    /// PasswordHash 只用于数据库保存，界面列表不显示该字段。
    /// </summary>
    public sealed class UserAccount
    {
        public long Id { get; set; }
        public string UserName { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public string RoleName { get; set; } = "操作员";
        public bool IsEnabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
