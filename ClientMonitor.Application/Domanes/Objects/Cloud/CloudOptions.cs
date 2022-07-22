using ClientMonitor.Application.Domanes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Domanes.Objects
{
    public class CloudOptions
    {
        public string Token { get; set; }
        public string Adress { get; set; } 
        public CloudTypes Name { get; set; } = CloudTypes.YandexCloud;
        public string Path { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string BaseAddress { get; set; }
    }
}
