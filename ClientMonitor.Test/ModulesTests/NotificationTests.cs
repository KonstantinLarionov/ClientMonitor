using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.Notifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ClientMonitor.Test.IntegratedTests
{
    
    public class NotificationTests : IClassFixture<ApplicationStanding>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly NotificationsFactory service;

        public NotificationTests(ApplicationStanding factory)
        {
            _factory = factory;
            service = _factory.Services.GetRequiredService<INotificationFactory>() as NotificationsFactory;
            //Тут отладка внутри проекта
        }


        [Fact]
        public void Notification_Sucess()
        {
            try
            {
                
               var notifyer = service.GetNotification(Application.Domanes.Enums.NotificationTypes.Telegram);
                if (notifyer == null)
                {
                    Assert.True(true);
                }
                notifyer.SendMessage("-742266994", ".");
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }


        [Fact]
        public void Notificatioin_Error()
        {
            var notifyer = service.GetNotification(Application.Domanes.Enums.NotificationTypes.Telegram);
            if (notifyer == null)
            {
                Assert.True(true);
            }
        }
    }
}
