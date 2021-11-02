using ClientMonitor.Application.Abstractions;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ClientMonitor.Infrastructure.Database.Repositories
{
    public class RamRepository : IRepository<RamInfo>
    {
        private LoggerContext db;
        public RamRepository()
        {
            db = new LoggerContext();
        }

        public void AddInDb(RamInfo info)
        {
            List<double> k = new();

            db.Database.EnsureCreated();
            db.Database.Migrate();
            var log = new EntitiesRam
            {
                DateTime = info.DateTime,
                BusyRam = info.BusyRam,
                FreeRam = info.FreeRam,
            };

            db.ERams.Add(log);
            db.SaveChanges();
        }

        public List<double> StatDb(DateTime dateTime)
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
            DateTime average = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0);
            DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1, 17, 1, 0);

            if (dateTime == start)
            {
                List<double> rams = new();
                rams.Add(db.ERams.Where(p => p.DateTime > end && p.DateTime < start).Min(u => u.BusyRam));
                rams.Add(db.ERams.Where(p => p.DateTime > end && p.DateTime < start).Max(u => u.BusyRam));
                rams.Add(db.ERams.Where(p => p.DateTime > end && p.DateTime < start).Average(u => u.BusyRam));
                return rams;
            }
            else
            {
                List<double> rams = new();
                rams.Add(db.ERams.Where(p => p.DateTime > start && p.DateTime < average).Min(u => u.BusyRam));
                rams.Add(db.ERams.Where(p => p.DateTime > start && p.DateTime < average).Max(u => u.BusyRam));
                rams.Add(db.ERams.Where(p => p.DateTime > start && p.DateTime < average).Average(u => u.BusyRam));
                return rams;
            }
        }
    }
}
