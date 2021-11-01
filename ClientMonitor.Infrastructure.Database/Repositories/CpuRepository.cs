using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;


namespace ClientMonitor.Infrastructure.Database.Repositories
{
    public class CpuRepository : IRepository<CpuInfo>
    {
        private LoggerContext db;
        public CpuRepository()
        {
            db = new LoggerContext();
        }

        public void AddInDb(CpuInfo info)
        {
            db.Database.EnsureCreated();
            db.Database.Migrate();
            var mon = new EntitiesCpu
            {
                DateTime = info.DateTime,
                BusyCpu= info.BusyCpu,
                FreeCpu=info.FreeCpu,
            };

            db.ECpus.Add(mon);
            db.SaveChanges();
        }
    }
}
