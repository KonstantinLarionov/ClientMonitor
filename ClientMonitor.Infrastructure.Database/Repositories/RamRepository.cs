using ClientMonitor.Application.Abstractions;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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

        public List<string> StatDb(DateTime dateTime)
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0);
            DateTime average = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0);
            DateTime end = average.AddDays(-1);
            if (dateTime.Hour == 8)
            {
                List<string> rams = new();
                rams.Add((Math.Round(db.ERams.Where(p => p.DateTime > end && p.DateTime < start).Min(u => u.BusyRam)), 3).ToString());
                double maxram = db.ERams.Where(p => p.DateTime > end && p.DateTime < start).Max(u => u.BusyRam);
                var dtram = db.ERams.FirstOrDefault(p => p.BusyRam == maxram);
                string ram =$"{Math.Round(maxram,3)}(Время: {dtram.DateTime})";
                rams.Add(ram);
                rams.Add((Math.Round(db.ERams.Where(p => p.DateTime > end && p.DateTime < start).Average(u => u.BusyRam)),3).ToString());
                return rams;
            }
            else
            {
                List<string> rams = new();
                rams.Add((Math.Round(db.ERams.Where(p => p.DateTime > start && p.DateTime < average).Min(u => u.BusyRam)),3).ToString());
                double maxram = db.ERams.Where(p => p.DateTime > start && p.DateTime < average).Max(u => u.BusyRam);
                var dtram = db.ERams.FirstOrDefault(p => p.BusyRam == maxram);
                string ram = $"{Math.Round(maxram,3)}(Время: {dtram.DateTime})";
                rams.Add(ram);
                rams.Add((Math.Round(db.ERams.Where(p => p.DateTime > start && p.DateTime < average).Average(u => u.BusyRam)),3).ToString());
                return rams;
            }
        }
    }
}
