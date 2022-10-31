using Microsoft.Extensions.DependencyInjection;
using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Handler;

namespace ClientMonitor.Application
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureHandler(this IServiceCollection services)
        {
            services.AddSingleton<IOldFileDeleteHandler, OldFileDeleteHandler>();
            services.AddSingleton<IVideoControlHandler, VideoControlHandler>();
            services.AddSingleton<ICheckFileHandler, CheckFileHandler>();
            services.AddSingleton<ICheckHandler, CheckHandler>();
            services.AddSingleton<ICheckYandexDiskHandler, CheckYandexDiskHandler>();
        }
    }
}
