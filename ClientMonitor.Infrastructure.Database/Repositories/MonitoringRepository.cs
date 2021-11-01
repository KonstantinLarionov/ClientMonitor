﻿using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;


namespace ClientMonitor.Infrastructure.Database.Repositories
{
    public class MonitoringRepository : IRepository<PcInfo>
    {
        private LoggerContext db;
        public MonitoringRepository()
        {
            db = new LoggerContext();
        }

        public void AddInDb(PcInfo info)
        {
            db.Database.EnsureCreated();
            db.Database.Migrate();
            var mon = new InfoMonitoring
            {
                DateTime = info.DateTime,
                Cpu = info.Cpu,
                Ram=info.Ram,
                Proc=info.Proc,
                Http=info.Http,
            };

            db.InfoMonitorings.Add(mon);
            db.SaveChanges();
        }
    }
}
