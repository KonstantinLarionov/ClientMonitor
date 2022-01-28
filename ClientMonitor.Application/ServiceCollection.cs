using Microsoft.Extensions.DependencyInjection;
using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Handler;


namespace ClientMonitor.Application
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureHandler(this IServiceCollection services)
        {
            //var assembly = typeof(ServiceCollection).GetTypeInfo().Assembly;
            services.AddSingleton<ICludUploadHendler, CloudUploadHendler>();
            services.AddSingleton<IExternalMonitorHandler, ExternalMonitorHandler>();
            services.AddSingleton<IPcMonitoringHandler, PcMonitoringHandler>();
            services.AddSingleton<IVideoControlHandler, VideoControlHandler>();
            services.AddSingleton<IRegruHandler, RegruHandler>();
        }
    }
}
