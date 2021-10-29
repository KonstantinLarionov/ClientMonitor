using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;


namespace ClientMonitor.Infrastructure.Database.Contexts
{
    public class PcContext : DbContext
    {
        public DbSet<InfoMonitoring> InfoMonitorings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = $"MonitorDB.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
    }
}
