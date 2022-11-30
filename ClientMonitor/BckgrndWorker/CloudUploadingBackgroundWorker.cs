using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;

using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClientMonitor.BckgrndWorker
{
    /// <summary>
    /// Фоновая задача Загрузка в облако
    /// </summary>
    public class CloudUploadingBackgroundWorker : BackgroundService
    {
        readonly ICludUploadHendler _handle;
        private static bool isEnable = false;
        public CloudUploadingBackgroundWorker(ICludUploadHendler handle)
        {
            _handle = handle;
        }

        /// <summary>
        /// Запуск службы загрузки в облако
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                DateTime dt = DateTime.Now;
                if (dt.Hour >= 9 && dt.Hour < 11)
                {
                    await _handle.Handle();
                    Thread.Sleep(60000);
                }
                Thread.Sleep(60000);

            }
            await Task.Delay(1000, stoppingToken);
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
