using System.Collections.Generic;

namespace xbd.WarehouseTHPro.Models
{
    /// <summary>
    /// 参数配置总实体（对应 FrmParamConfig 界面上的全部输入项）
    /// </summary>
    public class SystemConfig
    {
        /* ================================================================
         * 串口参数区（对应右 panel3 的 txtA* / txtB* 共10个 TextBox）
         * ================================================================ */

        /// <summary>A区串口参数（txtAPort / txtABaud / txtAParity / txtADataBits / txtAStopBits）</summary>
        public SerialPortParams SerialA { get; set; } = new SerialPortParams();

        /// <summary>B区串口参数（txtBPort / txtBBaud / txtBParity / txtBDataBits / txtBStopBits）</summary>
        public SerialPortParams SerialB { get; set; } = new SerialPortParams();

        /* ================================================================
         * 温湿度报警阈值（对应左 panel1 的 txtTemp* + 中 panel2 的 txtHum* 共24个 TextBox）
         * 顺序：A01、A02、A03、B01、B02、B03
         * ================================================================ */

        /// <summary>A01分区阈值</summary>
        public ZoneThreshold ZoneA01 { get; set; } = new ZoneThreshold { ZoneName = "A01" };

        /// <summary>A02分区阈值</summary>
        public ZoneThreshold ZoneA02 { get; set; } = new ZoneThreshold { ZoneName = "A02" };

        /// <summary>A03分区阈值</summary>
        public ZoneThreshold ZoneA03 { get; set; } = new ZoneThreshold { ZoneName = "A03" };

        /// <summary>B01分区阈值</summary>
        public ZoneThreshold ZoneB01 { get; set; } = new ZoneThreshold { ZoneName = "B01" };

        /// <summary>B02分区阈值</summary>
        public ZoneThreshold ZoneB02 { get; set; } = new ZoneThreshold { ZoneName = "B02" };

        /// <summary>B03分区阈值</summary>
        public ZoneThreshold ZoneB03 { get; set; } = new ZoneThreshold { ZoneName = "B03" };

        /// <summary>
        /// 根据分区名获取阈值对象（key：A01/A02/A03/B01/B02/B03）
        /// </summary>
        public ZoneThreshold? GetZone(string zoneName)
        {
            if (string.IsNullOrWhiteSpace(zoneName)) return null;
            return zoneName switch
            {
                "A01" => ZoneA01,
                "A02" => ZoneA02,
                "A03" => ZoneA03,
                "B01" => ZoneB01,
                "B02" => ZoneB02,
                "B03" => ZoneB03,
                _ => null
            };
        }

        /// <summary>
        /// 遍历全部6个分区阈值的集合（方便批量读写/校验）
        /// </summary>
        public IEnumerable<ZoneThreshold> AllZones
        {
            get
            {
                yield return ZoneA01;
                yield return ZoneA02;
                yield return ZoneA03;
                yield return ZoneB01;
                yield return ZoneB02;
                yield return ZoneB03;
            }
        }
    }
}
