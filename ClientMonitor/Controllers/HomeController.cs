using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientMonitor.Controllers
{

    //[ApiController]
    //[Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IMonitorFactory _monitorFactory;
        private readonly LoggerContext _db;
        IRepository<DataForEditInfo> dbData;
        IRepository<CpuInfo> dbCpu;
        IRepository<RamInfo> dbRam;
        IRepository<ProcInfo> dbProc;
        IRepository<HttpInfo> dbHttp;
        IRepository<LogInfo> dbLog;

        //public List<DataForEdit> Data { get; set; }
        public HomeController(ILogger<HomeController> logger, /*IMonitorFactory monitorFactory,*/ LoggerContext db, IRepository<DataForEditInfo> repositoryData, IRepository<LogInfo> repositoryLog, IRepository<CpuInfo> repositoryCpu, IRepository<RamInfo> repositoryRam, IRepository<ProcInfo> repositoryProc, IRepository<HttpInfo> repositoryHttp)
        {
            _logger = logger;
            //_monitorFactory = monitorFactory;
            _db = db;
            dbData = repositoryData;

            dbLog = repositoryLog;
            dbCpu = repositoryCpu;
            dbRam = repositoryRam;
            dbProc = repositoryProc;
            dbHttp = repositoryHttp;
        }

        public IActionResult Home()
        {
            return View(_db.EDataForEdit.ToList());
        }


        [HttpGet]
        public ActionResult Edit(string key)
        {
            if (key == null)
            {
                return View();
            }

            DataForEdit data1 = _db.EDataForEdit.Where(c => c.Name == key).FirstOrDefault();

            if (data1 != null)
            {
                return View(data1);
            }
            return RedirectToAction("Home");
        }


        [HttpPost]
        public ActionResult Edit(string key, string data)
        {
            key = Request.Form["hero"];
            dbData.Update(key, data);
            return RedirectToAction("Home");

        }

        [HttpPost]
        public ActionResult Stop()
        {
            dbData.Update("onOff", "True");
            return RedirectToAction("Home");
        }

        [HttpPost]
        public ActionResult Start()
        {
            dbData.Update("onOff", "False");
            return RedirectToAction("Home");
        }


        #region [Swagger]
        //[HttpGet]
        //[Route("GetCpu")]
        //public CpuInfo GetCpu()
        //{
        //    var infocpu = _monitorFactory.GetMonitor(Application.Domanes.Enums.MonitoringTypes.CPU);
        //    var resultMonitoringcpu = infocpu.ReceiveInfoMonitor() as List<ResultMonitoring>;
        //    var t = Convert.ToDouble(resultMonitoringcpu[0].Message);
        //    var t1 = Convert.ToDouble(resultMonitoringcpu[1].Message);
        //    CpuInfo cp = new CpuInfo
        //    {
        //        DateTime = DateTime.Now,
        //        BusyCpu = t1,
        //        FreeCpu = t,
        //    };
        //    return cp;
        //}

        //[HttpPost]
        //[Route("AddCpu")]
        //public void AddCpu(double t1, double t)
        //{
        //    CpuInfo cp = new CpuInfo
        //    {
        //        DateTime = DateTime.Now,
        //        BusyCpu = t1,
        //        FreeCpu = t,
        //    };
        //    dbCpu.AddInDb(cp);
        //}


        //[HttpGet]
        //[Route("GetRam")]
        //public RamInfo GetRam()
        //{
        //    var inforam = _monitorFactory.GetMonitor(Application.Domanes.Enums.MonitoringTypes.RAM);
        //    var resultMonitoringram = inforam.ReceiveInfoMonitor() as List<ResultMonitoring>;
        //    var r = Convert.ToDouble(resultMonitoringram[0].Message);
        //    var r1 = Convert.ToDouble(resultMonitoringram[1].Message);
        //    RamInfo ram = new RamInfo
        //    {
        //        DateTime = DateTime.Now,
        //        BusyRam = r1,
        //        FreeRam = r,
        //    };
        //    return ram;
        //}

        //[HttpPost]
        //[Route("AddRam")]
        //public void AddRam(double t1, double t)
        //{
        //    RamInfo ram = new RamInfo
        //    {
        //        DateTime = DateTime.Now,
        //        BusyRam = t1,
        //        FreeRam = t,
        //    };
        //    dbRam.AddInDb(ram);
        //}

        //[HttpGet]
        //[Route("GetProc")]
        //public ProcInfo GetProc()
        //{
        //    var infoproc = _monitorFactory.GetMonitor(Application.Domanes.Enums.MonitoringTypes.Proc);
        //    var resultMonitoringproc = infoproc.ReceiveInfoMonitor() as List<ResultMonitoring>;
        //    ProcInfo proc = new ProcInfo
        //    {
        //        DateTime = DateTime.Now,
        //        Process = resultMonitoringproc[0].Message,
        //    };
        //    return proc;
        //}

        //[HttpPost]
        //[Route("AddProc")]
        //public void AddProc(string t)
        //{
        //    ProcInfo proc = new ProcInfo
        //    {
        //        DateTime = DateTime.Now,
        //        Process = t,
        //    };
        //    dbProc.AddInDb(proc);
        //}

        //[HttpGet]
        //[Route("GetHttp")]
        //public HttpInfo GetHttp()
        //{
        //    var infohttp = _monitorFactory.GetMonitor(Application.Domanes.Enums.MonitoringTypes.HTTP);
        //    var resultMonitoringhttp = infohttp.ReceiveInfoMonitor() as List<ResultMonitoring>;
        //    HttpInfo http = new HttpInfo
        //    {
        //        DateTime = DateTime.Now,
        //        Length = Convert.ToInt32(resultMonitoringhttp[0].Message),
        //    };
        //    return http;
        //}

        //[HttpPost]
        //[Route("AddHttp")]
        //public void AddHttp(int k)
        //{
        //    HttpInfo http = new HttpInfo
        //    {
        //        DateTime = DateTime.Now,
        //        Length = k,
        //    };
        //    dbHttp.AddInDb(http);
        //}

        //[HttpPost]
        //[Route("EditParameters")]
        //public void EditPar(string key, string value)
        //{
        //    dbData.Update(key, value);
        //}
        #endregion
    }
}
