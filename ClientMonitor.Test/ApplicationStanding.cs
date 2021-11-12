using ClientMonitor.Application;
using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Handler;
using ClientMonitor.Infrastructure.CloudManager;
using ClientMonitor.Infrastructure.Database;
using ClientMonitor.Infrastructure.Monitor;
using ClientMonitor.Infrastructure.Notifications;
using ClientMonitor.Infrastructure.ScreenRecording;
using ClientMonitor.Infrastructure.StreamingRecording;
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
        //             //TODO: ����������� ServiceCollections

        //             //��� � �������� ������� �������� 
        //             //��������� ������ ICloud ����
        //             //
        //         })
        //        .UseStartup<Startup>();
        //}

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                 .ConfigureTestServices(services =>
                 {
                     services.AddInfrastructureCloudManager();
                     services.AddInfrastructureNotifications();
                     //services.AddInfrastructureScreenRecording();
                     services.AddInfrastructureHandler();
                     services.AddInfrastructureMonitor();
                     services.AddInfrastructureDatabase();
                     //services.AddInfrastructureStreamingRecording();

                 })
                .UseStartup<Startup>();
        }
    }
}
