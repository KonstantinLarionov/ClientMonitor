using ClientMonitor.Application.Abstractions;
using ClientMonitor.Infrastructure.CloudManager;
using Microsoft.AspNetCore;
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

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                 .ConfigureTestServices(services =>
                 {
                     //TODO: Регистрация ServiceCollections
                 })
                .UseStartup<Startup>();
        }
    }
}
