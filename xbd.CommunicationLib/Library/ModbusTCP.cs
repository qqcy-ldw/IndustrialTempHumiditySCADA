using modbus.CommunicationLibl.Base;
using modbus.CommunicationLibl.Enum;
using modbus.CommunicationLibl.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thinger.DataConvertLib;

namespace modbus.CommunicationLibl.Library
{
    public class ModbusTCP : TCPBase, IModbusRW
    {
        public OperateResult<bool[]> ReadCoils(ushort start, ushort length, byte slaveId = 1)
        {
            byte[] Messages = BuildReadMessageFrame(start,length,ModbusFunctionCode.ReadCoil);
            var result = SendAndReceive(Messages);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailResult<bool[]>(result.Message);
            }
            // 解析响应体
            var operateResult = CheckResponse(result.Content, true, slaveId);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult<bool[]>(operateResult.Message);
            }

            // 提取线圈状态数据
            // 数据位
            int byteLength = length % 8 == 0 ? length / 8 : length / 8 + 1;
            byte[] coilData = result.Content.Skip(9).Take(byteLength).ToArray();
            // 截取有效的线圈状态位
            return OperateResult.CreateSuccessResult(
                BitLib.GetBitArrayFromByteArray(coilData, length)
                );
        }

        public Task<OperateResult<bool[]>> ReadCoilsAsync(ushort start, ushort length, byte slaveId = 1)
        {
            throw new NotImplementedException();
        }

        public OperateResult<byte[]> ReadHoldingRegisters(ushort start, ushort length, byte slaveId = 1)
        {
            byte[] Messages = BuildReadMessageFrame(start, length, ModbusFunctionCode.ReadHoldingRegister);
            var result = SendAndReceive(Messages);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailResult<byte[]>(result.Message);
            }
            // 解析响应体
            var operateResult = CheckResponse(result.Content, true, slaveId);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult<byte[]>(operateResult.Message);
            }

            // 提取线圈状态数据
            // 数据位
            int byteLength = length * 2;
            byte[] holdingRegisters = result.Content.Skip(9).Take(byteLength).ToArray();
            // 截取有效的线圈状态位
            return OperateResult.CreateSuccessResult(holdingRegisters);
        }

        public Task<OperateResult<byte[]>> ReadHoldingRegistersAsync(ushort start, ushort length, byte slaveId = 1)
        {
            throw new NotImplementedException();
        }

        public OperateResult<byte[]> ReadInputRegisters(ushort start, ushort length, byte slaveId = 1)
        {
            byte[] Messages = BuildReadMessageFrame(start, length, ModbusFunctionCode.ReadInputRegister);
            var result = SendAndReceive(Messages);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailResult<byte[]>(result.Message);
            }
            // 解析响应体
            var operateResult = CheckResponse(result.Content, true, slaveId);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult<byte[]>(operateResult.Message);
            }

            // 提取输入寄存器数据
            int byteLength = length * 2;
            byte[] inputRegisters = result.Content.Skip(9).Take(byteLength).ToArray();
            return OperateResult.CreateSuccessResult(inputRegisters);
        }

        public Task<OperateResult<byte[]>> ReadInputRegistersAsync(ushort start, ushort length, byte slaveId = 1)
        {
            throw new NotImplementedException();
        }

        public OperateResult<bool[]> ReadInputs(ushort start, ushort length, byte slaveId = 1)
        {
            byte[] Messages = BuildReadMessageFrame(start, length, ModbusFunctionCode.ReadDiscreteInput);
            var result = SendAndReceive(Messages);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailResult<bool[]>(result.Message);
            }
            // 解析响应体
            var operateResult = CheckResponse(result.Content, true, slaveId);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult<bool[]>(operateResult.Message);
            }

            // 提取线圈状态数据
            // 数据位
            int byteLength = length % 8 == 0 ? length / 8 : length / 8 + 1;
            byte[] coilData = result.Content.Skip(9).Take(byteLength).ToArray();
            // 截取有效的线圈状态位
            return OperateResult.CreateSuccessResult(
                BitLib.GetBitArrayFromByteArray(coilData, length)
                );
        }

        public Task<OperateResult<bool[]>> ReadInputsAsync(ushort start, ushort length, byte slaveId = 1)
        {
            throw new NotImplementedException();
        }

        public OperateResult WriteMultipleCoils(ushort start, bool[] values, byte slaveId = 1)
        {
            if (values == null || values.Length == 0)
                return OperateResult.CreateFailResult("线圈数组不能为空");

            // 1、拼接报文: MBAP头(7) + 功能码0F + 地址(2) + 数量(2) + 字节数(1) + 线圈数据(N)
            byte[] coilData = ByteArrayLib.GetByteArrayFromBoolArray(values);
            byte[] request = BuildWriteMessageFrame(start, (ushort)values.Length, coilData, ModbusFunctionCode.WriteMultipleCoils, slaveId);

            // 2、发送并接收
            var result = SendAndReceive(request);
            if (!result.IsSuccess)
                return OperateResult.CreateFailResult(result.Message);

            // 3、响应验证
            var operateResult = CheckResponse(result.Content, false, slaveId);
            if (!operateResult.IsSuccess)
                return OperateResult.CreateFailResult(operateResult.Message);

            // 4、验证响应: MBAP头(7)+功能码(1)+地址(2)+数量(2)
            byte[] response = result.Content;
            if (response[7] != (byte)ModbusFunctionCode.WriteMultipleCoils)
                return OperateResult.CreateFailResult("响应帧功能码不匹配");

            ushort respStart = (ushort)((response[8] << 8) | response[9]);
            ushort respQty = (ushort)((response[10] << 8) | response[11]);
            if (respStart != start || respQty != values.Length)
                return OperateResult.CreateFailResult($"响应地址/数量不匹配：期望 {start}/{values.Length}，实际 {respStart}/{respQty}");

            return OperateResult.CreateSuccessResult();
        }

        public Task<OperateResult> WriteMultipleCoilsAsync(ushort start, bool[] values, byte slaveId = 1)
        {
            throw new NotImplementedException();
        }

        public OperateResult WriteMultipleRegisters(ushort start, ushort[] values, byte slaveId = 1)
        {
            if (values == null || values.Length == 0)
                return OperateResult.CreateFailResult("写入数据不能为空");

            // 1、拼接报文: MBAP头(7) + 功能码10 + 地址(2) + 数量(2) + 字节数(1) + 寄存器数据(N)
            byte[] regData = ByteArrayLib.GetByteArrayFromUShortArray(values);
            byte[] request = BuildWriteMessageFrame(start, (ushort)values.Length, regData, ModbusFunctionCode.WriteMultipleRegisters, slaveId);

            // 2、发送并接收
            var result = SendAndReceive(request);
            if (!result.IsSuccess)
                return OperateResult.CreateFailResult(result.Message);

            // 3、响应验证
            var operateResult = CheckResponse(result.Content, false, slaveId);
            if (!operateResult.IsSuccess)
                return OperateResult.CreateFailResult(operateResult.Message);

            // 4、验证响应: MBAP头(7)+功能码(1)+地址(2)+数量(2)
            byte[] response = result.Content;
            if (response[7] != (byte)ModbusFunctionCode.WriteMultipleRegisters)
                return OperateResult.CreateFailResult("响应帧功能码不匹配");

            ushort respStart = (ushort)((response[8] << 8) | response[9]);
            ushort respQty = (ushort)((response[10] << 8) | response[11]);
            if (respStart != start || respQty != values.Length)
                return OperateResult.CreateFailResult($"响应地址/数量不匹配：期望 {start}/{values.Length}，实际 {respStart}/{respQty}");

            return OperateResult.CreateSuccessResult();
        }

        public Task<OperateResult> WriteMultipleRegistersAsync(ushort start, ushort[] values, byte slaveId = 1)
        {
            throw new NotImplementedException();
        }

        public OperateResult WriteSingleCoil(ushort start, bool value, byte slaveId = 1)
        {
            // 1、拼接报文: MBAP头(7) + 功能码05 + 地址(2) + 值(2: FF00写1 / 0000写0)
            ByteArray byteArray = new ByteArray();
            byteArray.Add(transactionld);                        // 事务id（2）
            byteArray.Add((ushort)0x00);                         // 协议id（2）
            byteArray.Add((ushort)0x6);                          // 长度（2）= 单元id(1)+功能码(1)+地址(2)+值(2)
            byteArray.Add(slaveId);                              // 单元id（1）
            byteArray.Add((byte)ModbusFunctionCode.WriteSingleCoil);
            byteArray.Add(start);                                // 地址（2）
            byteArray.Add(value ? (ushort)0xFF00 : (ushort)0x0000);  // 值（2）
            byte[] request = byteArray.array;

            // 2、发送并接收
            var result = SendAndReceive(request);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailResult(result.Message);
            }

            // 3、响应验证
            var operateResult = CheckResponse(result.Content, false, slaveId);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult(operateResult.Message);
            }

            // 4、功能码05的响应帧必须是请求帧的完全回显
            if (result.Content.SequenceEqual(request))
            {
                return OperateResult.CreateSuccessResult();
            }
            return OperateResult.CreateFailResult("响应报文与请求报文不一致");
        }

        public Task<OperateResult> WriteSingleCoilAsync(ushort start, bool value, byte slaveId = 1)
        {
            throw new NotImplementedException();
        }

        public OperateResult WriteSingleRegister(ushort start, ushort value, byte slaveId = 1)
        {
            // 1、拼接报文: MBAP头(7) + 功能码06 + 地址(2) + 值(2)
            ByteArray byteArray = new ByteArray();
            byteArray.Add(transactionld);                        // 事务id（2）
            byteArray.Add((ushort)0x00);                         // 协议id（2）
            byteArray.Add((ushort)0x6);                          // 长度（2）= 单元id(1)+功能码(1)+地址(2)+值(2)
            byteArray.Add(slaveId);                              // 单元id（1）
            byteArray.Add((byte)ModbusFunctionCode.WriteSingleRegister);
            byteArray.Add(start);                                // 地址（2）
            byteArray.Add(value);                                // 值（2）
            byte[] request = byteArray.array;

            // 2、发送并接收
            var result = SendAndReceive(request);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailResult(result.Message);
            }

            // 3、响应验证
            var operateResult = CheckResponse(result.Content, false, slaveId);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult(operateResult.Message);
            }

            // 4、功能码06的响应帧必须是请求帧的完全回显
            if (result.Content.SequenceEqual(request))
            {
                return OperateResult.CreateSuccessResult();
            }
            return OperateResult.CreateFailResult("响应报文与请求报文不一致");
        }

        public Task<OperateResult> WriteSingleRegisterAsync(ushort start, ushort value, byte slaveId = 1)
        {
            throw new NotImplementedException();
        }

        private ushort _transactionId;

        public ushort transactionld
        {
            get
            {
                return _transactionId == ushort.MaxValue ? (ushort)1 : ++_transactionId;
            }
            set
            {
                if (value > 0)
                {
                    _transactionId = value;
                }
            }
        }

        private byte[] BuildReadMessageFrame(ushort start, ushort length, ModbusFunctionCode modbusCode, byte slaveId = 1)
        {
            ByteArray byteArray = new ByteArray();
            // 拼接MBAP头
            // 事务id（2）
            byteArray.Add(transactionld);
            // 协议id（2）
            byteArray.Add((ushort)0x00);
            // 长度（2）
            byteArray.Add((ushort)0x6);
            // 单元id（1）
            byteArray.Add(slaveId);

            // 功能码(1)
            byteArray.Add((byte)modbusCode);
            // 起始地址(2)
            byteArray.Add(start);
            // 读取地址位数(2)
            byteArray.Add(length);
            return byteArray.array;
        }

        /// <summary>
        /// 拼接写多个帧（功能码0F/10共用）
        /// </summary>
        /// <param name="start">起始地址</param>
        /// <param name="quantity">写入数量</param>
        /// <param name="data">打包后的数据字节</param>
        /// <param name="modbusCode">功能码</param>
        /// <param name="slaveId">从站地址</param>
        private byte[] BuildWriteMessageFrame(ushort start, ushort quantity, byte[] data, ModbusFunctionCode modbusCode, byte slaveId = 1)
        {
            ByteArray byteArray = new ByteArray();
            // 事务id（2）
            byteArray.Add(transactionld);
            // 协议id（2）
            byteArray.Add((ushort)0x00);
            // 长度（2）= 单元id(1)+功能码(1)+地址(2)+数量(2)+字节数(1)+数据(N)
            byteArray.Add((ushort)(7 + data.Length));
            // 单元id（1）
            byteArray.Add(slaveId);
            // 功能码（1）
            byteArray.Add((byte)modbusCode);
            // 起始地址（2）
            byteArray.Add(start);
            // 写入数量（2）
            byteArray.Add(quantity);
            // 字节数（1）
            byteArray.Add((byte)data.Length);
            // 数据（N）
            foreach (byte b in data)
            {
                byteArray.Add(b);
            }
            return byteArray.array;
        }

        /// <summary>
        /// 响应验证
        /// </summary>
        private OperateResult CheckResponse(byte[] response, bool isRead, byte slaveId)
        {
            // 读响应: MBAP头(7)+功能码(1)+字节数(1)+数据(N)，最短9字节
            // 写响应: MBAP头(7)+功能码(1)+地址(2)+数量(2)，固定12字节
            int minLength = isRead ? 9 : 12;
            if (response.Length < minLength)
            {
                return OperateResult.CreateFailResult("放回数据长度异常");
            }
            // MBAP头: 事务ID(2)+协议ID(2)+长度(2)+单元标识符(1)，单元标识符在索引6
            if (slaveId != response[6])
            {
                return OperateResult.CreateFailResult("单元标识符不一致");
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
