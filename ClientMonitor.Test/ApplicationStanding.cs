using ClientMonitor.Application;
using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Handler;
using ClientMonitor.Infrastructure.CloudManager;
using ClientMonitor.Infrastructure.Database;
using ClientMonitor.Infrastructure.Monitor;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace ClientMonitor.Test
{
    public class ApplicationStanding : WebApplicationFactory<Startup>
    {

        //protected override IWebHostBuilder CreateWebHostBuilder()
        //{
        //    return WebHost.CreateDefaultBuilder()
        //         .ConfigureTestServices(services =>
        //         {
        //             //TODO: Регистрация ServiceCollections

        //             //как в стартапе сделать вызывать 
        //             //проверить потоки ICloud тоже
        //             //
        //         })
        //        .UseStartup<Startup>();
        //}

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                 .ConfigureTestServices(services =>
                 {
                     services.AddSingleton<ICludUploadHendler, CloudUploadHendler>();
                     services.AddSingleton<IExternalMonitorHandler, ExternalMonitorHandler>();
                     services.AddSingleton<IPcMonitoringHandler, PcMonitoringHandler>();
                 })
                .UseStartup<Startup>();
        }
    }
}
