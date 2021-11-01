using ClientMonitor.Application.Abstractions;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

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
    }
}
