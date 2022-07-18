using Microsoft.Extensions.DependencyInjection;
using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Handler;
using ClientMonitor.Application.Abstractions.Metrika;

namespace ClientMonitor.Application
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureHandler(this IServiceCollection services)
        {
            //var assembly = typeof(ServiceCollection).GetTypeInfo().Assembly;
            services.AddSingleton<ICludUploadHendler, CloudUploadHendler>();
            //services.AddSingleton<IExternalMonitorHandler, ExternalMonitorHandler>();
            //services.AddSingleton<IPcMonitoringHandler, PcMonitoringHandler>();
            services.AddSingleton<IVideoControlHandler, VideoControlHandler>();
            services.AddSingleton<ICheckFileHandler, CheckFileHandler>();
            services.AddSingleton<ICheckHandler, CheckHandler>();
            services.AddSingleton<ICheckYandexDiskHandler, CheckYandexDiskHandler>();
            //services.AddSingleton<IRegruHandler, RegruHandler>();
            //services.AddSingleton<IMetrikaHandler, MetrikaHandler>();
        }
    }
}
