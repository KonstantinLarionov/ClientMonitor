using ClientMonitor.Infrastructure.Database.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ClientMonitor.Infrastructure.Database
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureDatabase(this IServiceCollection services)
        {
            services.AddEntityFrameworkSqlite().AddDbContext<LoggerContext>();
            //TODO : Регистрация репозитория на интерфейс IRepositories
        }
    }
}
