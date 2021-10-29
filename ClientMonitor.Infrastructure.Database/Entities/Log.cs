using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.Database.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string TypeLog { get; set; }
        public DateTime DateTime { get; set; }
    }
}
