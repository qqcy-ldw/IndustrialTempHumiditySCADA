namespace xbd.WarehouseTHPro.Models
{
    /// <summary>
    /// 单个分区的温湿度报警阈值（A01~B03 共6个分区共用）
    /// </summary>
    public class ZoneThreshold
    {
        /// <summary>分区名，如 A01、B03</summary>
        public string ZoneName { get; set; } = "";

        /// <summary>温度高限（℃），超过触发高温报警</summary>
        public double TempHigh { get; set; } = 30.0;

        /// <summary>温度低限（℃），低于触发低温报警</summary>
        public double TempLow { get; set; } = 10.0;

        /// <summary>湿度高限（%RH），超过触发高湿报警</summary>
        public double HumHigh { get; set; } = 80.0;

        /// <summary>湿度低限（%RH），低于触发低湿报警</summary>
        public double HumLow { get; set; } = 30.0;
    }
}
