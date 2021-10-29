using System;


namespace ClientMonitor.Infrastructure.Database.Entities
{
    public class InfoMonitoring
    {
        public DateTime DateTime { get; set; }
        public string Cpu { get; set; }
        public string Ram { get; set; }
        public string Proc { get; set; }
        public string Http { get; set; }
    }
}
