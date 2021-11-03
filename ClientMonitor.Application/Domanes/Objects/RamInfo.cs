using System;


namespace ClientMonitor.Infrastructure.Database.Entities
{
    public class RamInfo
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public double BusyRam { get; set; }
        public double FreeRam { get; set; }
    }
}
