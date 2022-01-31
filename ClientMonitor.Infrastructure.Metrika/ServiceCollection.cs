using ClientMonitor.Application.Abstractions.Metrika;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ClientMonitor.Infrastructure.Metrika
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureMetrika(this IServiceCollection services)
        {
            services.AddSingleton<IMetrikaFactory, MetrikaFactory>();
        }
    }
}
