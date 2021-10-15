using ClientMonitor.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.CloudManager
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureCloudManager(this IServiceCollection services)
        {
            services.AddSingleton<ICloudFactory, CloudsFactory>();
        }
    }
}
