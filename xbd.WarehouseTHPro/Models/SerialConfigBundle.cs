namespace xbd.WarehouseTHPro.Models
{
    /// <summary>
    /// 串口配置容器（对应文件 serialPort_config.json，A区+B区两份）
    /// </summary>
    public class SerialConfigBundle
    {
        public SerialPortParams SerialA { get; set; } = new SerialPortParams();
        public SerialPortParams SerialB { get; set; } = new SerialPortParams();
    }
}
