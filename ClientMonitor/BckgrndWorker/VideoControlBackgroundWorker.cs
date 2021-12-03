using ClientMonitor.Application.Abstractions;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace ClientMonitor.BckgrndWorker
{
    public class VideoControlBackgroundWorker : BackgroundService
    {
        readonly IVideoControlHandler _handle;

        public VideoControlBackgroundWorker(IVideoControlHandler handle)
        {
            _handle = handle;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _handle.Handle();
            return Task.CompletedTask;
        }

    }
}
