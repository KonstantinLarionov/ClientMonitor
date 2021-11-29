using ClientMonitor.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace ClientMonitor.Infrastructure.Notifications
{
    public static class ServiceCollection
    {
        /// <summary>
        /// Добавление инфраструктуры уведомлений
        /// </summary>
        /// <param name="services">Сервис</param>
        public static void AddInfrastructureNotifications(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollection).GetTypeInfo().Assembly;
            services.AddSingleton<INotificationFactory, NotificationsFactory>();
        }
    }
}
