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
        public static List<string> listpak = new List<string>();

		public object ReceiveInfoMonitor()
		{
			List<ResultMonitoring> resultMonitoring = new List<ResultMonitoring>();
			string listhttp = "";
            foreach (var list in listpak)
            {
                listhttp = listhttp + list + "\n";
            }
			resultMonitoring.Add(new ResultMonitoring(true, listhttp));
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
            mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
            String host = Dns.GetHostName();
            IPAddress ip = Dns.GetHostByName(host).AddressList[0];
            try
            {
                IPAddress Ip = IPAddress.Parse("192.168.89.20");
                IPEndPoint Ip_point = new IPEndPoint(Ip, 0);
                mainSocket.Bind(Ip_point);
                mainSocket.SetSocketOption(SocketOptionLevel.IP,
                               SocketOptionName.HeaderIncluded,
                               true);

                byte[] byTrue = new byte[4] { 1, 0, 0, 0 };
                byte[] byOut = new byte[4];

                mainSocket.IOControl(IOControlCode.ReceiveAll, byTrue, byOut);

                int k = 10;
                while (k > 0)
                {
                    mainSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
                    k--;
                }

            }
            catch { }
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

            listpak.Add($"Id {point.Id} From {point.From} To {point.To} Length {point.LenghtData} Protocol {point.Protocol} Время {point.DateTime}");
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
