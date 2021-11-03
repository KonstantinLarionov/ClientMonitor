using ClientMonitor.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.Notifications
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureNotifications(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollection).GetTypeInfo().Assembly;

            services.AddSingleton<INotificationFactory, NotificationsFactory>();
        }
    }
}
