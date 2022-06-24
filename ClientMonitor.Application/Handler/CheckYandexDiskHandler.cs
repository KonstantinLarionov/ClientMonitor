using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using System;
using System.Diagnostics;
using System.IO;

namespace ClientMonitor.Application.Handler
{
    /// <summary>
    /// Проверка запущен ли яндекс диск
    /// </summary>
    public class CheckYandexDiskHandler : ICheckYandexDiskHandler
    {
        readonly INotificationFactory _notificationFactory;
        /// <summary>
        /// Подключение библиотек
        /// </summary>
        public CheckYandexDiskHandler(INotificationFactory notificationFactory)
        {
            _notificationFactory = notificationFactory;
        }

        public void CheckYandexHandle()
        {
            var notifyer = _notificationFactory.GetNotification(NotificationTypes.Telegram);
            Process[] processList = Process.GetProcessesByName("YandexDisk2");
            if (processList.Length <= 0)
            {
                notifyer.SendMessage("-742266994", $"Яндекс диск не запущен__{System.Net.Dns.GetHostName()}");
            }
        }
    }
}
