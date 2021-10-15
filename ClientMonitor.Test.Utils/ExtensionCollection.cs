using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace ClientMonitor.Test.Utils
{
    public static class ExtensionCollection
    {
        public static Mock<T> MockSingleton<T>(this IServiceCollection services, Action<Mock<T>> setup = default)
            where T : class
        {
            var mock = new Mock<T>();

            setup?.Invoke(mock);

            services.AddSingleton(typeof(T), mock.Object);

            return mock;
        }

        public static Mock<T> GetMock<T>(this ServiceProvider provider)
            where T : class
        {
            var instance = provider.GetService<T>();

            return Mock.Get(instance);
        }

        public static void MockSingletonIfNotExists<T>(this IServiceCollection services)
            where T : class
        {
            var descriptor = services.LastOrDefault(service => service.ServiceType == typeof(T));

            if (descriptor == null || !descriptor.IsMock<T>())
            {
                services.MockSingleton<T>();
            }
        }

        private static bool IsMock<T>(this ServiceDescriptor descriptor)
            where T : class
        {
            var instance = descriptor.ImplementationInstance;

            if ((instance as IMocked<T>) != null)
            {
                return true;
            }

            Delegate @delegate;
            if ((object)(@delegate = instance as Delegate) != null && (@delegate.Target as IMocked<T>) != null)
            {
                return true;
            }

            if ((instance as IMocked) != null)
            {
                return true;
            }

            return false;
        }
    }
}
