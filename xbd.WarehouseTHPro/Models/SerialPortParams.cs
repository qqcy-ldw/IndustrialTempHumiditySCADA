namespace xbd.WarehouseTHPro.Models
{
    /// <summary>
    /// 单个串口的连接参数（A区/B区共用）
    /// </summary>
    public class SerialPortParams
    {
        /// <summary>端口号，如 COM1、COM3</summary>
        public string PortName { get; set; } = "";

        /// <summary>波特率，如 9600、19200、115200</summary>
        public int BaudRate { get; set; } = 9600;

        /// <summary>校验位：None/Even/Odd/Mark/Space</summary>
        public string Parity { get; set; } = "None";

        /// <summary>数据位：5~8</summary>
        public int DataBits { get; set; } = 8;

        /// <summary>停止位：None/One/OnePointFive/Two</summary>
        public string StopBits { get; set; } = "One";
    }
}
