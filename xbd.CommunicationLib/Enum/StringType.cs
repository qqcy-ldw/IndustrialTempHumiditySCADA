namespace modbus.CommunicationLibl.Enum;

/// <summary>
/// 字符串编码类型
/// </summary>
public enum StringType
{
    /// <summary>
    /// ASCII编码字符串（每个字节直接映射为ASCII字符）
    /// 例：0x41 0x42 → "AB"
    /// </summary>
    ASCIIString,

    /// <summary>
    /// 十六进制字符串（每个字节转成两位十六进制字符）
    /// 例：0x0A 0x1F → "0A1F"
    /// </summary>
    HexString,

    /// <summary>
    /// 十进制字符串（每个字节转成十进制数值再拼接）
    /// 例：0x0A → "10"
    /// </summary>
    DecString,

    /// <summary>
    /// BitConvert方式字节翻转后转字符串
    /// </summary>
    BitConvertString,

    /// <summary>
    /// 西门子字符串格式（首字节为有效长度，后续为ASCII字符）
    /// 例：0x05 0x48 0x45 0x4C 0x4C 0x4F → "HELLO"
    /// </summary>
    SiemensString,
}
