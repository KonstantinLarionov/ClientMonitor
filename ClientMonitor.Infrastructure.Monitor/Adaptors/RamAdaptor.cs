using ClientMonitor.Application.Abstractions;
using System;
using System.Diagnostics;
using System.Threading;

namespace ClientMonitor.Infrastructure.Monitor.Adaptors
{
	public class RamAdaptor : IMonitor
	{
		public object ReceiveInfoMonitor()
		{
            var ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            string s = "";
            int i = 2;
            while (i > 0)
            {
                s = ramCounter.NextValue().ToString();
                i--;
                Thread.Sleep(1000);
            }
            return s;
        }

	}
}
