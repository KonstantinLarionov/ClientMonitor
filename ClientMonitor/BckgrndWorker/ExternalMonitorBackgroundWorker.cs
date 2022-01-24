using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClientMonitor.BckgrndWorker
{
    /// <summary>
    /// Фоновая задача Мониторинг сайтов и серверов
    /// </summary>
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

        /// <summary>
        /// Запуск службы проверки сайтов и серверов
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //var repository = _db;
                //if (repository.GetData("onOff") != "")
                //{
                //    isEnable = ParseBool(repository.GetData("onOff"));
                //}
                int time = 3600000;
                //if (repository.GetData("onOff") != "")
                //{
                //    isEnable = ParseBool(repository.GetData("onOff"));
                //}
                if (isEnable == false)
                {
                    //получение времени с БД
                    //if (repository.GetData("PeriodMonitoring") != "")
                    //{
                    //    time = Convert.ToInt32(repository.GetData("PeriodMonitoring"));
                    //}
                    _handle.Handle();
                    Thread.Sleep(time);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }

        /// <summary>
        /// Конвертирование string в bool
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool ParseBool(string input)
        {
            if (input == "True")
                return true;
            if (input == "False")
                return false;
            else return false;
        }
    }
}
