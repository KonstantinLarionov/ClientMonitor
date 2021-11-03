using ClientMonitor.Application.Abstractions;
using ClientMonitor.Infrastructure.CloudManager;
using ClientMonitor.Infrastructure.CloudManager.Adaptors;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ClientMonitor.Test.ModulesTests
{
    public class CloudTests : IClassFixture<ApplicationStanding>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public CloudTests(ApplicationStanding factory)
        {
           // var a = new YandexAdaptor(,);
            _factory = factory;
            //_factory.Services.GetService(YandexAdaptor.ReferenceEquals);
            //Пример получения сервиса для теста (нужно сделать глобальным и писать тесты с ним)
            //var service = _factory.Services.GetRequiredService<ICloudFactory>() as CloudsFactory;
        }

        [Fact]
        public void NameTest_Success()
        {
            


           // var res = GetFilesAndFoldersAsync();
        }
        [Fact]
        public void NameTest_Error()
        {

        }
    }
}
