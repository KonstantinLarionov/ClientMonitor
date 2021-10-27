using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
