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
        private readonly WebApplicationFactory<Startup> _factory;

        public CloudTests(ApplicationStanding factory)
        {
            _factory = factory;
            //Тут отладка внутри проекта
        }

    }
}
