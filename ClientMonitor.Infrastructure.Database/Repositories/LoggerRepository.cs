﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientMonitor.Infrastructure.Database.Repositories
{
    public class LoggerRepository : IRepository<LogInfo>
    {
        private LoggerContext db;
        public LoggerRepository()
        {
            db = new LoggerContext();
        }

        public void AddInDb(LogInfo info)
        {
            try
            {
                db.Database.EnsureCreated();
                //db.Database.Migrate();
                var log = new Log
                {
                    DateTime = info.DateTime,
                    TypeLog = info.TypeLog,
                    Text = info.Text
                };

                db.Logs.Add(log);
                db.SaveChanges();
            }
            catch { }
        }

        public List<string> StatDb(DateTime dateTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
