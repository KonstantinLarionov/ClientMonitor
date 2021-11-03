using ClientMonitor.Application.Domanes.Enums;
using System;

namespace ClientMonitor.Infrastructure.Database.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public LogTypes TypeLog { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}
