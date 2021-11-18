using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Application.Domanes.Objects.HttpObject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ClientMonitor.Infrastructure.Monitor.Adaptors
{
    public class HttpAdaptor : IMonitor
    {
        private static string p;

        public object ReceiveInfoMonitor()
        {
            GetHttp();
            List<ResultMonitoring> resultMonitoring = new List<ResultMonitoring>();
            resultMonitoring.Add(new ResultMonitoring(true, p));
            return resultMonitoring;
        }

        public MonitorType Type { get; set; }

        private static byte[] byteData = new byte[3096];
        static private Socket mainSocket;


        private static void OnReceive(IAsyncResult ar)
        {
            try
            {
                int nReceived = mainSocket.EndReceive(ar);
                ParseData(byteData, nReceived);

            }
            catch { }
        }

        public static void GetHttp()
        {

            try
            {
                mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
                string myip = GetMyIP();
                IPAddress Ip = IPAddress.Parse(myip);
                IPEndPoint Ip_point = new IPEndPoint(Ip, 0);
                mainSocket.Bind(Ip_point);
                mainSocket.SetSocketOption(SocketOptionLevel.IP,
                               SocketOptionName.HeaderIncluded,
                               true);

                byte[] byTrue = new byte[4] { 1, 0, 0, 0 };
                byte[] byOut = new byte[4];

                mainSocket.IOControl(IOControlCode.ReceiveAll, byTrue, byOut);
                mainSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
            }
            catch { }
        }


        private static string GetMyIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return null;
        }

        private static void ParseData(byte[] byteData, int nReceived)
        {
            IPHeader ipHeader = new IPHeader(byteData, nReceived);
            HttpPoint point = new HttpPoint();
            point.TCPPoint = new TCPPoint() { DateTime = DateTime.Now };
            point.UDPPoint = new UDPPoint() { DateTime = DateTime.Now };
            point.From = ipHeader.SourceAddress.ToString();
            point.To = ipHeader.DestinationAddress.ToString();
            point.LenghtData = ipHeader.MessageLength;
            point.DateTime = DateTime.Now;
            switch (ipHeader.ProtocolType)
            {
                case Protocol.TCP:
                    TCPHeader tcpHeader = new TCPHeader(ipHeader.Data, ipHeader.MessageLength);
                    point.TCPPoint = new TCPPoint(tcpHeader.SourcePort, tcpHeader.DestinationPort,
                      tcpHeader.SequenceNumber, tcpHeader.AcknowledgementNumber, tcpHeader.HeaderLength,
                      tcpHeader.WindowSize, tcpHeader.UrgentPointer, tcpHeader.Flags,
                      tcpHeader.Checksum, ToSTR(tcpHeader.Data), tcpHeader.MessageLength, DateTime.Now);
                    break;
                case Protocol.UDP:
                    UDPHeader udpHeader = new UDPHeader(ipHeader.Data, (int)ipHeader.MessageLength);
                    point.UDPPoint = new UDPPoint(udpHeader.SourcePort,
                           udpHeader.DestinationPort, udpHeader.Length,
                           udpHeader.Checksum, ToSTR(udpHeader.Data), DateTime.Now);
                    break;
                case Protocol.Unknown:
                    break;
            }

            p = point.LenghtData.ToString();
        }
        private static string ToSTR(byte[] arr)
        {
            string str = "";
            foreach (var item in arr)
            {
                str += item + " ";
            }
            return str;
        }

    }
}
