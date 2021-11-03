using ClientMonitor.Application.Abstractions;
using ClientMonitor.Infrastructure.Monitor;
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
    public class MonitorTest : IClassFixture<ApplicationStanding>
    {
        WebApplicationFactory<Startup> _factory;
        IMonitorFactory MonitorFactory;
        public MonitorTest(ApplicationStanding applicationStanding)
        {
            _factory = applicationStanding;
            MonitorFactory = _factory.Services.GetRequiredService<IMonitorFactory>() as MonitorsFactory;
        }

        [Fact]
        public void CpuAdaptorTest()
        {

        }
    }
}
