using System;


namespace ClientMonitor.Infrastructure.Database.Entities
{
    public class ProcInfo
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Process { get; set; }
    }
}
