using ClientMonitor.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.Monitor.Adaptors
{
    public class ServersAdaptor : IMonitor
    {
        public string ReceiveInfoMonitor()
        {
            throw new NotImplementedException();
        }

        object IMonitor.ReceiveInfoMonitor()
        {
            throw new NotImplementedException();
        }
    }
}
