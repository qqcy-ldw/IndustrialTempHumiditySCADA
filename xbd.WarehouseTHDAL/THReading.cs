namespace xbd.WarehouseTHDAL
{
    public sealed class THReading
    {
        public long Id { get; set; }
        public DateTime RecordedAt { get; set; }
        public string DeviceName { get; set; } = "";
        public string ZoneName { get; set; } = "";
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public bool IsAvailable { get; set; }
    }
}
