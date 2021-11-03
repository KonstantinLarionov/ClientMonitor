using System;
using ClientMonitor.Application.Domanes.Enums;

namespace ClientMonitor.Application.Domanes.Objects
{
    public class PcMonitoringInfo
{
        public MonitoringTypes MonitoringType { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
}
}
