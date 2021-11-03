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
    public class MonitorTests : IClassFixture<ApplicationStanding>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public MonitorTests(ApplicationStanding factory)
        {
            _factory = factory;
            //Тут отладка внутри проекта
        }
    }
}
