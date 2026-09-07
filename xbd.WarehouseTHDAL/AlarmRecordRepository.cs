using Dapper;

namespace xbd.WarehouseTHDAL
{
    /// <summary>
    /// 报警记录数据访问类。
    /// 报警只在状态变化时写入，不会按采集周期重复插入。
    /// </summary>
    public sealed class AlarmRecordRepository
    {
        public void Initialize()
        {
            Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, "Data"));
            SQLiteHelper.ExecuteNonQuery(@"
CREATE TABLE IF NOT EXISTS th_alarm_records (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    occurred_at TEXT NOT NULL,
    recovered_at TEXT NULL,
    device_name TEXT NOT NULL,
    zone_name TEXT NOT NULL,
    variable_name TEXT NOT NULL,
    alarm_type TEXT NOT NULL,
    current_value REAL NULL,
    limit_value REAL NULL,
    alarm_note TEXT NULL,
    is_active INTEGER NOT NULL DEFAULT 1
);
CREATE INDEX IF NOT EXISTS idx_alarm_time ON th_alarm_records(occurred_at);
CREATE INDEX IF NOT EXISTS idx_alarm_active ON th_alarm_records(device_name, variable_name, alarm_type, is_active);");
        }

        public void InsertTriggered(AlarmRecord alarm)
        {
            string sql = @"INSERT INTO th_alarm_records
(occurred_at, device_name, zone_name, variable_name, alarm_type, current_value, limit_value, alarm_note, is_active)
VALUES (@OccurredAt, @DeviceName, @ZoneName, @VariableName, @AlarmType, @CurrentValue, @LimitValue, @AlarmNote, 1);";

            SQLiteHelper.ExecuteNonQuery(sql, alarm);
        }

        public void MarkRecovered(AlarmRecord alarm)
        {
            string sql = @"UPDATE th_alarm_records
SET recovered_at = @RecoveredAt, is_active = 0, current_value = @CurrentValue
WHERE id = (
    SELECT id FROM th_alarm_records
    WHERE device_name = @DeviceName AND variable_name = @VariableName
      AND alarm_type = @AlarmType AND is_active = 1
    ORDER BY occurred_at DESC, id DESC LIMIT 1
);";

            var parameters = new DynamicParameters(alarm);
            parameters.Add("RecoveredAt", alarm.RecoveredAt?.ToString("yyyy-MM-dd HH:mm:ss"));
            SQLiteHelper.ExecuteNonQuery(sql, parameters);
        }

        public IReadOnlyList<AlarmRecord> Query(DateTime from, DateTime to, string? zoneName, bool? isActive)
        {
            string sql = @"SELECT id Id, occurred_at OccurredAt, recovered_at RecoveredAt,
device_name DeviceName, zone_name ZoneName, variable_name VariableName,
alarm_type AlarmType, current_value CurrentValue, limit_value LimitValue,
alarm_note AlarmNote, is_active IsActive
FROM th_alarm_records
WHERE datetime(occurred_at) >= datetime(@From) AND datetime(occurred_at) <= datetime(@To)";

            var parameters = new DynamicParameters();
            parameters.Add("From", from.ToString("yyyy-MM-dd HH:mm:ss"));
            parameters.Add("To", to.ToString("yyyy-MM-dd HH:mm:ss"));

            if (!string.IsNullOrWhiteSpace(zoneName))
            {
                sql += " AND TRIM(zone_name) = TRIM(@ZoneName)";
                parameters.Add("ZoneName", zoneName.Trim());
            }

            if (isActive.HasValue)
            {
                sql += " AND is_active = @IsActive";
                parameters.Add("IsActive", isActive.Value ? 1 : 0);
            }

            sql += " ORDER BY occurred_at DESC, id DESC;";
            return SQLiteHelper.Query<AlarmRecord>(sql, parameters);
        }

    }
}
