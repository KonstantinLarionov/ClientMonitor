using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ClientMonitor.Infrastructure.Monitor.Adaptors
{
    public class ServersAdaptor : IMonitor
    {

        #region [ServerData]
        public readonly List<DataServer> Servers = new List<DataServer>()
        {
        new DataServer
            {
                IpServer = "188.186.238.120",
                PortServer = 8888,
            },
            new DataServer
            {
                IpServer = "188.186.238.120",
                PortServer = 8889,
            },
            new DataServer
            {
                IpServer = "92.255.240.7",
                PortServer = 8890,
            },
        };

        #endregion [ServerData]
        public object ReceiveInfoMonitor()
        {
            List<ResultMonitoring> resultMonitoring = new List<ResultMonitoring>();

            foreach (var server in Servers)
            {
                var result = ResultCheckStatus(server);
                if (result.Success)
                {
                    resultMonitoring.Add(new ResultMonitoring(true, $"IpServer: {server.IpServer}, PortServer: {server.PortServer} : {result.Message}"));
                }
                else { resultMonitoring.Add(new ResultMonitoring(false, $"IpServer: {server.IpServer}, PortServer: {server.PortServer}, Ошибка")); }
            }

            return resultMonitoring;
        }

        public ResultStatusRequest ResultCheckStatus(DataServer resourse)
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(resourse.IpServer, resourse.PortServer);
                byte[] msg = new byte[256];
                StringBuilder response = new StringBuilder();
                NetworkStream stream = client.GetStream();
                do
                {
                    int bytes = stream.Read(msg, 0, msg.Length);
                    response.Append(Encoding.UTF8.GetString(msg, 0, bytes));
                }
                while (stream.DataAvailable);
                string rspnc = response.ToString();
                if (rspnc.ToString() == "1") { return new ResultStatusRequest("Сервер работает", DateTime.Now, true); }
                else { return new ResultStatusRequest("Сервер НЕ работает", DateTime.Now, false); }
            }
            catch
            {
                return new ResultStatusRequest("Нет подключения к серверу", DateTime.Now, false);
            }
        }

    }
}
