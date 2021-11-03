using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<double> StatDb(DateTime dateTime)
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
            DateTime average = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0);
            DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day-1, 17, 0, 0);

            if (dateTime == start)
            {
                List<double> cpus = new();
                cpus.Add(db.ECpus.Where(p => p.DateTime > end && p.DateTime < start).Min(u => u.BusyCpu));
                cpus.Add(db.ECpus.Where(p => p.DateTime > end && p.DateTime < start).Max(u => u.BusyCpu));
                cpus.Add(db.ECpus.Where(p => p.DateTime > end && p.DateTime < start).Average(u => u.BusyCpu));
                return cpus;
            }
            else
            {
                List<double> cpus = new();
                cpus.Add(db.ECpus.Where(p => p.DateTime > start && p.DateTime < average).Min(u => u.BusyCpu));
                cpus.Add(db.ECpus.Where(p => p.DateTime > start && p.DateTime < average).Max(u => u.BusyCpu));
                cpus.Add(db.ECpus.Where(p => p.DateTime > start && p.DateTime < average).Average(u => u.BusyCpu));
                return cpus;
            }      
        }
    }
}
