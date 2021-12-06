
using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace ClientMonitor.Infrastructure.CloudManager
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureCloudManager(this IServiceCollection services, IConfiguration configuration)
        {
            // TODO: Есть другой механизм по лучше, нужно будет переделать позже
            var section = configuration.GetSection("YandexCloudOptions").Get<CloudOptions>();
            services.Configure<CloudOptions>(config => 
            {
                config.BaseAddress = section.BaseAddress;
                config.Adress = section.Adress;
                config.Login = section.Login;
                config.Name = section.Name;
                config.Password = section.Password;
                config.Path = section.Path;
                config.Token = section.Token;
            });

            var assembly = typeof(ServiceCollection).GetTypeInfo().Assembly;
            services.AddAutoMapper(assembly);
            services.AddSingleton<ICloudFactory, CloudsFactory>();
        }
    }
}
