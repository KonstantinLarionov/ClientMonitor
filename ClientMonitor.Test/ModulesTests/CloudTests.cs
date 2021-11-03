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

namespace ClientMonitor.Test.ModulesTests
{
    public class CloudTests : IClassFixture<ApplicationStanding>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public CloudTests(ApplicationStanding factory)
        {
            _factory = factory;
            //Пример получения сервиса для теста (нужно сделать глобальным и писать тесты с ним)
            var service = _factory.Services.GetRequiredService<ICloudFactory>() as CloudsFactory;
        }

        [Fact]
        public void NameTest_Success()
        {

        }
        [Fact]
        public void NameTest_Error()
        {

        }
    }
}
