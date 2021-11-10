using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Controllers;
using ClientMonitor.Infrastructure.Database.Entities;
using ClientMonitor.Infrastructure.Monitor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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

        //интеграц выше мод
        //интеграц вызывать хендлер 
        //
        [Fact]
        public void Test()
        {
            // Arrange
            var mock = new Moq.Mock<IRepository<CpuInfo>>();
            DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0);
            mock.Setup(a => a.StatDb(now)).Returns(new List<string>());

            MonitoringController controller = new MonitoringController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result.Model);
        }      
    }
}
