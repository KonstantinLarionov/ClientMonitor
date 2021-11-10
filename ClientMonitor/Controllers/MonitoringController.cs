using ClientMonitor.Application.Abstractions;
using ClientMonitor.Infrastructure.Database.Entities;
using ClientMonitor.Infrastructure.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMonitor.Controllers
{
    public class MonitoringController : Controller
    {
        IRepository<CpuInfo> repo;

        public MonitoringController(IRepository<CpuInfo> r)
        {
            repo = r;
        }

        public MonitoringController()
        {
            repo = new CpuRepository();
        }

        public ActionResult Index()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0);
            repo.StatDb(now);
            base.Dispose(disposing);
        }
    }
}
