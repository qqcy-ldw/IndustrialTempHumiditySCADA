using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modbus.CommunicationLibl.Helper
{
    public class CRCHelper
    {
        /// <summary>
        /// 计算 Modbus RTU CRC-16
        /// </summary>
        /// <param name="data">要计算的数据</param>
        /// <param name="offset">起始索引</param>
        /// <param name="length">长度</param>
        /// <returns>2 字节 CRC，低字节在前(返回的是小端序)</returns>
        public static byte[] Calculate(byte[] data, int offset = 0, int length = -1)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentException("数据不能为空");

            if (length < 0)
                length = data.Length - offset;

            ushort crc = 0xFFFF;
            for (int i = offset; i < offset + length; i++)
            {
                crc ^= data[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;  // 多项式 0x8005 的反转
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }

            // 返回低字节在前（小端序）
            return new byte[2] { (byte)(crc & 0xFF), (byte)(crc >> 8) };
        }

        /// <summary>
        /// 计算 CRC 并追加到字节数组末尾，返回新数组
        /// </summary>
        /// <param name="data">原始数据（不包含 CRC）</param>
        /// <returns>原始数据 + 2 字节 CRC 的新数组</returns>
        public static byte[] Append(byte[] data)
        {
            byte[] crc = Calculate(data);
            byte[] result = new byte[data.Length + 2];
            Array.Copy(data, 0, result, 0, data.Length);
            result[data.Length] = crc[0];     // CRC 低字节
            result[data.Length + 1] = crc[1]; // CRC 高字节
            return result;
        }

        /// <summary>
        /// 校验接收到的帧的 CRC 是否正确
        /// </summary>
        /// <param name="frame">完整帧（包含最后两字节 CRC）</param>
        /// <returns>true: 校验通过; false: 校验失败</returns>
        public static bool Verify(byte[] frame)
        {
            if (frame == null || frame.Length < 3)
                return false;

            // 取帧的数据部分（不含最后2字节CRC），重新计算CRC
            int dataLen = frame.Length - 2;
            byte[] computedCrc = Calculate(frame, 0, dataLen);

            // 比较计算出的CRC与帧中的CRC
            return computedCrc[0] == frame[dataLen] && computedCrc[1] == frame[dataLen + 1];
        }
    }
}
