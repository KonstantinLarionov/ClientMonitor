﻿using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;


namespace ClientMonitor.Application.Handler
{
    public class ExternalMonitorHandler : IExternalMonitorHandler
    {
        IMonitorFactory MonitorFactory;
        INotificationFactory NotificationFactory;
        IRepository<LogInfo> db;
        public ExternalMonitorHandler(IMonitorFactory monitorFactory, INotificationFactory notificationFactory, IRepository<LogInfo> repository)
        {
            MonitorFactory = monitorFactory;
            NotificationFactory = notificationFactory;
            db = repository;
        }

        public void Handle()
        {
            var notifyer = NotificationFactory.GetNotification(Domanes.Enums.NotificationTypes.Telegram);
            if (notifyer == null)
            {
                AddInLog("Ошибка соединения!");
                //return;
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
                string test1 = "";

                foreach (var result in results)
                {
                    if (!result.Success)
                    {
                        test1 = test1 + "!Ошибка проверки!\r\n" + result.Message + "\r\n";
                        AddInLog(result.Message);
                    }
                    else
                    {
                        test1 = test1 + "!Проверка успешна!\r\n" + result.Message + "\r\n";
                        AddInLog(result.Message);
                    }
                }
                notifyer.SendMessage("-742266994", test1);
            }
            catch
            {
                AddInLog("Ошибка выполнения метода проверки сайтов и серверов");
                notifyer.SendMessage("-742266994", "Ошибка выполнения проверки сайтов и серверов");
            }
        }

        private void AddInLog(string k)
        {
            LogInfo log = new LogInfo
            {
                TypeLog = LogTypes.Information,
                Text = k,
                DateTime = DateTime.Now
            };

            db.AddInDb(log);
        }
    }
}