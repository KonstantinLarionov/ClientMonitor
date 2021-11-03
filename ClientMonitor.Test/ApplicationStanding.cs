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
        /// <summary>
        ///  [Trait("Category", "Unit")]
        //public class PostsControllerTests : IClassFixture<ApiFactory>
        //{
        //    private readonly WebApplicationFactory<Startup> _factory;

        //    public PostsControllerTests(ApiFactory factory)
        //    {
        //        _factory = factory;
        //    }
            /// </summary>
            /// <returns></returns>
            protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                // #if withDatabase
                //.ConfigureServices(services =>
                //{
                //    services.AddDbContext<DatabaseContext>(opt => opt.UseInMemoryDatabase("Template"));
                //})
                 // #endif
                 .ConfigureTestServices(services =>
                 {
                     services.AddSingleton<ICloudFactory, CloudsFactory>();
                 })
                .UseStartup<Startup>();
        }
    }
}
