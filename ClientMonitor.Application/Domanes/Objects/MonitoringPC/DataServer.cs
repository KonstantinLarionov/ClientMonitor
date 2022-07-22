using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Domanes.Objects
{
    public class DataServer
    {
        public string IpServer { get; set; }
        public int PortServer { get; set; }
        public string GetInfo()
        {
            return ($"Ip сервера: {IpServer}  Номер порта: {PortServer}");
        }
    }
}
