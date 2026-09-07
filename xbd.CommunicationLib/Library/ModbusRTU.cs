using modbus.CommunicationLibl.Base;
using modbus.CommunicationLibl.Helper;
using modbus.CommunicationLibl.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thinger.DataConvertLib;

namespace modbus.CommunicationLibl.Library
{
    public class ModbusRTU : SerialDeviceBase, IModbusRW
    {
        /// <summary>
        /// 读取线圈状态
        /// </summary>
        public OperateResult<bool[]> ReadCoils(ushort start, ushort length, byte slaveId = 1)
        {
            // 1、拼接报文请求格式(从站地址 + 功能码 + 起始地址 + 读取数量 + CRC校验)
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);
            sendCommand.Add(0x01); // 功能码
            SplitUshortToBigEndian(start, out byte startHigh, out byte startLow);
            sendCommand.Add(startHigh);
            sendCommand.Add(startLow);
            SplitUshortToBigEndian(length, out byte lengthHigh, out byte lengthLow);
            sendCommand.Add(lengthHigh);
            sendCommand.Add(lengthLow);
            sendCommand.AddRange(CRCHelper.Calculate(sendCommand.ToArray()));

            // 2、发送报文并接收响应
            var result = SendAndReceive(sendCommand.ToArray());
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailResult<bool[]>(result.Message);
            }

            // 3、解析响应数据
            bool crcValid = CRCHelper.Verify(result.Content);
            if (!crcValid)
            {
                return OperateResult.CreateFailResult<bool[]>("CRC校验失败"+result.Content);
            }
            bool isModbusError = IsModbusErrorFrame(result.Content, isTcp: false, out byte realFuncCodem, out byte errorCode);
            if (isModbusError)
            {
                return OperateResult.CreateFailResult<bool[]>($"异常！原始功能码：0x{realFuncCodem:X2}，异常码：{errorCode}");
            }

