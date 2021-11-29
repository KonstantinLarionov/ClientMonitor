using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
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
            services.AddTransient<IRepository<LogInfo>, LoggerRepository>();
            services.AddTransient<IRepository<CpuInfo>, CpuRepository>();
            services.AddTransient<IRepository<RamInfo>, RamRepository>();
            services.AddTransient<IRepository<ProcInfo>, ProcRepository>();
            services.AddTransient<IRepository<HttpInfo>, HttpRepository>();
            services.AddTransient<IRepository<DataForEditInfo>, AddInDataForEdit>();
            //TODO : Регистрация репозитория на интерфейс IRepositories
        }
    }
}
