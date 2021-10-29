﻿using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
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
        IRepository<LogInfo> db;
        public ExternalMonitorHandler(IMonitorFactory monitorFactory, INotificationFactory notificationFactory,IRepository<LogInfo> repository)
        { 
            MonitorFactory = monitorFactory;
            NotificationFactory = notificationFactory;
            db = repository;
        }

        public void Handle()
        {

            LogInfo log = new LogInfo
            {
                TypeLog = LogTypes.Information,
                Text = "Успешно",
                DateTime = DateTime.Now
            };

            db.AddInDb(log);

            List<ResultMonitoring> results = new List<ResultMonitoring>(); 
            var monitor = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.Sites);
            var notifyer = NotificationFactory.GetNotification(Domanes.Enums.NotificationTypes.Telegram);
            var infocpu = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.CPU);
            var inforam = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.RAM);
            //var infoproc =  MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.Proc);
            var infoservers = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.Servers);
            //var infohttp = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.HTTP);

            var resultMonitoring = monitor.ReceiveInfoMonitor() as List<ResultMonitoring>;
            results.AddRange(resultMonitoring);
            var resultMonitoringcpu = infocpu.ReceiveInfoMonitor() as List<ResultMonitoring>;
            results.AddRange(resultMonitoringcpu);
            var resultMonitoringram = inforam.ReceiveInfoMonitor() as List<ResultMonitoring>;
            results.AddRange(resultMonitoringram);
            var resultMonitoringservers = infoservers.ReceiveInfoMonitor() as List<ResultMonitoring>;
            results.AddRange(resultMonitoringservers);

            //foreach (var result in results)
            //{
            //    if (!result.Success)
            //    { notifyer.SendMessage("-742266994", "!Ошибка проверки!\r\n" + result.Message); }
            //    else
            //    { notifyer.SendMessage("-742266994", "Проверка успешна\r\n" + result.Message); }
            //}

            string test1 = "";
            foreach (var result in results)
            {
                test1 = test1 + "__"+result.Message+ "\r\n";
            }
            test1 = test1 + "\r\n" + "TypeLog:"+log.TypeLog +"__Status:"+ log.Text +"__Time:"+ log.DateTime;
            
            notifyer.SendMessage("-742266994", "!Успешная проверка!\r\n" + test1);
        }
    }
}
