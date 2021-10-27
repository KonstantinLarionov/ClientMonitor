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
			string listproc = "Список процессов: ";
			string lastproc = "";
			foreach (Process instance in processes)
			{
				if (instance.ProcessName != "svchost")
				{
					if (lastproc != instance.ProcessName)
					{
						listproc = listproc + instance.ProcessName +"/";
					}
				}

				else { continue; }
				lastproc = instance.ProcessName;
				//resultMonitoring.Add(new ResultMonitoring(true, "Полигонная процессы : " + instance.ProcessName));			
			}
			//string test="";
			//foreach (var list in listProc)
			//{
			//	test = list + ",";
			//}

			resultMonitoring.Add(new ResultMonitoring(true, listproc));
			return resultMonitoring;

		}

	}
}
