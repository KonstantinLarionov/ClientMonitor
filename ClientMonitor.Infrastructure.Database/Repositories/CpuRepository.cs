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
                BusyCpu = info.BusyCpu,
                FreeCpu = info.FreeCpu,
            };

            db.ECpus.Add(mon);

            DateTime threeday = DateTime.Now.AddDays(-3);
            if (db.ECpus.Any())
            {
                db.ECpus.RemoveRange(db.ECpus.Where(x => x.DateTime < threeday));
            }
            db.SaveChanges();
        }

        public string GetData(string old)
        {
            throw new NotImplementedException();
        }

        public List<string> StatDb(DateTime dateTime)
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
            DateTime average = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 30, 0);
            DateTime end = average.AddDays(-1);
            List<string> cpus = new();
            if (db.EDataForEdit.Any())
            {
                start = Convert.ToDateTime(db.EDataForEdit.Where(c => c.Name == "TimeFirst").Select(x => x.Value).FirstOrDefault());
                average = Convert.ToDateTime(db.EDataForEdit.Where(c => c.Name == "TimeSecond").Select(x => x.Value).FirstOrDefault());
            }

            if (dateTime.Hour == start.Hour)
            {
                cpus.Add(Math.Round(db.ECpus.Where(p => p.DateTime > end && p.DateTime < start).Min(u => u.BusyCpu), 3).ToString());
                double maxcpu = db.ECpus.Where(p => p.DateTime > end && p.DateTime < start).Max(u => u.BusyCpu);
                var dtcpu = db.ECpus.FirstOrDefault(p => p.BusyCpu == maxcpu);
                string cpu = $"{Math.Round(maxcpu, 3)}(Время: {dtcpu.DateTime})";
                cpus.Add(cpu);
                cpus.Add(Math.Round(db.ECpus.Where(p => p.DateTime > end && p.DateTime < start).Average(u => u.BusyCpu), 3).ToString());
                return cpus;
            }
            else
            {
                cpus.Add(Math.Round(db.ECpus.Where(p => p.DateTime > start && p.DateTime < average).Min(u => u.BusyCpu), 3).ToString());
                double maxcpu = db.ECpus.Where(p => p.DateTime > start && p.DateTime < average).Max(u => u.BusyCpu);
                var dtcpu = db.ECpus.FirstOrDefault(p => p.BusyCpu == maxcpu);
                string cpu = $"{Math.Round(maxcpu, 3)}(Время: {dtcpu.DateTime})";
                cpus.Add(cpu);
                cpus.Add(Math.Round(db.ECpus.Where(p => p.DateTime > start && p.DateTime < average).Average(u => u.BusyCpu), 3).ToString());
                return cpus;
            }
        }

        public void Update(string key, string news)
        {
            throw new NotImplementedException();
        }
    }
}
