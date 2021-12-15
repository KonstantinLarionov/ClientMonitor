using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;

namespace ClientMonitor.Application.Handler
{
    /// <summary>
    /// Ежечасовой мониторинг сайтов и серверов, логика
    /// </summary>
    public class ExternalMonitorHandler : IExternalMonitorHandler
    {
        IMonitorFactory MonitorFactory;
        INotificationFactory NotificationFactory;
        IRepository<LogInfo> db;
        IRepository<DataForEditInfo> dbData;

        /// <summary>
        /// Подключение библиотек
        /// </summary>
        /// <param name="monitorFactory">Фабрика мониторинга</param>
        /// <param name="notificationFactory"></param>
        /// <param name="repository">Репоз логов</param>
        /// <param name="repositoryData">Репоз параметров приложения</param>
        public ExternalMonitorHandler(IMonitorFactory monitorFactory, INotificationFactory notificationFactory, IRepository<LogInfo> repository, IRepository<DataForEditInfo> repositoryData)
        {
            MonitorFactory = monitorFactory;
            NotificationFactory = notificationFactory;
            db = repository;
            dbData = repositoryData;
        }

        /// <summary>
        /// Логика
        /// </summary>
        public void Handle()
        {
            var notifyer = NotificationFactory.GetNotification(Domanes.Enums.NotificationTypes.Telegram);
            if (notifyer == null)
            {
                AddInLog("Ошибка соединения!");
            }
            string idChatServer = "-742266994";
            if (dbData.GetData("IdChatServer") != "")
            {
                idChatServer = dbData.GetData("IdChatServer");
            }
            try
            {
                List<ResultMonitoring> results = new List<ResultMonitoring>();
                var monitor = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.Sites);
                var infoservers = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.Servers);
                var resultMonitoring = monitor.ReceiveInfoMonitor() as List<ResultMonitoring>;
                results.AddRange(resultMonitoring);
                var resultMonitoringservers = infoservers.ReceiveInfoMonitor() as List<ResultMonitoring>;
                results.AddRange(resultMonitoringservers);
                string msgError = "";

                foreach (var result in results)
                {
                    if (!result.Success)
                    {
                        msgError = msgError + "!Ошибка проверки!\r\n" + result.Message + "\r\n";
                        AddInLog("!Ошибка проверки! "+result.Message);
                    }
                    else
                    {
                        AddInLog("!Проверка успешна! "+result.Message);
                    }
                }
                if (msgError != "")
                {
                    notifyer.SendMessage(idChatServer, msgError);
                }
            }
            catch
            {
                AddInLog("Ошибка выполнения метода проверки сайтов и серверов");
                notifyer.SendMessage(idChatServer, "Ошибка выполнения проверки сайтов и серверов");
            }
        }

        /// <summary>
        /// Добавление логов в бд
        /// </summary>
        /// <param name="message">Значение лога</param>
        private void AddInLog(string message)
        {
            LogInfo log = new LogInfo
            {
                TypeLog = LogTypes.Information,
                Text = message,
                DateTime = DateTime.Now
            };
            db.AddInDb(log);
        }
    }
}
