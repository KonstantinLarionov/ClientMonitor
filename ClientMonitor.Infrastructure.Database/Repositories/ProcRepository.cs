﻿using ClientMonitor.Application.Abstractions;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.Database.Repositories
{
   public class ProcRepository : IRepository<ProcInfo>
    {
        private LoggerContext db;
        public ProcRepository()
        {
            db = new LoggerContext();
        }

        public void AddInDb(ProcInfo info)
        {
            db.Database.EnsureCreated();
            db.Database.Migrate();
            var log = new EntitiesProc
            {
                DateTime = info.DateTime,
                Process = info.Process,
            };

            db.EProcs.Add(log);
            db.SaveChanges();
        }

    }
}