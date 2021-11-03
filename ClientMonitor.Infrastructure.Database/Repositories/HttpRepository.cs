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
            db.SaveChanges();
        }

        public List<double> StatDb(DateTime dateTime)
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 30, 0);
            DateTime average = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0);
            DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1, 17, 1, 0);

            if (dateTime == start)
            {
                List<double> https = new();
                https.Add(db.EHttps.Where(p => p.DateTime > end && p.DateTime < start).Sum(u => u.Length));
                return https;
            }
            else
            {
                List<double> https = new();
                https.Add(db.EHttps.Where(p => p.DateTime > start && p.DateTime < average).Sum(u => u.Length));
                return https;
            }
        }
    }
}
