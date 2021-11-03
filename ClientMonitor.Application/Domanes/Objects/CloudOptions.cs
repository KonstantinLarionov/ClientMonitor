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
        public string Token { get; set; } = "AQAAAAA0xXEYAAdv3jbmZQ52CEQyv4Hw3ibzF_o";
        public string Adress { get; set; } 
        public CloudTypes Name { get; set; } = CloudTypes.YandexCloud;
        public string Path { get; set; } = "/";
    }
}
