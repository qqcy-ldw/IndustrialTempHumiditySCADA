namespace xbd.WarehouseTHPro.Models
{
    /// <summary>
    /// 阈值配置容器（对应文件 threshold_config.json，A01~B03 共6个分区）
    /// </summary>
    public class ThresholdConfigBundle
    {
        public ZoneThreshold ZoneA01 { get; set; } = new ZoneThreshold { ZoneName = "A01" };
        public ZoneThreshold ZoneA02 { get; set; } = new ZoneThreshold { ZoneName = "A02" };
        public ZoneThreshold ZoneA03 { get; set; } = new ZoneThreshold { ZoneName = "A03" };
        public ZoneThreshold ZoneB01 { get; set; } = new ZoneThreshold { ZoneName = "B01" };
        public ZoneThreshold ZoneB02 { get; set; } = new ZoneThreshold { ZoneName = "B02" };
        public ZoneThreshold ZoneB03 { get; set; } = new ZoneThreshold { ZoneName = "B03" };
    }
}
