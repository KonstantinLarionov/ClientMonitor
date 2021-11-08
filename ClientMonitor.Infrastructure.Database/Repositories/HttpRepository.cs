using ClientMonitor.Application.Abstractions;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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

        public List<string> StatDb(DateTime dateTime)
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
            DateTime average = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 24, 0);
            DateTime end = average.AddDays(-1);

            if (dateTime == start)
            {
                List<string> https = new();
                https.Add((db.EHttps.Where(p => p.DateTime > end && p.DateTime < start).Sum(u => u.Length)).ToString());
                return https;
            }
            else
            {
                List<string> https = new();
                https.Add(db.EHttps.Where(p => p.DateTime > start && p.DateTime < average).Sum(u => u.Length).ToString());
                return https;
            }
        }
    }
}
