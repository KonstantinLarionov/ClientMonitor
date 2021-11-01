using ClientMonitor.Application;
using ClientMonitor.Infrastructure.CloudManager;
using ClientMonitor.Infrastructure.Notifications;
using ClientMonitor.Infrastructure.ScreenRecording;
using ClientMonitor.Infrastructure.Monitor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using ClientMonitor.Infrastructure.Database;
using ClientMonitor.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using ClientMonitor.Application.Domanes.Enums;

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
            services.AddInfrastructureHandler();
            services.AddInfrastructureMonitor();
            services.AddInfrastructureDatabase();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClientMonitor", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Пример работы с базой напрямую без репозитория конкретного объекта, надо убрать отсюда 
            // Ниже строчки можно перенести в конструктор репозитория, что бы база обновлялась и крафтилась если ее нет, иначе не будет подключения без создания базы
            //                db.Database.EnsureCreated();
            //                db.Database.Migrate();

           //using (var db = new LoggerContext())
           // {
           //     db.Database.EnsureCreated();
           //     db.Database.Migrate();

            //    //db.Logs.Add(new Infrastructure.Database.Entities.Log { Text = "1" });
            //    db.Logs.Add(new Infrastructure.Database.Entities.Log { DateTime = DateTime.Now, TypeLog = LogTypes.Information, Text = "test" });
            //    db.SaveChanges();
            //    //var log = db.Logs.Where(x => x.Id == 1).FirstOrDefault();
            //}


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

            #region [WorkBehind]
            //Работа с облаком и видео
            /*
            app.UseCloudUploading(cloudHandler => 
            {
                cloudHandler.Handle(); 
            });*/

            //Работа с проверкой сайтов и серверов
            app.UseExternalMonitor(externalMonitorHandler =>
            {
                externalMonitorHandler.Handle();
            });

            //проверка пк
            app.UsePcMonitoring(
            cpuHandler =>
            {
                cpuHandler.HandleCpu();
            }, 
            ramHandler => 
            {
                ramHandler.HandleRam();
            },
            procHandler =>
            {
                procHandler.HandleProc();
            },
             httpHandler =>
             {
                 httpHandler.HandleHttp();
             }
            );
            #endregion
        }
    }
}
