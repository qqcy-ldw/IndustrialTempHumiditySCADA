using System.IO.Ports;
using modbus.CommunicationLibl.Interface;
using modbus.CommunicationLibl.Lock;
using thinger.DataConvertLib;

namespace modbus.CommunicationLibl.Base;

public class SerialDeviceBase
{

    //protected SerialDeviceBase() { }

    private SerialPort _serialPort;

    public int _readTimeout { get; set; } = 2000;

    public int _writeTimeout { get; set; } = 2000;

    public bool DtrEnable { get; set; } = true;

    public bool RtsEnable { get; set; } = true;

    private SimpleHybridLock _lock { get; set; } = new SimpleHybridLock();

    // 缓存请求报文
    public byte[] Request { get; set; }

    // 缓存响应
    public byte[] Response {  get; set; }

    /// <summary>
    /// 帧间隔时间（连续多少ms没收到新数据认为帧结束）
    /// </summary>
    private int FrameGapTime = 50;

    // 总超时时间
    private int ReceiveTimeOut = 3000;

    /// <summary>
    /// 打开串口
    /// </summary>
    /// <param name="portName">串口名称</param>
    /// <param name="baudRate">初始波特率:9600</param>
    /// <param name="dataBits">初始数据位:8</param>
    /// <param name="parity">初始校验位:None无校验</param>
    /// <param name="stopBits">初始停止位:1</param>
    /// <returns>打开结果；true=串口打开成功，false=打开失败</returns>
    public bool Connect(string portName, int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None,
        StopBits stopBits = StopBits.One)
    {
        _serialPort = new SerialPort();
        if (_serialPort.IsOpen)
        {
            _serialPort.Close();
        }

        _serialPort.PortName = portName;
        _serialPort.BaudRate = baudRate;
        _serialPort.DataBits = dataBits;
        _serialPort.Parity = parity;
        _serialPort.StopBits = stopBits;

        _serialPort.ReadTimeout = _readTimeout;
        _serialPort.WriteTimeout = _writeTimeout;
        _serialPort.DtrEnable = DtrEnable;
        _serialPort.RtsEnable = RtsEnable;

        try
        {
            _serialPort.Open();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

        return true;
    }

    /// <summary>
    /// 断开连接
    /// </summary>
    public void Close()
    {
        if (_serialPort.IsOpen)
        {
            _serialPort.Close();
        }
    }


    /// <summary>
    /// 发送数据并接收数据，返回原始的字节数据
    /// </summary>
    /// <param name="sendBytes">要发送的字节数组</param>
    /// <param name="timeoutMs">超时时间（毫秒）</param>
    /// <returns></returns>
    public OperateResult<byte[]> SendAndReceive(byte[] sendBytes, int timeoutMs = 3000)
    {
        if (_serialPort != null)
        {
            if (!_serialPort.IsOpen)
            {
                return OperateResult.CreateFailResult<byte[]>("串口未打开");
            }
        }
        else
        {
            return OperateResult.CreateFailResult<byte[]>("串口未初始化");
        }
        // 清空缓冲区，防止旧脏数据干扰
        _serialPort.DiscardInBuffer();
        _serialPort.DiscardOutBuffer();

        try
        {
            _lock.Enter();
            // 发送数据
            _serialPort.Write(sendBytes, 0, sendBytes.Length);
            // 接收数据
            using var ms = new MemoryStream();
            byte[] buffer = new byte[1024];
            DateTime start = DateTime.Now;
            DateTime lastDataTime = DateTime.Now;
            while (true)
            {
                int count = _serialPort.BytesToRead;
                if (count > 0)
                {
                    _serialPort.Read(buffer, 0, count);
                    ms.Write(buffer, 0, count);
                    lastDataTime = DateTime.Now;
                }
                else if (ms.Length > 0 &&
                         (DateTime.Now - lastDataTime).TotalMilliseconds > FrameGapTime)
                {
                    break;   // 帧间隔超时，收完了
                }
                else if ((DateTime.Now - start).TotalMilliseconds > timeoutMs)
                {
                    return OperateResult.CreateFailResult<byte[]>("数据读取超时");
                }
            }
            return OperateResult.CreateSuccessResult(ms.ToArray());
        }
        catch (Exception ex)
        {
            return OperateResult.CreateFailResult<byte[]>(ex.Message);
        }
        finally
        {
            _lock.Leave();
        }
    }

}
