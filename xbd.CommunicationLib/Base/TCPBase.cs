using modbus.CommunicationLibl.Lock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using thinger.DataConvertLib;

namespace modbus.CommunicationLibl.Base
{
    public class TCPBase
    {
        private TcpClient tcpClient;
        /// <summary>
        /// 发送超时时间
        /// </summary>
        public int SendTimeout { get; set; } = 2000;
        /// <summary>
        /// 接收超时时间
        /// </summary>
        public int ReceiveTimeout { get; set; } = 500;
        /// <summary>
        /// 连接超时时间
        /// </summary>
        public int ConnectTimeout { get; set; } = 2000;
        /// <summary>
        /// 读取超时时间
        /// </summary>
        public int ReadTimeout { get; set; } = 2000;
        /// <summary>
        /// 帧间隔时间（连续多少ms没收到新数据认为帧结束）
        /// </summary>
        private int FrameGapTime = 50;

        private SimpleHybridLock _lock { get; set; } = new SimpleHybridLock();

        /// <summary>
        /// 连接TCP服务器
        /// </summary>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="port">端口号</param>
        /// <returns></returns>
        public OperateResult ConnectTcp(string ipAddress, int port)
        {
            tcpClient = new TcpClient();
            try
            {
                Task connectTask = tcpClient.ConnectAsync(IPAddress.Parse(ipAddress), port);
                bool isFinished = connectTask.Wait(ConnectTimeout);

                // 情况1：超时
                if (!isFinished)
                {
                    tcpClient.Close();
                    return OperateResult.CreateFailResult($"连接超时: {ipAddress}:{port}");
                }

                // 情况2：任务完成但内部报错
                if (connectTask.Exception != null)
                {
                    tcpClient.Close();
                    var innerEx = connectTask.Exception.InnerException;
                    return OperateResult.CreateFailResult($"连接异常：{innerEx?.Message}");
                }

                // 连接成功
                tcpClient.SendTimeout = SendTimeout;
                tcpClient.ReceiveTimeout = ReceiveTimeout;
                return OperateResult.CreateSuccessResult();
            }
            catch (FormatException)
            {
                tcpClient?.Close();
                return OperateResult.CreateFailResult("IP地址格式错误");
            }
            catch (Exception e)
            {
                tcpClient?.Close();
                return OperateResult.CreateFailResult($"连接失败: {e.Message}");
            }
        }

        

        /// <summary>
        /// 断开TCP连接
        /// </summary>
        public void Close()
        {
            if (tcpClient == null) return;

            try
            {
                // 优雅关闭：先 Shutdown 通知对端，再 Close 释放资源
                if (tcpClient.Connected)
                {
                    tcpClient.Client.Shutdown(SocketShutdown.Both);
                }
            }
            catch
            {
                // 对端已断开时 Shutdown 会抛异常，忽略，直接走 Close
            }
            finally
            {
                tcpClient.Close();
                tcpClient.Dispose();
                tcpClient = null;
            }
        }

        public OperateResult<byte[]> SendAndReceive(byte[] request, int timeoutMs = 3000)
        {
            if (!tcpClient?.Connected ?? true)
            {
                return OperateResult.CreateFailResult<byte[]>("连接断开");
            }
            NetworkStream stream = tcpClient.GetStream();
            try
            {
                _lock.Enter();
                using MemoryStream ms = new MemoryStream();
                // 发送数据
                stream.Write(request, 0, request.Length);
                // 强制写入缓存数据
                stream.Flush();
                byte[] buffer = new byte[1024];
                DateTime lastDataTime = DateTime.Now;
                DateTime start = DateTime.Now;
                while (true)
                {
                    if (tcpClient.Available > 0)
                    {
                        int count = stream.Read(buffer, 0, Math.Min(tcpClient.Available, buffer.Length));

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
            catch (Exception e)
            {
                return OperateResult.CreateFailResult<byte[]>(e.Message);
            }
            finally
            {
                _lock.Leave();
                // 注意：不能 stream.Dispose()，NetworkStream 的 Dispose 会连带关闭 TCP 连接
            }
        }
    }
}
