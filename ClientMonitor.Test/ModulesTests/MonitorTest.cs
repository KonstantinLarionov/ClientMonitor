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
        private readonly MonitorsFactory service;
        public MonitorTest(ApplicationStanding factory)
        {
            _factory = factory;
            service = _factory.Services.GetRequiredService<IMonitorFactory>() as MonitorsFactory;
        }

        #region [TestData]
        [Fact]
        public void Cpu_Success()
        {
            var infoCpu = service.GetMonitor(MonitoringTypes.CPU);
            var resultMonitoringcpu = infoCpu.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.True(Convert.ToDouble(resultMonitoringcpu[0].Message) > 0);
            Assert.True(Convert.ToDouble(resultMonitoringcpu[1].Message) > 0);
        }

        [Fact]
        public void Ram_Succes()
        {
            var infoRam = service.GetMonitor(MonitoringTypes.RAM);
            var resultMonitoringRam = infoRam.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.True(Convert.ToDouble(resultMonitoringRam[0].Message) > 500);
            Assert.True(Convert.ToDouble(resultMonitoringRam[1].Message) > 500);
        }

        [Fact]
        public void Proc_Succes()
        {
            var infProc = service.GetMonitor(MonitoringTypes.Proc);
            var resultMonitoringcProc = infProc.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.NotNull(resultMonitoringcProc);
            Assert.True(resultMonitoringcProc[0].Message.Length>100); //размер длины списка процессов
        }
        [Fact]
        public void Sites_Succes()
        {
            var infoSites = service.GetMonitor(MonitoringTypes.Sites);
            var resultMonitoringSite = infoSites.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.True(resultMonitoringSite.Count==8);
            foreach (var s in resultMonitoringSite)
            {
                Assert.True(s.Message.Length > 10);
            }
        }
        [Fact]
        public void Servers_Succes()
        {
            var infoServers = service.GetMonitor(MonitoringTypes.Servers);
            var resultMonitoringServ = infoServers.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.True(resultMonitoringServ.Count == 3);
            foreach (var s in resultMonitoringServ)
            {
                Assert.True(s.Message.Length > 8);
            }
        }
        [Fact]
        public void Http_Succes()
        {
            var infoHttp = service.GetMonitor(MonitoringTypes.HTTP);
            var resultMonitoringHttp = infoHttp.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.True(resultMonitoringHttp.Count == 1);
            Assert.True(Convert.ToDouble(resultMonitoringHttp[0].Message)>=0);
        }

        #endregion

        [Fact]
        public void Cpu_Error()
        {
            var infoCpu = service.GetMonitor(MonitoringTypes.CPU);
            var resultMonitoringcpu = infoCpu.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.False(Convert.ToDouble(resultMonitoringcpu[0].Message) < 0);
            Assert.False(Convert.ToDouble(resultMonitoringcpu[1].Message) < 0);
        }
        [Fact]
        public void Ram_Error()
        {
            var infoRam = service.GetMonitor(MonitoringTypes.RAM);
            var resultMonitoringRam = infoRam.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.False(Convert.ToDouble(resultMonitoringRam[0].Message) < 500);
            Assert.False(Convert.ToDouble(resultMonitoringRam[1].Message) < 500);
        }
        [Fact]
        public void Proc_Error()
        {

            var infProc = service.GetMonitor(MonitoringTypes.Proc);
            var resultMonitoringcProc = infProc.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.False(resultMonitoringcProc[0].Message.Length < 100);
        }
        [Fact]
        public void Site_Error()
        {
            var infoSites = service.GetMonitor(MonitoringTypes.Sites);
            var resultMonitoringSite = infoSites.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.False(resultMonitoringSite.Count < 8);
            foreach (var s in resultMonitoringSite)
            {
                Assert.False(s.Message.Length < 8);
            }
        }
        [Fact]
        public void Servers_Error()
        {

            var infoServers = service.GetMonitor(MonitoringTypes.Servers);
            var resultMonitoringServ = infoServers.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.False(resultMonitoringServ.Count < 3);
            foreach (var s in resultMonitoringServ)
            {
                Assert.False(s.Message.Length < 7);
            }
        }

        [Fact]
        public void Http_Error()
        {
            var infoHttp = service.GetMonitor(MonitoringTypes.HTTP);
            var resultMonitoringHttp = infoHttp.ReceiveInfoMonitor() as List<ResultMonitoring>;
            Assert.True(resultMonitoringHttp.Count == 1);
            Assert.False(Convert.ToDouble(resultMonitoringHttp[0].Message)<0);
        }
    }
}
