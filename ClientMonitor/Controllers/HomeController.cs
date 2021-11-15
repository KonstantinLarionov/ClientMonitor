using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMonitor.Controllers
{

    //[ApiController]
    //[Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private readonly IMonitorFactory MonitorFactory;
        //private LoggerContext db = new LoggerContext();
        IRepository<DataForEdit> db;

        public List<DataForEdit> Data { get; set; }
        public HomeController(ILogger<HomeController> logger, IMonitorFactory monitorFactory)
        {
            _logger = logger;
            MonitorFactory = monitorFactory;
        }

        public IActionResult Home()
        {
            var entities = new LoggerContext();
            ViewBag.DataForEdit = entities.EDataForEdit.ToList();
            //List<DataForEdit> listdata = new List<DataForEdit>();
            return View();
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.DataId = id;
            return View();
        }
        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо," + purchase.Person + ", за покупку!";
        }









        //    [HttpGet]
        //    [Route("GetCpu")]
        //    public CpuInfo GetCpu()
        //    {
        //        var infocpu = MonitorFactory.GetMonitor(Application.Domanes.Enums.MonitoringTypes.CPU);
        //        var resultMonitoringcpu = infocpu.ReceiveInfoMonitor() as List<ResultMonitoring>;
        //        var t = Convert.ToDouble(resultMonitoringcpu[0].Message);
        //        var t1 = Convert.ToDouble(resultMonitoringcpu[1].Message);
        //        CpuInfo cp = new CpuInfo
        //        {
        //            DateTime = DateTime.Now,
        //            BusyCpu = t1,
        //            FreeCpu = t,
        //        };

        //        return cp;
        //    }

        //    [HttpGet]
        //    [Route("GetRam")]
        //    public RamInfo GetRam()
        //    {
        //        var inforam = MonitorFactory.GetMonitor(Application.Domanes.Enums.MonitoringTypes.RAM);
        //        var resultMonitoringram = inforam.ReceiveInfoMonitor() as List<ResultMonitoring>;
        //        var r = Convert.ToDouble(resultMonitoringram[0].Message);
        //        var r1 = Convert.ToDouble(resultMonitoringram[1].Message);
        //        RamInfo ram = new RamInfo
        //        {
        //            DateTime = DateTime.Now,
        //            BusyRam = r1,
        //            FreeRam = r,
        //        };

        //        return ram;
        //    }

        //    [HttpGet]
        //    [Route("GetProc")]
        //    public ProcInfo GetProc()
        //    {
        //        var infoproc = MonitorFactory.GetMonitor(Application.Domanes.Enums.MonitoringTypes.Proc);
        //        var resultMonitoringproc = infoproc.ReceiveInfoMonitor() as List<ResultMonitoring>;
        //        ProcInfo proc = new ProcInfo
        //        {
        //            DateTime = DateTime.Now,
        //            Process = resultMonitoringproc[0].Message,
        //        };

        //        return proc;
        //    }

        //    [HttpGet]
        //    [Route("GetHttp")]
        //    public HttpInfo GetHttp()
        //    {
        //        var infohttp = MonitorFactory.GetMonitor(Application.Domanes.Enums.MonitoringTypes.HTTP);
        //        var resultMonitoringhttp = infohttp.ReceiveInfoMonitor() as List<ResultMonitoring>;
        //        HttpInfo http = new HttpInfo
        //        {
        //            DateTime = DateTime.Now,
        //            Length = Convert.ToInt32(resultMonitoringhttp[0].Message),
        //        };
        //        return http;
        //    }
    }
    }
