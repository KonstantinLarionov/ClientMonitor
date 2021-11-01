using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Domanes.Objects
{
    public class PcInfo
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Cpu { get; set; }
        public string Ram { get; set; }
        public string Proc { get; set; }
        public string Http { get; set; }
    }
}
