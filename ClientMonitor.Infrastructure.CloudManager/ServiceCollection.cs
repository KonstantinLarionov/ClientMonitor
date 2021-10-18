using ClientMonitor.Application.Abstractions;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ClientMonitor.Infrastructure.CloudManager
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureCloudManager(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollection).GetTypeInfo().Assembly;
            services.AddAutoMapper(assembly);
            services.AddSingleton<ICloudFactory, CloudsFactory>();
        }

    }
}
