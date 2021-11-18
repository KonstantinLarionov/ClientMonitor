using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;

namespace ClientMonitor.Infrastructure.Monitor.Adaptors
{
    public class CpuAdaptor : IMonitor
    {
        public object ReceiveInfoMonitor()
        {
            List<ResultMonitoring> resultMonitoring = new List<ResultMonitoring>();
            var cpuload = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            double s = 0;
            int i = 2;
            while (i > 0)
            {
                s = cpuload.NextValue();
                i--;
                Thread.Sleep(1000);
            }
            double s1 = 100 - s;
            resultMonitoring.Add(new ResultMonitoring(true, s.ToString()));
            resultMonitoring.Add(new ResultMonitoring(true, s1.ToString()));
            return resultMonitoring;
        }
    }
}
