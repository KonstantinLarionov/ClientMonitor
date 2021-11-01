using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Handler
{
    public class PcMonitoringHandler : IPcMonitoringHandler
    {
        IMonitorFactory MonitorFactory;
        IRepository<PcInfo> db;
        public PcMonitoringHandler(IMonitorFactory monitorFactory, IRepository<PcInfo> repository)
        {
            MonitorFactory = monitorFactory;
            db = repository;
        }
        public void Handle()
        {
            List<ResultMonitoring> results = new List<ResultMonitoring>();
            var infocpu = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.CPU);
            var inforam = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.RAM);
            var infoproc =  MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.Proc);
            var infohttp = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.HTTP);

            var resultMonitoringcpu = infocpu.ReceiveInfoMonitor() as List<ResultMonitoring>;
            var resultMonitoringram = inforam.ReceiveInfoMonitor() as List<ResultMonitoring>;
            var resultMonitoringproc = infoproc.ReceiveInfoMonitor() as List<ResultMonitoring>;
            var resultMonitoringhttp = infohttp.ReceiveInfoMonitor() as List<ResultMonitoring>;

            PcInfo stat = new PcInfo
            {
                DateTime = DateTime.Now,
                //Cpu = infocpu.ToString(),
                Cpu = resultMonitoringcpu.ToString(),
                Ram = resultMonitoringram.ToString(),
                Proc = resultMonitoringproc.ToString(),
                Http = resultMonitoringhttp.ToString(),
            };

            db.AddInDb(stat);
        }
    }
}
