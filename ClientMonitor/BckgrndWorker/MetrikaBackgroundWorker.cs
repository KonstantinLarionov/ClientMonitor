using ClientMonitor.Application.Abstractions.Metrika;

using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClientMonitor.BckgrndWorker
{
    public class MetrikaBackgroundWorker : BackgroundService
    {
        readonly IMetrikaHandler _handle;
        public MetrikaBackgroundWorker(IMetrikaHandler handle)
        {
            _handle = handle;
        }

        /// <summary>
        /// Запуск службы записи видосов  скамер
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                DateTime dt= DateTime.Now;
                if (dt.Hour >= 21 && dt.Hour < 22)
                {
                    _handle.Handle();
                    Thread.Sleep(32400000);
                }
                Thread.Sleep(60000);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}
