using ClientMonitor.Application.Abstractions;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientMonitor.Infrastructure.Database.Repositories
{
    public class HttpRepository : IRepository<HttpInfo>
    {
        private LoggerContext db;
        public HttpRepository()
        {
            db = new LoggerContext();
        }

        public void AddInDb(HttpInfo info)
        {
            db.Database.EnsureCreated();
            db.Database.Migrate();
            var mon = new EntitiesHttp
            {
                DateTime = info.DateTime,
                Length = info.Length,
            };

            db.EHttps.Add(mon);

            DateTime threeday = DateTime.Now.AddDays(-3);
            db.EHttps.RemoveRange(db.EHttps.Where(x => x.DateTime < threeday));
            db.SaveChanges();
        }

        public List<string> StatDb(DateTime dateTime)
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
            DateTime average = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 30, 0);
            DateTime end = average.AddDays(-1);

            if (dateTime.Hour == 6)
            {
                List<string> https = new();
                double sum = db.EHttps.Where(p => p.DateTime > end && p.DateTime < start).Sum(u => u.Length);
                sum = sum / 1024 / 1024;
                sum = Math.Round(sum, 3);
                https.Add((sum).ToString());
                return https;
            }
            else
            {
                List<string> https = new();
                double sum = db.EHttps.Where(p => p.DateTime > start && p.DateTime < average).Sum(u => u.Length);
                sum = sum / 1024 / 1024;
                sum = Math.Round(sum, 3);
                https.Add((sum).ToString());
                return https;
            }
        }
    }
}