using ClientMonitor.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ClientMonitor.Infrastructure.Monitor
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureMonitor(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollection).GetTypeInfo().Assembly;

            services.AddSingleton<IMonitorFactory, MonitorFactory>();
        }

    }
}
