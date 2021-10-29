using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Handler
{
    class PcMonitoringHandler : IPcMonitoringHandler
    {
        IMonitorFactory MonitorFactory;
        IRepositoryPc<PcInfo> db;
        public PcMonitoringHandler(IMonitorFactory monitorFactory, IRepositoryPc<PcInfo> repository)
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
            results.AddRange(resultMonitoringcpu);
            var resultMonitoringram = inforam.ReceiveInfoMonitor() as List<ResultMonitoring>;
            results.AddRange(resultMonitoringram);

            string test1 = "";
            //foreach (var result in results)
            //{
            //    test1 = test1 + "__" + result.Message + "\r\n";
            //}


            PcInfo stat = new PcInfo
            {
                DateTime = DateTime.Now,
                Cpu = infocpu.ToString(),
                Ram = inforam.ToString(),
                Proc = infoproc.ToString(),
                Http = infohttp.ToString(),
            };

            db.AddInDb(stat);
        }
    }
}
