using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace ClientMonitor.Infrastructure.Monitor.Adaptors
{
	public class RamAdaptor : IMonitor
	{
		public object ReceiveInfoMonitor()
		{
            List<ResultMonitoring> resultMonitoring = new List<ResultMonitoring>();
            var ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            string s = "";
            int i = 2;
            while (i > 0)
            {
                s = ramCounter.NextValue().ToString();
                i--;
                Thread.Sleep(1000);
            }
            resultMonitoring.Add(new ResultMonitoring(true, "RAM mB свободная память Полигонная: " + s));
            return resultMonitoring;
        }

	}
}
