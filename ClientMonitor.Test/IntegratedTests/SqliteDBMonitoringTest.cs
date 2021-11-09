using ClientMonitor.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;


namespace ClientMonitor.Test.IntegratedTests
{
    public class SqliteDBMonitoringTest : MonitorTests
    {
        public SqliteDBMonitoringTest() 
            : base(
                  new DbContextOptionsBuilder<LoggerContext>()
                  .UseSqlite("TestDb.db")
                  .Options)
        {
        }
    }
}
