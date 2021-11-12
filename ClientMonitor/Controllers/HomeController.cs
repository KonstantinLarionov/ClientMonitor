using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMonitor.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        
        private readonly IMonitorFactory MonitorFactory;
        public HomeController(ILogger<HomeController> logger, IMonitorFactory monitorFactory)
        {
            _logger = logger;
            MonitorFactory = monitorFactory;
        }

        [HttpGet]
        [Route("GetCpu")]
        public CpuInfo GetCpu()
        {
            var infocpu = MonitorFactory.GetMonitor(Application.Domanes.Enums.MonitoringTypes.CPU);
            var resultMonitoringcpu = infocpu.ReceiveInfoMonitor() as List<ResultMonitoring>;
            var t = Convert.ToDouble(resultMonitoringcpu[0].Message);
            var t1 = Convert.ToDouble(resultMonitoringcpu[1].Message);
            CpuInfo cp = new CpuInfo
            {
                DateTime = DateTime.Now,
                BusyCpu = t1,
                FreeCpu = t,
            };

            return cp;
        }

        [HttpGet]
        [Route("GetRam")]
        public RamInfo GetRam()
        {
            var inforam = MonitorFactory.GetMonitor(Application.Domanes.Enums.MonitoringTypes.RAM);
            var resultMonitoringram = inforam.ReceiveInfoMonitor() as List<ResultMonitoring>;
            var r = Convert.ToDouble(resultMonitoringram[0].Message);
            var r1 = Convert.ToDouble(resultMonitoringram[1].Message);
            RamInfo ram = new RamInfo
            {
                DateTime = DateTime.Now,
                BusyRam = r1,
                FreeRam = r,
            };

            return ram;
        }

        [HttpGet]
        [Route("GetProc")]
        public ProcInfo GetProc()
        {
            var infoproc = MonitorFactory.GetMonitor(Application.Domanes.Enums.MonitoringTypes.Proc);
            var resultMonitoringproc = infoproc.ReceiveInfoMonitor() as List<ResultMonitoring>;
            ProcInfo proc = new ProcInfo
            {
                DateTime = DateTime.Now,
                Process = resultMonitoringproc[0].Message,
            };

            return proc;
        }

        [HttpGet]
        [Route("GetHttp")]
        public HttpInfo GetHttp()
        {
            var infohttp = MonitorFactory.GetMonitor(Application.Domanes.Enums.MonitoringTypes.HTTP);
            var resultMonitoringhttp = infohttp.ReceiveInfoMonitor() as List<ResultMonitoring>;
            HttpInfo http = new HttpInfo
            {
                DateTime = DateTime.Now,
                Length = Convert.ToInt32(resultMonitoringhttp[0].Message),
            };
            return http;
        }
    }
}
