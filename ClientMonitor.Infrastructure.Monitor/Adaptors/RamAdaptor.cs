using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.Management;

namespace ClientMonitor.Infrastructure.Monitor.Adaptors
{
	public class RamAdaptor : IMonitor
	{
		public object ReceiveInfoMonitor()
		{
            List<ResultMonitoring> resultMonitoring = new List<ResultMonitoring>();

            string bsrm = "";
            string frrm = "";
            ManagementObjectSearcher ramMonitor =    //запрос к WMI для получения памяти ПК
            new ManagementObjectSearcher("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem");
            foreach (ManagementObject objram in ramMonitor.Get())
            {
                ulong totalRam = Convert.ToUInt64(objram["TotalVisibleMemorySize"]);    //общая память ОЗУ
                ulong busyRam = (totalRam - Convert.ToUInt64(objram["FreePhysicalMemory"]));
                frrm = ((totalRam - busyRam) / 1024).ToString();
                bsrm = (busyRam / 1024).ToString();
            }
            resultMonitoring.Add(new ResultMonitoring(true, frrm + "/"+ bsrm));
            return resultMonitoring;
        }
	}
}
