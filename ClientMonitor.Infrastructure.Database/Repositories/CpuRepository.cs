using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ClientMonitor.Infrastructure.Database.Repositories
{
    public class CpuRepository : IRepository<CpuInfo>
    {
        private LoggerContext db;
        public CpuRepository()
        {
            db = new LoggerContext();
        }
        public void AddInDb(CpuInfo info)
        {
            db.Database.EnsureCreated();
            db.Database.Migrate();
            var mon = new EntitiesCpu
            {
                DateTime = info.DateTime,
                BusyCpu= info.BusyCpu,
                FreeCpu=info.FreeCpu,
            };

            db.ECpus.Add(mon);
            db.SaveChanges();
        }

        public List<string> StatDb(DateTime dateTime)
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
            DateTime average = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0);
            DateTime end = average.AddDays(-1);

            if (dateTime == start)
            {
                List<string> cpus = new();
                double maxcpu = Math.Round(db.ECpus.Where(p => p.DateTime > end && p.DateTime < start).Min(u => u.BusyCpu), 3);
                var dtcpu = db.ECpus.Where(p => p.BusyCpu == maxcpu).Select(u => u.DateTime);
                string cpu = $"{maxcpu}({dtcpu})";
                cpus.Add(cpu);
                cpus.Add(Math.Round(db.ECpus.Where(p => p.DateTime > end && p.DateTime < start).Max(u => u.BusyCpu), 3).ToString());
                cpus.Add(Math.Round(db.ECpus.Where(p => p.DateTime > end && p.DateTime < start).Average(u => u.BusyCpu), 3).ToString());
                return cpus;
            }
            else
            {
                List<string> cpus = new();
                double maxcpu = Math.Round(db.ECpus.Where(p => p.DateTime > start && p.DateTime < average).Min(u => u.BusyCpu), 3);
                var dtcpu = db.ECpus.Where(p => p.BusyCpu == maxcpu).Select(u => u.DateTime);
                string cpu = $"{maxcpu}({dtcpu})";
                cpus.Add(cpu);
                cpus.Add(Math.Round(db.ECpus.Where(p => p.DateTime > start && p.DateTime < average).Max(u => u.BusyCpu),3).ToString());
                cpus.Add(Math.Round(db.ECpus.Where(p => p.DateTime > start && p.DateTime < average).Average(u => u.BusyCpu),3).ToString());
                return cpus;
            }      
        }
    }
}
