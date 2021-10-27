using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Handler
{
    public class ExternalMonitorHandler: IExternalMonitorHandler
    {
        IMonitorFactory MonitorFactory;
        INotificationFactory NotificationFactory;
        public ExternalMonitorHandler(IMonitorFactory monitorFactory, INotificationFactory notificationFactory)
        { 
            MonitorFactory = monitorFactory;
            NotificationFactory = notificationFactory;
        }

        public void Handle()
        {
            var monitor = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.Sites);
            var notifyer = NotificationFactory.GetNotification(Domanes.Enums.NotificationTypes.Telegram);
            var resultMonitoring = monitor.ReceiveInfoMonitor() as List<ResultMonitoring>;
            foreach (var result in resultMonitoring)
            {
                if (!result.Success)
                { notifyer.SendMessage("-742266994", "!Ошибка проверки!\r\n" + result.Message); }
                else
                { notifyer.SendMessage("-742266994", "Проверка успешна\r\n" + result.Message); }
            }
        }
    }
}
