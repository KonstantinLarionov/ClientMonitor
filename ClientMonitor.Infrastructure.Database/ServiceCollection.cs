using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
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
            services.AddSingleton<IRepository<CpuInfo>, CpuRepository>();
            services.AddSingleton<IRepository<RamInfo>, RamRepository>();
            services.AddSingleton<IRepository<ProcInfo>, ProcRepository>();
            services.AddSingleton<IRepository<HttpInfo>, HttpRepository>();
            //TODO : Регистрация репозитория на интерфейс IRepositories
        }
    }
}
