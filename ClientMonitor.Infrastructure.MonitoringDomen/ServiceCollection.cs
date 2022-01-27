using ClientMonitor.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ClientMonitor.Infrastructure.MonitoringDomen
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureMonitoringDomens(this IServiceCollection services)
        {
            services.AddSingleton<IRegruHandlerFactory, RegruHandlerFactory>();
        }
    }
}
