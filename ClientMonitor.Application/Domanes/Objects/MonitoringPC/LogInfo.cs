using ClientMonitor.Application.Domanes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Domanes.Objects
{
    public class LogInfo
    {
        public int Id { get; set; }
        public LogTypes TypeLog { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}
