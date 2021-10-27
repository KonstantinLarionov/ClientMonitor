using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ClientMonitor.Infrastructure.Monitor.Adaptors
{
	public class ProcAdaptor : IMonitor
	{

		public object ReceiveInfoMonitor()
		{
			List<ResultMonitoring> resultMonitoring = new List<ResultMonitoring>();
			Process[] processes;
			//List<string> listProc = new List<string>();
			processes = Process.GetProcesses();
			string listproc = "";
			foreach (Process instance in processes)
			{
				resultMonitoring.Add(new ResultMonitoring(true, "Полигонная" + instance.ProcessName));			
			}
			//string test="";
			//foreach (var list in listProc)
			//{
			//	test = list + ",";
			//}
			return resultMonitoring;

		}

	}
}
