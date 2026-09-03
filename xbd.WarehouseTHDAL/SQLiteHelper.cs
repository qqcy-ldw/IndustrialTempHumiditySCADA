using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace xbd.WarehouseTHDAL
{
    /// <summary>
    /// SQLite 通用数据访问助手（Dapper 风格）
    /// 数据库文件：运行目录 Data\warehouse_th.db（与 THReadingRepository 同一份库）
    /// </summary>
    public class SQLiteHelper
    {
        /// <summary>
        /// 连接字符串（库文件不存在时自动创建）
        /// </summary>
        public static string ConnString { get; set; } = new SqliteConnectionStringBuilder
        {
            DataSource = Path.Combine(AppContext.BaseDirectory, "Data", "warehouse_th.db"),
            Mode = SqliteOpenMode.ReadWriteCreate
        }.ToString();

        /// <summary>
        /// 执行增删改，返回受影响行数
        /// </summary>
        public static int ExecuteNonQuery(string sql, object? param = null)
        {
            using var conn = new SqliteConnection(ConnString);
            return conn.Execute(sql, param);
        }

        /// <summary>
        /// 带事务的批量执行：同一批要么全部成功，要么全部回滚（适合批量插入历史数据）
        /// </summary>
        public static int ExecuteNonQueryWithTransaction<T>(string sql, IEnumerable<T> rows)
        {
            using var conn = new SqliteConnection(ConnString);
            conn.Open();
            using var tx = conn.BeginTransaction();
            int count = conn.Execute(sql, rows, tx);   // Dapper 把列表展开成逐行执行
            tx.Commit();
            return count;
        }

        /// <summary>
        /// 返回首行首列
        /// </summary>
        public static object? ExecuteScalar(string sql, object? param = null)
        {
            using var conn = new SqliteConnection(ConnString);
            return conn.ExecuteScalar(sql, param);
        }

        /// <summary>
        /// 查询多行，返回强类型列表
        /// </summary>
        public static List<T> Query<T>(string sql, object? param = null)
        {
            using var conn = new SqliteConnection(ConnString);
            return conn.Query<T>(sql, param).AsList();
        }

        /// <summary>
        /// 查询单个对象（找不到返回 null）
        /// </summary>
        public static T? QueryFirst<T>(string sql, object? param = null)
        {
            using var conn = new SqliteConnection(ConnString);
            return conn.QueryFirstOrDefault<T>(sql, param);
        }
    }
}
