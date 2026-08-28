using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using thinger.DataConvertLib;

namespace xbd.WarehouseTHUtils
{
    public static class ConfigHelper
    {
        /// <summary>
        /// 串口参数配置文件路径
        /// </summary>
        public static readonly string PlcCommConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "serialPort_config.json");
        /// <summary>
        /// 阈值配置配置文件路径
        /// </summary>
        public static readonly string SystemConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "threshold_config.json");

        /// <summary>
        /// 从 JSON 文件加载 PLC 配置
        /// </summary>
        /// <returns>PLC 配置实体，文件不存在则返回默认配置</returns>
        public static OperateResult<T> Load<T>(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return OperateResult.CreateFailResult<T>("配置文件不存在");
                }
                string json = File.ReadAllText(path);
                var config = JsonSerializer.Deserialize<T>(json);
                if (config == null) return OperateResult.CreateFailResult<T>("配置文件内容为空或格式错误");
                return OperateResult.CreateSuccessResult(config);  // 返回读取到的配置对象
            }
            catch (Exception e)
            {
                return OperateResult.CreateFailResult<T>(e.Message);
            }
        }

        /// <summary>
        /// 保存 PLC 配置到 JSON 文件
        /// </summary>
        /// <param name="config">PLC 配置实体</param>
        /// <param name="path">保存路径</param>
        /// <returns>操作结果</returns>
        public static OperateResult Save<T>(T config, string path)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                if (config != null)
                {
                    var json = JsonSerializer.Serialize(config, options);
                    File.WriteAllText(path, json);
                }
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception e)
            {
                return OperateResult.CreateFailResult(e.Message);
            }
        }
    }
}
