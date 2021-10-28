using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Infrastructure.Monitor.Adaptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.Monitor
{
    class MonitorFactory : IMonitorFactory
    {
        private readonly Dictionary<MonitoringTypes, IMonitor> _adaptors;

        public MonitorFactory()
        {
            _adaptors = new Dictionary<MonitoringTypes, IMonitor>()
            {
                {MonitoringTypes.RAM, new RamAdaptor() },
                {MonitoringTypes.CPU, new CpuAdaptor() },
                {MonitoringTypes.HTTP, new HttpAdaptor() },
                {MonitoringTypes.Proc, new ProcAdaptor() },
                {MonitoringTypes.Sites, new SitesAdaptor() },
                {MonitoringTypes.Servers, new ServersAdaptor() },
            };
        }
        public IMonitor GetMonitor(MonitoringTypes type) => _adaptors.FirstOrDefault(x => x.Key == type).Value;
    }
}
