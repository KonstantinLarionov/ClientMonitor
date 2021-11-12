using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Infrastructure.Notifications.Adaptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.Notifications
{
   public class NotificationsFactory : INotificationFactory
    {
        private readonly Dictionary<NotificationTypes, INotification> _adaptors;

        public NotificationsFactory()
        {
            _adaptors = new Dictionary<NotificationTypes, INotification>()
            {
                {NotificationTypes.Telegram, new TelegramAdaptor() },
                {NotificationTypes.Mail, new MailAdaptor() }
            };
        }
        public INotification GetNotification(NotificationTypes type) => _adaptors.FirstOrDefault(x => x.Key == type).Value;

    }
}
