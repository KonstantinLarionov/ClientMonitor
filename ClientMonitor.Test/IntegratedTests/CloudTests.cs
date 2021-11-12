using ClientMonitor.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClientMonitor.Test.IntegratedTests
{
    public class CloudTests : IClassFixture<ApplicationStanding>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly IApplicationBuilder _app;

        public CloudTests(ApplicationStanding factory)
        {
            _factory = factory;
            _app = _factory.Services.GetRequiredService<IApplicationBuilder>();
            //Тут отладка внутри проекта
        }

        [Fact]
        public void TestHandler()
        {
            try
            {
                _app.UseCloudUploading(
              cloudHandler =>
              {
                  cloudHandler.Handle();
              });
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
         }
    }
}
