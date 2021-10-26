using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.CloudManager;
using ClientMonitor.Infrastructure.Notifications;
using ClientMonitor.Infrastructure.ScreenRecording;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMonitor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddInfrastructureCloudManager();
            services.AddInfrastructureNotifications();
            services.AddInfrastructureScreenRecording();

            services.AddInfrastructureMonitor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClientMonitor", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMonitorFactory mon)
        {

            //var yandex = cloud.GetCloud(Application.Domanes.Enums.CloudTypes.YandexCloud);
            //var test = yandex.GetFilesAndFoldersAsync();
            //var test1 = yandex.UploadFiles(new UploadedFilesInfo());
            //var tg = mas.GetNotification(Application.Domanes.Enums.NotificationTypes.Telegram);
            //tg.SendMassage("398615402","привет");

            //sr.StartScreenRecording();
            
            //var test = mon.GetMonitor(Application.Domanes.Enums.MonitoringTypes.CPU);
            //string s = mon.ReceiveInfoMonitor();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClientMonitor v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
