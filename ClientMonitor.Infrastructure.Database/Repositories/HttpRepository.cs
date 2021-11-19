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
            //удаление каждые 3 дня
            DateTime threeday = DateTime.Now.AddDays(-3);
            if (db.EHttps.Any())
            {
                db.EHttps.RemoveRange(db.EHttps.Where(x => x.DateTime < threeday));
            }
            db.SaveChanges();
        }

        public string GetData(string old)
        {
            throw new NotImplementedException();
        }

        public List<string> StatDb(DateTime dateTime)
        {
            //DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
            //DateTime average = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 30, 0);
            DateTime start = Convert.ToDateTime(db.EDataForEdit.Where(c => c.Name == "TimeFirst").FirstOrDefault());
            DateTime average = Convert.ToDateTime(db.EDataForEdit.Where(c => c.Name == "TimeSecond").FirstOrDefault());
            DateTime end = average.AddDays(-1);
            List<string> https = new();
            if (db.EHttps.Any())
            {
                if (dateTime.Hour == start.Hour)
                {
                    double sum = db.EHttps.Where(p => p.DateTime > end && p.DateTime < start).Sum(u => u.Length);
                    sum = sum / 1024 / 1024;
                    sum = Math.Round(sum, 3);
                    https.Add((sum).ToString());
                    return https;
                }
                else
                {
                    double sum = db.EHttps.Where(p => p.DateTime > start && p.DateTime < average).Sum(u => u.Length);
                    sum = sum / 1024 / 1024;
                    sum = Math.Round(sum, 3);
                    https.Add((sum).ToString());
                    return https;
                }
            }
            else { return https; }
        }
        public void Update(string key, string news)
        {
            throw new NotImplementedException();
        }
    }
}
