using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;


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
            List<ResultMonitoring> results = new List<ResultMonitoring>(); 
            var monitor = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.Sites);
            var notifyer = NotificationFactory.GetNotification(Domanes.Enums.NotificationTypes.Telegram);
            //var infocpu = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.CPU);
            //var inforam = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.RAM);
            //var infoproc =  MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.Proc);
            var infoservers = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.Servers);
            //var infohttp = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.HTTP);

            var resultMonitoring = monitor.ReceiveInfoMonitor() as List<ResultMonitoring>;
            results.AddRange(resultMonitoring);
            //var resultMonitoringcpu = infocpu.ReceiveInfoMonitor() as List<ResultMonitoring>;
            //results.AddRange(resultMonitoringcpu);
            //var resultMonitoringram = inforam.ReceiveInfoMonitor() as List<ResultMonitoring>;
            //results.AddRange(resultMonitoringram);
            var resultMonitoringservers = infoservers.ReceiveInfoMonitor() as List<ResultMonitoring>;
            results.AddRange(resultMonitoringservers);

            string test1 = "";
            //foreach (var result in results)
            //{
            //    test1 = test1 + "__" + result.Message + "\r\n";
            //}


            foreach (var result in results)
            {
                if (!result.Success)
                { 
                    test1 = test1+"!Ошибка проверки!\r\n" + result.Message+ "r\n";
                    LogInfo log = new LogInfo
                    {
                        TypeLog = LogTypes.Error,
                        Text = result.Message,
                        DateTime = DateTime.Now
                    };

                    db.AddInDb(log);
                }
                else
                { 
                    test1 = test1+"!Проверка успешна!\r\n" + result.Message + "r\n";
                    LogInfo log = new LogInfo
                    {
                        TypeLog = LogTypes.Information,
                        Text = result.Message,
                        DateTime = DateTime.Now
                    };

                    db.AddInDb(log);
                }
            }

            //notifyer.SendMessage("-742266994", test1);

            //LogInfo log = new LogInfo
            //{
            //    TypeLog = LogTypes.Information,
            //    Text = test1,
            //    DateTime = DateTime.Now
            //};

            //db.AddInDb(log);


        }
    }
}
