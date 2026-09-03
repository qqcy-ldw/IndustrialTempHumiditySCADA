using System;
using System.Collections.Generic;
using System.IO;
using Dapper;

namespace xbd.WarehouseTHDAL
{
    public sealed class THReadingRepository
    {
        /// <summary>
        /// 数据库文件路径（运行目录 Data\warehouse_th.db）
        /// </summary>
        public static string DatabasePath => Path.Combine(AppContext.BaseDirectory, "Data", "warehouse_th.db");

        /// <summary>
        /// 初始化：建目录 + 建表 + 建索引（表不存在才创建，可重复调用）
        /// </summary>
        public void Initialize()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(DatabasePath)!);

            SQLiteHelper.ExecuteNonQuery(@"
CREATE TABLE IF NOT EXISTS th_readings (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    recorded_at TEXT NOT NULL,
    device_name TEXT NOT NULL,
    zone_name TEXT NOT NULL,
    temperature REAL,
    humidity REAL,
    is_available INTEGER NOT NULL DEFAULT 1
);
CREATE INDEX IF NOT EXISTS idx_th_readings_time ON th_readings(recorded_at);
CREATE INDEX IF NOT EXISTS idx_th_readings_zone_time ON th_readings(zone_name, recorded_at);");
        }

        /// <summary>
        /// 批量插入历史记录（带事务：一批要么全成功，要么全回滚）
        /// </summary>
        public void InsertMany(IEnumerable<THReading> readings)
        {
            var rows = readings.ToList();
            if (rows.Count == 0) return;

            SQLiteHelper.ExecuteNonQueryWithTransaction(@"INSERT INTO th_readings
            (recorded_at, device_name, zone_name, temperature, humidity, is_available)
            VALUES (@RecordedAt, @DeviceName, @ZoneName, @Temperature, @Humidity, @IsAvailable);", rows);
        }

        /// <summary>
        /// 按时间范围（+ 可选区域）查询历史记录
        /// </summary>
        public IReadOnlyList<THReading> Query(DateTime from, DateTime to, string? zoneName = null)
        {
            // datetime() 兼容 SQLite 默认的空格分隔格式以及历史数据中的 ISO-T 格式
            string sql = @"SELECT id Id, recorded_at RecordedAt,
            device_name DeviceName, zone_name ZoneName, temperature Temperature,
            humidity Humidity, is_available IsAvailable
            FROM th_readings
            WHERE datetime(recorded_at) >= datetime(@From) AND datetime(recorded_at) <= datetime(@To)";

            var parameters = new DynamicParameters();
            parameters.Add("From", from.ToString("yyyy-MM-dd HH:mm:ss"));
            parameters.Add("To", to.ToString("yyyy-MM-dd HH:mm:ss"));

            // 全部区域和指定区域分开组装条件，避免 NULL 参数在不同 SQLite 驱动版本中的差异。
            if (!string.IsNullOrWhiteSpace(zoneName))
            {
                sql += " AND TRIM(zone_name) = TRIM(@ZoneName)";
                parameters.Add("ZoneName", zoneName.Trim());
            }

            sql += " ORDER BY recorded_at, zone_name;";
            return SQLiteHelper.Query<THReading>(sql, parameters);
        }
    }
}
