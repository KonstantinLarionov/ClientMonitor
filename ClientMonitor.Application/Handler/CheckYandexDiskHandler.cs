using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;

using System.Diagnostics;

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
            try
            {
                var notifyer = _notificationFactory.GetNotification(NotificationTypes.Telegram);
                Process[] processList = Process.GetProcessesByName("YandexDisk2");
                if (processList.Length <= 0)
                {
                    Process.Start("YandexDisk");
                }
            }
            catch { }
        }
    }
}