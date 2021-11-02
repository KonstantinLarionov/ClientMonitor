using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Handler
{
    public class PcMonitoringHandler : IPcMonitoringHandler
    {
        IMonitorFactory MonitorFactory;
        IRepository<CpuInfo> dbCpu;
        IRepository<RamInfo> dbRam;
        IRepository<ProcInfo> dbProc;
        IRepository<HttpInfo> dbHttp;
        INotificationFactory NotificationFactory;
        IRepository<LogInfo> dbLog;

        public PcMonitoringHandler(IMonitorFactory monitorFactory, IRepository<LogInfo> repositoryLog, INotificationFactory notificationFactory, IRepository<CpuInfo> repositoryCpu, IRepository<RamInfo> repositoryRam, IRepository<ProcInfo> repositoryProc, IRepository<HttpInfo> repositoryHttp)
        {
            MonitorFactory = monitorFactory;
            dbLog = repositoryLog;
            dbCpu = repositoryCpu;
            dbRam = repositoryRam;
            dbProc = repositoryProc;
            dbHttp = repositoryHttp;
            NotificationFactory = notificationFactory;
        }
        public void HandleCpu()
        {
            var infocpu = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.CPU);
            var resultMonitoringcpu = infocpu.ReceiveInfoMonitor() as List<ResultMonitoring>;
            if ((resultMonitoringcpu == null) && (resultMonitoringcpu.Any())) 
            {
                LogInfo log = new LogInfo
                {
                    TypeLog = LogTypes.Error,
                    Text = "Ошибка получения CPU",
                    DateTime = DateTime.Now
                };
                dbLog.AddInDb(log);
                return;
            }
            var t = Convert.ToDouble(resultMonitoringcpu[0].Message);
            var t1 = Convert.ToDouble(resultMonitoringcpu[1].Message);
            CpuInfo cp = new CpuInfo
            {
                DateTime = DateTime.Now,
                BusyCpu = t1,
                FreeCpu = t,
            };
            dbCpu.AddInDb(cp);
        }
        public void HandleRam()
        {
            var inforam = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.RAM);
            var resultMonitoringram = inforam.ReceiveInfoMonitor() as List<ResultMonitoring>;
            if ((resultMonitoringram == null) && (resultMonitoringram.Any()))
            {
                LogInfo log = new LogInfo
                {
                    TypeLog = LogTypes.Error,
                    Text = "Ошибка получения RAM",
                    DateTime = DateTime.Now
                };
                dbLog.AddInDb(log);
                return;
            }
            var r = Convert.ToDouble(resultMonitoringram[0].Message);
            var r1 = Convert.ToDouble(resultMonitoringram[1].Message);
            RamInfo ram = new RamInfo
            {
                DateTime = DateTime.Now,
                BusyRam = r1,
                FreeRam = r,
            };
            dbRam.AddInDb(ram);
        }

        public void HandleProc()
        {

            var infoproc = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.Proc);
            var resultMonitoringproc = infoproc.ReceiveInfoMonitor() as List<ResultMonitoring>;
            if ((resultMonitoringproc == null) && (resultMonitoringproc.Any()))
            {
                LogInfo log = new LogInfo
                {
                    TypeLog = LogTypes.Error,
                    Text = "Ошибка проверки Процессов",
                    DateTime = DateTime.Now
                };
                dbLog.AddInDb(log);
                return;
            }
            ProcInfo proc = new ProcInfo
            {
                DateTime = DateTime.Now,
                Process = resultMonitoringproc[0].Message,
            };
            dbProc.AddInDb(proc);
        }

        public void HandleHttp()
        {
            var infohttp = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.HTTP);
            var resultMonitoringhttp = infohttp.ReceiveInfoMonitor() as List<ResultMonitoring>;
            if ((resultMonitoringhttp == null) && (resultMonitoringhttp.Any()))
            {
                LogInfo log = new LogInfo
                {
                    TypeLog = LogTypes.Error,
                    Text = "Ошибка проверки пакетов Http",
                    DateTime = DateTime.Now
                };
                dbLog.AddInDb(log);
                return;
            }
            HttpInfo http = new HttpInfo
            {
                DateTime = DateTime.Now,
                Length = Convert.ToInt32(resultMonitoringhttp[0].Message),
            };
            dbHttp.AddInDb(http);
        }

        public void HandleMessageMonitoringPc()
        {
            var notifyer = NotificationFactory.GetNotification(Domanes.Enums.NotificationTypes.Telegram);
            if (notifyer == null) 
            {
                LogInfo log = new LogInfo
                {
                    TypeLog = LogTypes.Error,
                    Text = "Ошибка соединения",
                    DateTime = DateTime.Now
                };
                dbLog.AddInDb(log);
                return; 
            }
            var resCpu = dbCpu.StatDb(DateTime.Now);
            var resRam = dbRam.StatDb(DateTime.Now);
            var resHttp = dbHttp.StatDb(DateTime.Now);
            string test = $"Статистика CPU, RAM и HTTP на {DateTime.Now}";

            if ((resCpu != null) && (!resCpu.Any()))
            {
                test = test + "\r\n" + $"Цп использовалось % Мин: {Math.Round(resCpu[0], 3)} Max: {Math.Round(resCpu[1], 3)} Сред: {Math.Round(resCpu[2], 3)}";
            }
            else if ((resRam != null) && (!resRam.Any()))
            {
                test = test + "\r\n" + $"Используемая память mB Мин: {Math.Round(resRam[0], 3)} Max: {Math.Round(resRam[1], 3)} Сред: {Math.Round(resRam[2], 3)}";
            }
            else if ((resHttp != null) && (!resHttp.Any()))
            {
                test = test + "\r\n" + $"Сумма пакетов http в байтах: {resHttp[0]}";
            }
            else
            {
                LogInfo log = new LogInfo
                {
                    TypeLog = LogTypes.Error,
                    Text = "Ошибка получения информации о ПК",
                    DateTime = DateTime.Now
                };
                dbLog.AddInDb(log);
            }
            notifyer.SendMessage("-693501604", test);
        }
    } 
}

