using System;
using System.Net;


namespace ClientMonitor.Infrastructure.Database.Entities
{
   public class HttpInfo
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Length { get; set; }
    }
}
