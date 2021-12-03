using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClientMonitor.BckgrndWorker
{
    public class PcMonitoringMessageBackgroundWorker : BackgroundService
    {
        readonly IPcMonitoringHandler _handle;
        readonly IRepository<DataForEditInfo> _db;
        private static bool isEnable = false;
        public PcMonitoringMessageBackgroundWorker(IPcMonitoringHandler handle, IRepository<DataForEditInfo> db)
        {
            _handle = handle;
            _db = db;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 30, 0);
            DateTime date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 30, 0);
            while (true)
            {
                var repository = _db;
                if (isEnable == false)
                {
                    //получение времени с БД
                    if (repository.GetData("TimeFirst") != "" && repository.GetData("TimeSecond") != "")
                    {
                        date = Convert.ToDateTime(repository.GetData("TimeFirst"));
                        date1 = Convert.ToDateTime(repository.GetData("TimeSecond"));
                    }
                    DateTime dateTime = DateTime.Now;
                    if (date.Hour == dateTime.Hour && date.Minute == dateTime.Minute)
                    {
                        _handle.HandleMessageMonitoringPc();
                        Thread.Sleep(32400000);
                    }
                    else if (date1.Hour == dateTime.Hour && date1.Minute == dateTime.Minute)
                    {
                        _handle.HandleMessageMonitoringPc();
                        Thread.Sleep(32400000);
                    }
                    Thread.Sleep(10000);
                }
            }
        }

        /// <summary>
        /// Конвертирование string в bool
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ParseBool(string input)
        {
            if (input == "True")
                return true;
            if (input == "False")
                return false;
            else return false;
        }
    }
}
