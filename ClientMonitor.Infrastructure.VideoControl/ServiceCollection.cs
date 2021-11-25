using ClientMonitor.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace ClientMonitor.Infrastructure.VideoControl
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureVideoMonitor(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollection).GetTypeInfo().Assembly;
            services.AddSingleton<IVideoControlFactory, VideoControlFactory>();
        }
    }
}
