using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
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
            try
            {
                var infocpu = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.CPU);
                var resultMonitoringcpu = infocpu.ReceiveInfoMonitor() as List<ResultMonitoring>;
                if (resultMonitoringcpu.Count == 0)
                {
                    AddInLog("Ошибка получения CPU");
                    //return;
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
            catch
            {
                AddInLog("Ошибка выполнения метода проверки CPU");
            }
        }
        public void HandleRam()
        {
            try
            {
                var inforam = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.RAM);
                var resultMonitoringram = inforam.ReceiveInfoMonitor() as List<ResultMonitoring>;
                if (resultMonitoringram.Count == 0)
                {
                    AddInLog("Ошибка получения RAM");
                    //return;
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
            catch
            {
                AddInLog("Ошибка выполнения метода проверки RAM");
            }
        }

        public void HandleProc()
        {
            try
            {
                var infoproc = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.Proc);
                var resultMonitoringproc = infoproc.ReceiveInfoMonitor() as List<ResultMonitoring>;
                if (resultMonitoringproc.Count == 0)
                {
                    AddInLog("Ошибка проверки Процессов");
                    //return;
                }
                ProcInfo proc = new ProcInfo
                {
                    DateTime = DateTime.Now,
                    Process = resultMonitoringproc[0].Message,
                };
                dbProc.AddInDb(proc);
            }
            catch
            {
                AddInLog("Ошибка выполнения метода проверки процессов");
            }
        }

        public void HandleHttp()
        {
            try
            {
                var infohttp = MonitorFactory.GetMonitor(Domanes.Enums.MonitoringTypes.HTTP);
                var resultMonitoringhttp = infohttp.ReceiveInfoMonitor() as List<ResultMonitoring>;
                if (resultMonitoringhttp.Count == 0)
                {
                    AddInLog("Ошибка проверки пакетов Http");
                    //return;
                }
                HttpInfo http = new HttpInfo
                {
                    DateTime = DateTime.Now,
                    Length = Convert.ToInt32(resultMonitoringhttp[0].Message),
                };
                dbHttp.AddInDb(http);
            }
            catch
            {
                AddInLog("Ошибка выолнения метода проверки пакетов Http");
            }
        }

        public void HandleMessageMonitoringPc()
        {
            var notifyer = NotificationFactory.GetNotification(Domanes.Enums.NotificationTypes.Telegram);
            if (notifyer == null)
            {
                AddInLog("Ошибка соединения");
                //return;
            }
            try
            {
                var resCpu = dbCpu.StatDb(DateTime.Now);
                var resRam = dbRam.StatDb(DateTime.Now);
                var resHttp = dbHttp.StatDb(DateTime.Now);
                string test = $"Статистика CPU, RAM и HTTP на {DateTime.Now}";
                string proverkaOnNull = "";
                if (resCpu.Count != 0)
                {
                    test = test + "\r\n" + $"Цп использовалось % Мин: {resCpu[0]} Max: {resCpu[1]} Сред: {resCpu[2]}";
                }
                else { proverkaOnNull = "Ошибка проверки CPU"; }
                if (resRam.Count != 0)
                {
                    test = test + "\r\n" + $"Используемая память mB Мин: {resRam[0]} Max: {resRam[1]} Сред: {resRam[2]}";
                }
                else { proverkaOnNull = proverkaOnNull + "\r\n" + "Ошибка проверки RAM"; }
                if (resHttp.Count != 0)
                {
                    test = test + "\r\n" + $"Сумма пакетов http в mB: {resHttp[0]}";
                }
                else { proverkaOnNull = proverkaOnNull + "\r\n" + "Ошибка проверки HTTP"; }
                if (proverkaOnNull != "")
                {
                    AddInLog($"Ошибка получения информации о ПК: {proverkaOnNull}");
                }

                try
                {
                    string k = dbData.GetData("IdChatMonitoring");
                    //notifyer.SendMessage(k, test);
                }
                catch {
                    //notifyer.SendMessage("-693501604", test);
                    }
            }
            catch
            {
                AddInLog("Ошибка выполнения статистики RAM|CPU|HTTP");
                try
                {
                    string k = dbData.GetData("IdChatMonitoring");
                    //notifyer.SendMessage(k, "Ошибка выполнения статистики RAM|CPU|HTTP");
                }
                catch
                {
                    //notifyer.SendMessage("-693501604", "Ошибка выполнения статистики RAM|CPU|HTTP");
                }
            }
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

        public void HandleSettings()
        {
            var a = new DataForEditInfo { Name ="PathClaim", Date = "Путь выгрузки файлов ~Выдача", Note = @"C:\Users\Big Lolipop\Desktop\Записи с камер\video\ZLOSE" };
            dbData.AddInDb(a);
            var asklad = new DataForEditInfo { Name = "PathStorage", Date = "Путь выгрузки файлов ~Склад", Note = @"C:\Users\Big Lolipop\Desktop\Записи с камер\video\KMXLM" };
            dbData.AddInDb(asklad);
            var a1 = new DataForEditInfo { Name = "FormatFile", Date = "Формат выгрузки файлов", Note = "*mp4" };
            dbData.AddInDb(a1);
            var a2 = new DataForEditInfo { Name = "PathDownloadClaim", Date = "Путь хранения файлов в облаке ~Выдача", Note = "Записи/Выдача" };
            dbData.AddInDb(a2);
            var a3 = new DataForEditInfo { Name = "PathDownloadStorage", Date = "Путь хранения файлов в облаке ~Склад", Note = "Записи/Склад" };
            dbData.AddInDb(a3);
            var amail = new DataForEditInfo { Name = "Mail", Date = "Почта для входа в облако", Note = "afc.studio@yandex.ru" };
            dbData.AddInDb(amail);
            var apas = new DataForEditInfo { Name = "Pas", Date = "Пароль для входа в облако", Note = "lollipop321123" };
            dbData.AddInDb(apas);

            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
            DateTime date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 30, 0);
            DateTime date2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 0, 0);

            var timecloud = new DataForEditInfo { Name = "TimeCloud", Date = "Время начала загрузки в облако", Note = date2.ToString() };
            dbData.AddInDb(timecloud);

            var timestart = new DataForEditInfo { Name = "TimeFirst", Date = "Время первой проверки мониторинга характеристик ПК", Note = date.ToString() };
            dbData.AddInDb(timestart);
            var timeend = new DataForEditInfo { Name = "TimeSecond", Date = "Время второй проверки мониторинга характеристик ПК", Note = date1.ToString() };
            dbData.AddInDb(timeend);

            var a5 = new DataForEditInfo { Name = "PeriodMonitoring", Date = "Периодичность мониторинга сайтов/серверов", Note = "3600000" };
            dbData.AddInDb(a5);

            var a7 = new DataForEditInfo { Name = "IdChatServer", Date = "Id чата в телеграме для отправки сообщений по мониторингу сайтов и серверов ", Note = "-742266994" };
            dbData.AddInDb(a7);
            var a8 = new DataForEditInfo { Name = "IdChatMonitoring", Date = "Id чата в телеграме для отправки сообщений по мониторингу характеристик ПК", Note = "-693501604" };
            dbData.AddInDb(a8);

        }
    }
}