            // 提取线圈状态数据
            int byteLength = length % 8 == 0 ? length / 8 : length / 8 + 1;
            byte[] coilData = result.Content.Skip(3).Take(byteLength).ToArray();
            // 截取有效的线圈状态位
            return OperateResult.CreateSuccessResult(
                BitLib.GetBitArrayFromByteArray(coilData, length)
                );
        }

        public async Task<OperateResult<bool[]>> ReadCoilsAsync(ushort start, ushort length, byte slaveId = 1)
        {
            throw new NotImplementedException();
        }

        public OperateResult<byte[]> ReadHoldingRegisters(ushort start, ushort length, byte slaveId = 1)
        {
            // 1、拼接报文请求格式(从站地址 + 功能码 + 起始地址 + 读取数量 + CRC校验)
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);
            sendCommand.Add(0x03); // 功能码
            SplitUshortToBigEndian(start, out byte startHigh, out byte startLow);
            sendCommand.Add(startHigh);
            sendCommand.Add(startLow);
            SplitUshortToBigEndian(length, out byte lengthHigh, out byte lengthLow);
            sendCommand.Add(lengthHigh);
            sendCommand.Add(lengthLow);
            sendCommand.AddRange(CRCHelper.Calculate(sendCommand.ToArray()));

            // 2、发送报文并接收响应
            var result = SendAndReceive(sendCommand.ToArray());
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailResult<byte[]>(result.Message);
            }

            // 3、解析响应数据
            bool crcValid = CRCHelper.Verify(result.Content);
            if (!crcValid)
            {
                return OperateResult.CreateFailResult<byte[]>("CRC校验失败" + result.Content);
            }
            bool isModbusError = IsModbusErrorFrame(result.Content, isTcp: false, out byte realFuncCodem, out byte errorCode);
            if (isModbusError)
            {
                return OperateResult.CreateFailResult<byte[]>($"异常！原始功能码：0x{realFuncCodem:X2}，异常码：{errorCode}");
            }

            // 提取寄存器数据
            int byteLength = length * 2;
            byte[] coilData = result.Content.Skip(3).Take(byteLength).ToArray();
            return OperateResult.CreateSuccessResult(coilData);
        }

        public Task<OperateResult<byte[]>> ReadHoldingRegistersAsync(ushort start, ushort length, byte slaveId = 1)
        {
            throw new NotImplementedException();
        }

        public OperateResult<byte[]> ReadInputRegisters(ushort start, ushort length, byte slaveId = 1)
        {
            // 1、拼接报文请求格式(从站地址 + 功能码 + 起始地址 + 读取数量 + CRC校验)
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);
            sendCommand.Add(0x04); // 功能码
            SplitUshortToBigEndian(start, out byte startHigh, out byte startLow);
            sendCommand.Add(startHigh);
            sendCommand.Add(startLow);
            SplitUshortToBigEndian(length, out byte lengthHigh, out byte lengthLow);
            sendCommand.Add(lengthHigh);
            sendCommand.Add(lengthLow);
            sendCommand.AddRange(CRCHelper.Calculate(sendCommand.ToArray()));

            // 2、发送报文并接收响应
            var result = SendAndReceive(sendCommand.ToArray());
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailResult<byte[]>(result.Message);
            }

            // 3、解析响应数据
            bool crcValid = CRCHelper.Verify(result.Content);
            if (!crcValid)
            {
                return OperateResult.CreateFailResult<byte[]>("CRC校验失败" + result.Content);
            }
            bool isModbusError = IsModbusErrorFrame(result.Content, isTcp: false, out byte realFuncCodem, out byte errorCode);
            if (isModbusError)
            {
                return OperateResult.CreateFailResult<byte[]>($"异常！原始功能码：0x{realFuncCodem:X2}，异常码：{errorCode}");
            }

            // 提取线圈状态数据
            int byteLength = length * 2;
            byte[] coilData = result.Content.Skip(3).Take(byteLength).ToArray();
            // 截取有效的线圈状态位
            return OperateResult.CreateSuccessResult(coilData);
        }

        public Task<OperateResult<byte[]>> ReadInputRegistersAsync(ushort start, ushort length, byte slaveId = 1)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 读取输入线圈
        /// </summary>
        public OperateResult<bool[]> ReadInputs(ushort start, ushort length, byte slaveId = 1)
        {
            // 1、拼接报文请求格式(从站地址 + 功能码 + 起始地址 + 读取数量 + CRC校验)
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);
            sendCommand.Add(0x02); // 功能码
            SplitUshortToBigEndian(start, out byte startHigh, out byte startLow);
            sendCommand.Add(startHigh);
            sendCommand.Add(startLow);
            SplitUshortToBigEndian(length, out byte lengthHigh, out byte lengthLow);
            sendCommand.Add(lengthHigh);
            sendCommand.Add(lengthLow);
            sendCommand.AddRange(CRCHelper.Calculate(sendCommand.ToArray()));

            // 2、发送报文并接收响应
            var result = SendAndReceive(sendCommand.ToArray());
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailResult<bool[]>(result.Message);
            }

            // 3、解析响应数据
            bool crcValid = CRCHelper.Verify(result.Content);
            if (!crcValid)
            {
                return OperateResult.CreateFailResult<bool[]>("CRC校验失败" + result.Content);
            }

            // 提取线圈状态数据
            int byteLength = length % 8 == 0 ? length / 8 : length / 8 + 1;
            byte[] coilData = result.Content.Skip(3).Take(byteLength).ToArray();
            // 截取有效的线圈状态位
            return OperateResult.CreateSuccessResult(
                BitLib.GetBitArrayFromByteArray(coilData, length)
                );
        }

        public Task<OperateResult<bool[]>> ReadInputsAsync(ushort start, ushort length, byte slaveId = 1)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 写入多个线圈
        /// </summary>
        public OperateResult WriteMultipleCoils(ushort start, bool[] values, byte slaveId = 1)
        {
            if (values == null || values.Length == 0)
                return OperateResult.CreateFailResult("线圈数组不能为空");

            // 1、拼接报文请求格式(从站地址 + 功能码 + 起始地址 + 写入数量 + 字节数 + 数据 + CRC校验)
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);
            sendCommand.Add(0x0F); // 功能码
            SplitUshortToBigEndian(start, out byte startHigh, out byte startLow);
            sendCommand.Add(startHigh);
            sendCommand.Add(startLow);
            SplitUshortToBigEndian((ushort)values.Length, out byte lengthHigh, out byte lengthLow);
            sendCommand.Add(lengthHigh);
            sendCommand.Add(lengthLow);
            int byteLength = values.Length % 8 == 0 ? values.Length / 8 : values.Length / 8 + 1;
            sendCommand.Add((byte)byteLength);
            sendCommand.AddRange(ByteArrayLib.GetByteArrayFromBoolArray(values));
            sendCommand.AddRange(CRCHelper.Calculate(sendCommand.ToArray()));
            // 发送并接受
            var result = SendAndReceive(sendCommand.ToArray());
            if (!result.IsSuccess)
                return OperateResult.CreateFailResult(result.Message);

            // 3. CRC 校验
            if (!CRCHelper.Verify(result.Content))
                return OperateResult.CreateFailResult("CRC校验失败");
            bool isModbusError = IsModbusErrorFrame(result.Content, isTcp: false, out byte realFuncCodem, out byte errorCode);
            if (isModbusError)
            {
                return OperateResult.CreateFailResult($"异常！原始功能码：0x{realFuncCodem:X2}，异常码：{errorCode}");
            }

            // 4. 解析响应帧并验证
            byte[] response = result.Content;

            // 响应长度至少为 8 字节（地址+功能码+地址高+地址低+数量高+数量低+CRC2）
            if (response.Length < 8)
                return OperateResult.CreateFailResult("响应帧长度不足");

            // 检查从站地址和功能码
            if (response[0] != slaveId || response[1] != 0x0F)
                return OperateResult.CreateFailResult("响应帧地址或功能码不匹配");

            // 提取响应的起始地址
            ushort respStart = (ushort)((response[2] << 8) | response[3]);
            // 提取响应的线圈数量
            ushort respQty = (ushort)((response[4] << 8) | response[5]);

            if (respStart != start || respQty != values.Length)
                return OperateResult.CreateFailResult($"响应地址/数量不匹配：期望 {start}/{values.Length}，实际 {respStart}/{respQty}");

            return OperateResult.CreateSuccessResult();
        }

        public Task<OperateResult> WriteMultipleCoilsAsync(ushort start, bool[] values, byte slaveId = 1)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 写入多个寄存器
        /// </summary>
        public OperateResult WriteMultipleRegisters(ushort start, ushort[] values, byte slaveId = 1)
        {
            if (values == null || values.Length == 0)
                return OperateResult.CreateFailResult("写入数据不能为空");

            // 1、拼接报文请求格式(从站地址 + 功能码 + 起始地址 + 寄存器数量 + 字节计数 + 数据 + CRC)
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);
            sendCommand.Add(0x10);
            SplitUshortToBigEndian(start, out byte startHigh, out byte startLow);
            sendCommand.Add(startHigh);
            sendCommand.Add(startLow);
            SplitUshortToBigEndian((ushort)values.Length, out byte lengthHigh, out byte lengthLow);
            sendCommand.Add(lengthHigh);
            sendCommand.Add(lengthLow);
            int byteLength = values.Length * 2;  // ✅ 每个寄存器 2 字节
            sendCommand.Add((byte)byteLength);
            sendCommand.AddRange(ByteArrayLib.GetByteArrayFromUShortArray(values));
            sendCommand.AddRange(CRCHelper.Calculate(sendCommand.ToArray()));

            // 2、发送并接收
            var result = SendAndReceive(sendCommand.ToArray());
            if (!result.IsSuccess)
                return OperateResult.CreateFailResult(result.Message);

            // 3、CRC 校验
            if (!CRCHelper.Verify(result.Content))
                return OperateResult.CreateFailResult("CRC校验失败");
            bool isModbusError = IsModbusErrorFrame(result.Content, isTcp: false, out byte realFuncCodem, out byte errorCode);
            if (isModbusError)
            {
                return OperateResult.CreateFailResult($"异常！原始功能码：0x{realFuncCodem:X2}，异常码：{errorCode}");
            }

            // 4、解析响应帧并验证
            byte[] response = result.Content;

            if (response.Length < 8)
                return OperateResult.CreateFailResult("响应帧长度不足");

            // ✅ 检查功能码是 0x10，不是 0x0F
            if (response[0] != slaveId || response[1] != 0x10)
                return OperateResult.CreateFailResult("响应帧地址或功能码不匹配");

            ushort respStart = (ushort)((response[2] << 8) | response[3]);
            ushort respQty = (ushort)((response[4] << 8) | response[5]);

            if (respStart != start || respQty != values.Length)
                return OperateResult.CreateFailResult($"响应地址/数量不匹配：期望 {start}/{values.Length}，实际 {respStart}/{respQty}");

            return OperateResult.CreateSuccessResult();
        }

        public Task<OperateResult> WriteMultipleRegistersAsync(ushort start, ushort[] values, byte slaveId = 1)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 写单个线圈
        /// </summary>
        public OperateResult WriteSingleCoil(ushort start, bool value, byte slaveId = 1)
        {
            // 1、拼接报文请求格式(从站地址 + 功能码 + 起始地址 + 写入值 + CRC校验)
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);
            sendCommand.Add(0x05); // 功能码
            SplitUshortToBigEndian(start, out byte startHigh, out byte startLow);
            sendCommand.Add(startHigh);
            sendCommand.Add(startLow);
            sendCommand.Add(value ? (byte)0xFF : (byte)0x00);
            sendCommand.Add(0x00);
            sendCommand.AddRange(CRCHelper.Calculate(sendCommand.ToArray()));

            // 2、发送报文并接收响应
            var result = SendAndReceive(sendCommand.ToArray());

            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailResult(result.Message);
            }
            // 3、验证报文是否正确
            if (CRCHelper.Verify(result.Content))
            {
                bool isModbusError = IsModbusErrorFrame(result.Content, isTcp: false, out byte realFuncCodem, out byte errorCode);
                if (isModbusError)
                {
                    return OperateResult.CreateFailResult<bool[]>($"异常！原始功能码：0x{realFuncCodem:X2}，异常码：{errorCode}");
                }
                // 4、验证响应是否与请求一致（回显校验），功能码 05 的响应帧必须是请求帧的完全拷贝
                if (result.Content.SequenceEqual(sendCommand.ToArray()))
                {
                    return OperateResult.CreateSuccessResult();
                }
                else
                {
                    return OperateResult.CreateFailResult("响应报文与请求报文不一致");
                }
            }
            else
            {
                return OperateResult.CreateFailResult("CRC校验失败" + result.Content);
            }
        }

        public Task<OperateResult> WriteSingleCoilAsync(ushort start, bool value, byte slaveId = 1)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 写单个寄存器
        /// </summary>
        public OperateResult WriteSingleRegister(ushort start,  ushort value, byte slaveId = 1)
        {
            // 1、拼接报文请求格式(从站地址 + 功能码 + 起始地址 + 写入值 + CRC校验)
            List<byte> sendCommand = new List<byte>();
            sendCommand.Add(slaveId);
            sendCommand.Add(0x06); // 功能码
            SplitUshortToBigEndian(start, out byte startHigh, out byte startLow);
            sendCommand.Add(startHigh);
            sendCommand.Add(startLow);
            SplitUshortToBigEndian(value, out byte valueHigh, out byte valueLow);
            sendCommand.Add(valueHigh);
            sendCommand.Add(valueLow);
            sendCommand.AddRange(CRCHelper.Calculate(sendCommand.ToArray()));

            // 2、发送报文并接收响应
            var result = SendAndReceive(sendCommand.ToArray());

            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailResult(result.Message);
            }
            // 3、验证报文是否正确
            if (CRCHelper.Verify(result.Content))
            {
                bool isModbusError = IsModbusErrorFrame(result.Content, isTcp: false, out byte realFuncCodem, out byte errorCode);
                if (isModbusError)
                {
                    return OperateResult.CreateFailResult<bool[]>($"异常！原始功能码：0x{realFuncCodem:X2}，异常码：{errorCode}");
                }
                // 4、验证响应是否与请求一致（回显校验），功能码 06 的响应帧必须是请求帧的完全拷贝
                if (result.Content.SequenceEqual(sendCommand.ToArray()))
                {
                    return OperateResult.CreateSuccessResult();
                }
                else
                {
                    return OperateResult.CreateFailResult("响应报文与请求报文不一致");
                }
            }
            else
            {
                return OperateResult.CreateFailResult("CRC校验失败" + result.Content);
            }
        }

        public Task<OperateResult> WriteSingleRegisterAsync(ushort start, ushort value, byte slaveId = 1)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 将ushort拆成Modbus大端的高低字节
        /// </summary>
        /// <param name="value">16位数值</param>
        /// <param name="high">输出高字节</param>
        /// <param name="low">输出低字节</param>
        public static void SplitUshortToBigEndian(ushort value, out byte high, out byte low)
        {
            high = (byte)(value >> 8);
            low = (byte)(value & 0xFF);
        }

        /// <summary>
        /// 解析Modbus应答帧，判断是否为异常响应
        /// </summary>
        /// <param name="data">完整报文</param>
        /// <param name="isTcp">true=ModbusTCP，false=ModbusRTU</param>
        /// <param name="realFuncCode">输出原始功能码</param>
        /// <param name="errorCode">输出异常码，正常时为0</param>
        /// <returns>true=异常应答；false=正常应答</returns>
        public static bool IsModbusErrorFrame(byte[] data, bool isTcp, out byte realFuncCode, out byte errorCode)
        {
            realFuncCode = 0;
            errorCode = 0;
            if (data == null || data.Length < 5)
                return false;
            byte funcByte;
            if (isTcp)
            {
                funcByte = data[7];
                if (data.Length < 9)
                    return false;
            }
            else
            {
                funcByte = data[1];
            }

            if (funcByte > 0X80)
            {
                realFuncCode = (byte)(funcByte - 0X80);
                errorCode = isTcp ? data[8] : data[2];
                return true;
            }
            realFuncCode = funcByte;
            return false;
        }
    }
}
