namespace xbd.WarehouseTHDAL
{
    /// <summary>
    /// 一条温湿度报警记录。
    /// </summary>
    public sealed class AlarmRecord
    {
        public long Id { get; set; }
        public DateTime OccurredAt { get; set; }
        public DateTime? RecoveredAt { get; set; }
        public string DeviceName { get; set; } = "";
        public string ZoneName { get; set; } = "";
        public string VariableName { get; set; } = "";
        public string AlarmType { get; set; } = "";
        public double? CurrentValue { get; set; }
        public double? LimitValue { get; set; }
        public string AlarmNote { get; set; } = "";
        public bool IsActive { get; set; }
    }
}
