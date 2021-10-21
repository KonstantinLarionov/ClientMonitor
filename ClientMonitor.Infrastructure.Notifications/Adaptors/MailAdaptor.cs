using ClientMonitor.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.Notifications.Adaptors
{
    public class MailAdaptor : INotification
    {
        public MailAdaptor()
        {

        }

        public void SendMassage(string to, string massage)
        {
            throw new NotImplementedException();
        }
    }
}
