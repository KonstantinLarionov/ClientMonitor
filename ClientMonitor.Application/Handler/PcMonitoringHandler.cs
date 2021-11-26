using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.Database.Entities;
using System;
using System.Collections.Generic;

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
        IRepository<DataForEditInfo> dbData;
        public PcMonitoringHandler(IMonitorFactory monitorFactory, IRepository<LogInfo> repositoryLog, INotificationFactory notificationFactory, IRepository<CpuInfo> repositoryCpu, IRepository<RamInfo> repositoryRam, IRepository<ProcInfo> repositoryProc, IRepository<HttpInfo> repositoryHttp, IRepository<DataForEditInfo> repositoryData)
        {
            MonitorFactory = monitorFactory;
            dbLog = repositoryLog;
            dbCpu = repositoryCpu;
            dbRam = repositoryRam;
            dbProc = repositoryProc;
            dbHttp = repositoryHttp;
            dbData = repositoryData;
            NotificationFactory = notificationFactory;
        }

        public void HandleCpu()
        {

            var infocpu = MonitorFactory.GetMonitor(MonitoringTypes.CPU);
            var resultMonitoringcpu = infocpu.ReceiveInfoMonitor() as List<ResultMonitoring>;
            if (resultMonitoringcpu.Count == 0)
            {
                AddInLog("Ошибка получения CPU");
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
            var inforam = MonitorFactory.GetMonitor(MonitoringTypes.RAM);
            var resultMonitoringram = inforam.ReceiveInfoMonitor() as List<ResultMonitoring>;
            if (resultMonitoringram.Count == 0)
            {
                AddInLog("Ошибка получения RAM");
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
            var infoproc = MonitorFactory.GetMonitor(MonitoringTypes.Proc);
            var resultMonitoringproc = infoproc.ReceiveInfoMonitor() as List<ResultMonitoring>;
            if (resultMonitoringproc.Count == 0)
            {
                AddInLog("Ошибка проверки Процессов");
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
            var infohttp = MonitorFactory.GetMonitor(MonitoringTypes.HTTP);
            var resultMonitoringhttp = infohttp.ReceiveInfoMonitor() as List<ResultMonitoring>;
            if (resultMonitoringhttp.Count == 0)
            {
                AddInLog("Ошибка проверки пакетов Http");
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
            var notifyer = NotificationFactory.GetNotification(NotificationTypes.Telegram);
            string k = "-693501604";
            if (notifyer == null)
            {
                AddInLog("Ошибка соединения");
                return;
            }
            List<string> resCpu;
            List<string> resRam;
            List<string> resHttp;

            string test = $"Статистика CPU, RAM и HTTP на {DateTime.Now}";
            string proverkaOnNull = "";

            if (dbCpu.StatDb(DateTime.Now).Count != 0)
            {
                resCpu = dbCpu.StatDb(DateTime.Now);
                test = test + "\r\n" + $"Цп использовалось % Мин: {resCpu[0]} Max: {resCpu[1]} Сред: {resCpu[2]}";
            }
            else { proverkaOnNull = "Ошибка проверки CPU"; }

            if (dbRam.StatDb(DateTime.Now).Count != 0)
            {
                resRam = dbRam.StatDb(DateTime.Now);
                test = test + "\r\n" + $"Используемая память mB Мин: {resRam[0]} Max: {resRam[1]} Сред: {resRam[2]}";
            }
            else { proverkaOnNull = proverkaOnNull + "\r\n" + "Ошибка проверки RAM"; }
            if (dbHttp.StatDb(DateTime.Now).Count != 0)
            {
                resHttp = dbHttp.StatDb(DateTime.Now);
                test = test + "\r\n" + $"Сумма пакетов http в mB: {resHttp[0]}";
            }
            else { proverkaOnNull = proverkaOnNull + "\r\n" + "Ошибка проверки HTTP"; }
            if (proverkaOnNull != "")
            {
                AddInLog($"Ошибка получения информации о ПК: {proverkaOnNull}");
            }
            //if (dbData.GetData("IdChatMonitoring") != "")
            //{
            //    k = dbData.GetData("IdChatMonitoring");
            //}
            notifyer.SendMessage(k, test);
        }

        private void AddInLog(string k)
        {
            LogInfo log = new LogInfo
            {
                TypeLog = LogTypes.Error,
                Text = k,
                DateTime = DateTime.Now
            };
            dbLog.AddInDb(log);
        }
    }
}

