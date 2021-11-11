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
            _factory = factory;
        }
    }
}
