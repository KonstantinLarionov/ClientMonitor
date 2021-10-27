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
            List<ResultMonitoring> results = new List<ResultMonitoring>(); 
            var monitor = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.Sites);
            var notifyer = NotificationFactory.GetNotification(Domanes.Enums.NotificationTypes.Telegram);
            var infocpu = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.CPU);
            var inforam = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.RAM);
            var infoproc =  MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.Proc);
            var infoservers = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.Servers);

            var resultMonitoring = monitor.ReceiveInfoMonitor() as List<ResultMonitoring>;
            results.AddRange(resultMonitoring);
            var resultMonitoringcpu = infocpu.ReceiveInfoMonitor() as List<ResultMonitoring>;
            results.AddRange(resultMonitoringcpu);
            var resultMonitoringram = inforam.ReceiveInfoMonitor() as List<ResultMonitoring>;
            results.AddRange(resultMonitoringram);
            var resultMonitoringproc = infoproc.ReceiveInfoMonitor() as List<ResultMonitoring>;
            results.AddRange(resultMonitoringproc);
            var resultMonitoringservers = infoservers.ReceiveInfoMonitor() as List<ResultMonitoring>;
            results.AddRange(resultMonitoringservers);


            //foreach (var result in results)
            //{
            //    if (!result.Success)
            //    { notifyer.SendMessage("-742266994", "!Ошибка проверки!\r\n" + result.Message); }
            //    else
            //    { notifyer.SendMessage("-742266994", "Проверка успешна\r\n" + result.Message); }
            //}
            string test1 = "____";
            foreach (var result in results)
            {
                test1 = test1 + "____"+result.Message+ "\r\n";
            }
            notifyer.SendMessage("-742266994", "!Успешная проверка!\r\n" + test1);
        }
    }
}
