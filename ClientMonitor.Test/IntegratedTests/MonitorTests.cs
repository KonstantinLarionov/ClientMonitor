using ClientMonitor.Application.Abstractions;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using ClientMonitor.Infrastructure.Database.Repositories;
using ClientMonitor.Infrastructure.Monitor;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
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



        IRepository<CpuInfo> dbCpu;
        protected DbContextOptions<LoggerContext> ContextOptions { get; }
        protected MonitorTests(DbContextOptions<LoggerContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        private void Seed()
        {
            using (var context = new LoggerContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var one = new CpuInfo
                {
                    DateTime = DateTime.Now,
                    BusyCpu = 1000,
                    FreeCpu = 1000,
                };
                context.AddRange(one);
                context.SaveChanges();

            }
        }

/*
        [Fact]
        public void Can_get_items()
        {
            using (var context = new LoggerContext())
            {
                var controller = new CpuRepository(context);

                var items = controller.Get().ToList();

                Assert.Equal(3, items.Count);
                Assert.Equal("ItemOne", items[0].Name);
            }
        }
*/
        [Fact]
        public void Can_add_Cpu()
        {

        }
    }
}
