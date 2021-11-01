using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.Database.Entities
{
    public class EntitiesHttp
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Length { get; set; }
    }
}
