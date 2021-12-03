using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClientMonitor.BckgrndWorker
{
    public class ExternalMonitorBackgroundWorker : BackgroundService
    {
        readonly IExternalMonitorHandler _handle;
        readonly IRepository<DataForEditInfo> _db;
        private static bool isEnable = false;
        public ExternalMonitorBackgroundWorker(IExternalMonitorHandler handle, IRepository<DataForEditInfo> db)
        {
            _handle = handle;
            _db = db;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                var repository = _db;
                bool isEnable = false;
                if (repository.GetData("onOff") != "")
                {
                    isEnable = ParseBool(repository.GetData("onOff"));
                }
                int time = 3600000;
                if (repository.GetData("onOff") != "")
                {
                    isEnable = ParseBool(repository.GetData("onOff"));
                }
                if (isEnable == false)
                {
                    //получение времени с БД
                    if (repository.GetData("PeriodMonitoring") != "")
                    {
                        time = Convert.ToInt32(repository.GetData("PeriodMonitoring"));
                    }
                    _handle.Handle();
                    Thread.Sleep(time);
                }
                else
                {
                    Thread.Sleep(1000);
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
