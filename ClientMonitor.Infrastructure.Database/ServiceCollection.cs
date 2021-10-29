using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ClientMonitor.Infrastructure.Database
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureDatabase(this IServiceCollection services)
        {
            services.AddEntityFrameworkSqlite().AddDbContext<LoggerContext>();
            services.AddSingleton<IRepository<LogInfo>, LoggerRepository>();
            //TODO : Регистрация репозитория на интерфейс IRepositories
        }
    }
}
