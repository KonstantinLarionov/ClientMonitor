using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.Database.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Handler
{
    public class PcMonitoringHandler : IPcMonitoringHandler
    {
        IMonitorFactory MonitorFactory;
        IRepository<CpuInfo> db;
        IRepository<RamInfo> db1;
        IRepository<ProcInfo> db2;
        IRepository<HttpInfo> db3;
        public PcMonitoringHandler(IMonitorFactory monitorFactory, IRepository<CpuInfo> repository, IRepository<RamInfo> repository1, IRepository<ProcInfo> repository2, IRepository<HttpInfo> repository3)
        {
            MonitorFactory = monitorFactory;
            db = repository;
            db1 = repository1;
            db2 = repository2;
            db3 = repository3;
        }
        public void Handle()
        {
            var infocpu = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.CPU);
            var resultMonitoringcpu = infocpu.ReceiveInfoMonitor() as List<ResultMonitoring>;
            var t = Convert.ToDouble(resultMonitoringcpu[0].Message);
            var t1 = Convert.ToDouble(resultMonitoringcpu[1].Message);
            CpuInfo cp = new CpuInfo
            {
                DateTime = DateTime.Now,
                BusyCpu = t1,
                FreeCpu = t,
            };
            db.AddInDb(cp);

            var inforam = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.RAM);
            var resultMonitoringram = inforam.ReceiveInfoMonitor() as List<ResultMonitoring>;
            var r = Convert.ToDouble(resultMonitoringram[0].Message);
            var r1 = Convert.ToDouble(resultMonitoringram[1].Message);
            RamInfo ram = new RamInfo
            {
                DateTime = DateTime.Now,
                BusyRam = r1,
                FreeRam = r,
            };
            db1.AddInDb(ram);

            var infoproc = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.Proc);
            var resultMonitoringproc = infoproc.ReceiveInfoMonitor() as List<ResultMonitoring>;
            ProcInfo proc = new ProcInfo
            {
                DateTime = DateTime.Now,
                Process = resultMonitoringproc[0].Message,
            };
            db2.AddInDb(proc);

            var infohttp = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.HTTP);
            var resultMonitoringhttp = infohttp.ReceiveInfoMonitor() as List<ResultMonitoring>;
            HttpInfo http = new HttpInfo
            {
                DateTime = DateTime.Now,
                Length = Convert.ToInt32(resultMonitoringhttp[0].Message),
            };
            db3.AddInDb(http);
        } 
    }
}
