using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace ClientMonitor.BckgrndWorker
{
    public class HttpBackgroundWorker : BackgroundService
    {
        readonly IPcMonitoringHandler _handle;
        readonly IRepository<DataForEditInfo> _db;
        private static bool isEnable = false;
        public HttpBackgroundWorker(IPcMonitoringHandler handle, IRepository<DataForEditInfo> db)
        {
            _handle = handle;
            _db = db;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                var repository = _db;
                if (isEnable == false)
                {
                    _handle.HandleHttp();
                    Thread.Sleep(1000);
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