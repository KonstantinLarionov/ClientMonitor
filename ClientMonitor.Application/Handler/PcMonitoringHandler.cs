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
        IRepository<CpuInfo> dbCpu;
        IRepository<RamInfo> dbRam;
        IRepository<ProcInfo> dbProc;
        IRepository<HttpInfo> dbHttp;
        public PcMonitoringHandler(IMonitorFactory monitorFactory, IRepository<CpuInfo> repositoryCpu, IRepository<RamInfo> repositoryRam, IRepository<ProcInfo> repositoryProc, IRepository<HttpInfo> repositoryHttp)
        {
            MonitorFactory = monitorFactory;
            dbCpu = repositoryCpu;
            dbRam = repositoryRam;
            dbProc = repositoryProc;
            dbHttp = repositoryHttp;
        }
        public void HandleCpu()
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
            dbCpu.AddInDb(cp);
        }
        public void HandleRam()
        {
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
            dbRam.AddInDb(ram);
        }

        public void HandleProc()
        {

            var infoproc = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.Proc);
            var resultMonitoringproc = infoproc.ReceiveInfoMonitor() as List<ResultMonitoring>;
            ProcInfo proc = new ProcInfo
            {
                DateTime = DateTime.Now,
                Process = resultMonitoringproc[0].Message,
            };
            dbProc.AddInDb(proc);
        }

        public void HandleHttp()
        {
            var infohttp = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.HTTP);
            var resultMonitoringhttp = infohttp.ReceiveInfoMonitor() as List<ResultMonitoring>;
            HttpInfo http = new HttpInfo
            {
                DateTime = DateTime.Now,
                Length = Convert.ToInt32(resultMonitoringhttp[0].Message),
            };
            dbHttp.AddInDb(http);
        }
    } 
}

