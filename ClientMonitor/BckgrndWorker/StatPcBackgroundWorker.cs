using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace ClientMonitor.BckgrndWorker
{
    public class StatPcBackgroundWorker : BackgroundService
    {
        readonly IPcMonitoringHandler _handle;
        readonly IRepository<DataForEditInfo> _db;
        private static bool isEnable = false;
        public StatPcBackgroundWorker(IPcMonitoringHandler handle, IRepository<DataForEditInfo> db)
        {
            _handle = handle;
            _db = db;
        }

        /// <summary>
        /// Запуск службы мониторинга CPU,RAM,Proc,Http
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var repository = _db;
                if (repository.GetData("onOff") != "")
                {
                    isEnable = ParseBool(repository.GetData("onOff"));
                }
                if (isEnable == false)
                {
                    _handle.HandleCpu();
                    _handle.HandleRam();
                    _handle.HandleHttp();
                    _handle.HandleProc();
                }
                await Task.Delay(100, stoppingToken);
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
