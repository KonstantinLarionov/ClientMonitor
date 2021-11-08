using ClientMonitor.Application.Abstractions;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 10, 0);
            if ((int)DateTime.Now.DayOfWeek == 1 || (int)DateTime.Now.DayOfWeek == 4 && DateTime.Now == start)
            {
                db.Database.EnsureDeleted();
                Thread.Sleep(60000);
            }
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

        public List<string> StatDb(DateTime start)
        {
            throw new System.NotImplementedException();
        }

    }
}
