using ClientMonitor.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ClientMonitor.Infrastructure.Monitor.Adaptors
{
	public class ProcAdaptor : IMonitor
	{

		public string ReceiveInfoMonitor()
		{
			Process[] processes;
			List<string> listProc = new List<string>();
			processes = Process.GetProcesses();
			foreach (Process instance in processes)
				listProc.Add(instance.ProcessName);
			string test="";
			foreach (var list in listProc)
				test = list + ",";
			return test;

		}

	}
}
