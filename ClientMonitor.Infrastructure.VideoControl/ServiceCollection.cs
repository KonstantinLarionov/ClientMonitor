using ClientMonitor.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;


namespace ClientMonitor.Infrastructure.VideoControl
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureVideoMonitor(this IServiceCollection services)
        {
            services.AddSingleton<IVideoControlFactory, VideoControlFactory>();
        }
    }
}
