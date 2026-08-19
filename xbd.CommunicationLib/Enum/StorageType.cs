namespace modbus.CommunicationLibl.Enum;

/// <summary>
/// 寄存器存储最小单位（地址步进）
/// </summary>
public enum StorageType
{
    /// <summary>
    /// 字节寻址，1个地址=1字节（如西门子S7）
    /// </summary>
    Byte = 1,

    /// <summary>
    /// 字寻址，1个地址=2字节（如Modbus）
    /// </summary>
    Word = 2,
}