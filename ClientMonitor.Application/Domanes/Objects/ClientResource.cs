using ClientMonitor.Application.Domanes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Domanes.Objects
{
    public class ClientResource
    {
        public string Name { get; set; }
        public ClientResourseType Type { get; set; }
        public string Path { get; set; }

        public string GetInfo()
        {
            return ($"Название ресурса: {Name}  Путь к ресурсу: {Path}");
        }
    }
}
