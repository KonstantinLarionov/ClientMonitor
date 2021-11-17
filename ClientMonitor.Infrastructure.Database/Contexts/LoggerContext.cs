using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClientMonitor.Infrastructure.Database.Contexts
{
    public class LoggerContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        public DbSet<EntitiesRam> ERams { get; set; }
        public DbSet<EntitiesCpu> ECpus { get; set; }
        public DbSet<EntitiesProc> EProcs { get; set; }
        public DbSet<EntitiesHttp> EHttps { get; set; }
        public DbSet<DataForEdit> EDataForEdit { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = $"MonitorDB.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
             
        }
    }
}
