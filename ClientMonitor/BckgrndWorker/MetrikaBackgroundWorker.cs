using ClientMonitor.Application.Abstractions.Metrika;

using Microsoft.Extensions.Hosting;

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
            _handle.Handle();
            await Task.Delay(1000, stoppingToken);
        }
    }
}
