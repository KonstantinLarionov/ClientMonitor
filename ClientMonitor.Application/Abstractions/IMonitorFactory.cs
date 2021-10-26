using ClientMonitor.Application.Domanes.Enums;
using System;

namespace ClientMonitor.Application.Abstractions
{
	public interface IMonitorFactory
	{
		IMonitor GetMonitor(MonitoringTypes type);
	}
}

