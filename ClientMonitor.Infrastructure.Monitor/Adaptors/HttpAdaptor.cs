using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ClientMonitor.Infrastructure.Monitor.Adaptors
{
	public class HttpAdaptor : IMonitor
	{
		public HttpAdaptor()
        {
			
        }
		public void StartMonitoring()
        {
			GetHttp();
		}

		public void StopMonitoring()
		{
			throw new NotImplementedException();
		}


        public void GetHttp()
        {
            
        }



    }
}
