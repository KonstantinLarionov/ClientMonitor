using ClientMonitor.Application.Abstractions;
using ClientMonitor.Infrastructure.CloudManager;
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
        WebApplicationFactory<Startup> _factory;
        ICloudFactory CloudFactory;
        public CloudTests(ApplicationStanding applicationStanding)
        {
            _factory = applicationStanding;
            CloudFactory = _factory.Services.GetRequiredService<ICloudFactory>() as CloudsFactory;
        }

        [Fact]
        public void GetFilesAndFoldersTest()
        { 
            
        }
    }
}
