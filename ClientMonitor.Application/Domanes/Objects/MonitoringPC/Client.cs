using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Domanes.Objects
{
    public class Client
    {
        public string ClientName { get; set; }
        public DateTime DateCreate { get; set; }
        public List<ClientResource> ClientResources { get; set; }

        public string GetInfo()
        {
            return ($"Имя клиента: {ClientName}  Дата регистрации: {DateCreate}");
        }
    }
}
