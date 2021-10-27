using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;

namespace ClientMonitor.Infrastructure.Monitor.Adaptors
{
    public class CpuAdaptor : IMonitor
    {
        public CpuAdaptor()
        {

        }

        public object ReceiveInfoMonitor()
        {
            List<ResultMonitoring> resultMonitoring = new List<ResultMonitoring>();

            var cpuload = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            string s = "";
            int i = 2;
            while (i>0)
            {
                s=cpuload.NextValue().ToString();
                i--;
                Thread.Sleep(1000);
                //return;
            }
            resultMonitoring.Add(new ResultMonitoring(true, "CPU  % загруженности Полигонная: " + s));
            return resultMonitoring;

        }


    }
}
