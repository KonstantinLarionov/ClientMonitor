using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.Database.Entities;
using ClientMonitor.Infrastructure.Monitor;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace ClientMonitor.Test.ModulesTests
{
    public class MonitorTest : IClassFixture<ApplicationStanding>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public MonitorTest(ApplicationStanding factory)
        {
            _factory = factory;
        }

        [Fact]
        public void Cpu_Success()
        {
            //чето надо написать 
            var service = _factory.Services.GetRequiredService<IMonitorFactory>() as MonitorsFactory;
            var infoCpu = service.GetMonitor(MonitoringTypes.CPU);
            var resultMonitoringcpu = infoCpu.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.NotNull(resultMonitoringcpu);  
        }

        [Fact]
        public void Ram_Succes()
        {
            var service = _factory.Services.GetRequiredService<IMonitorFactory>() as MonitorsFactory;
            var infoRam = service.GetMonitor(MonitoringTypes.RAM);
            var resultMonitoringRam = infoRam.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.NotNull(resultMonitoringRam);
        }
        [Fact]
        public void Proc_Succes()
        {
            var service = _factory.Services.GetRequiredService<IMonitorFactory>() as MonitorsFactory;
            var infProc = service.GetMonitor(MonitoringTypes.Proc);
            var resultMonitoringcProc = infProc.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.NotNull(resultMonitoringcProc);
        }
        [Fact]
        public void Sites_Succes()
        {
            var service = _factory.Services.GetRequiredService<IMonitorFactory>() as MonitorsFactory;
            var infoSites = service.GetMonitor(MonitoringTypes.Sites);
            var resultMonitoringSite = infoSites.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.NotNull(resultMonitoringSite);
        }
        [Fact]
        public void Servers_Succes()
        {
            var service = _factory.Services.GetRequiredService<IMonitorFactory>() as MonitorsFactory;
            var infoServers = service.GetMonitor(MonitoringTypes.Servers);
            var resultMonitoringServ = infoServers.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.NotNull(resultMonitoringServ);
        }
        [Fact]
        public void Http_Succes()
        {
            var service = _factory.Services.GetRequiredService<IMonitorFactory>() as MonitorsFactory;
            var infoHttp = service.GetMonitor(MonitoringTypes.HTTP);
            var resultMonitoringHttp = infoHttp.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.NotNull(resultMonitoringHttp);
        }


        [Fact]
        public void NameTest_Error()
        {

        }
    }
}
