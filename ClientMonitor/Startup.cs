using ClientMonitor.Application;
using ClientMonitor.Infrastructure.Notifications;
using ClientMonitor.Infrastructure.Database;
using ClientMonitor.Infrastructure.MonitoringDomen;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ClientMonitor.BckgrndWorker;
using ClientMonitor.Infrastructure.CloudManager;
using ClientMonitor.Infrastructure.Monitor;
using ClientMonitor.Infrastructure.VideoControl;
using ClientMonitor.Infrastructure.Metrika;

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
            services.AddControllersWithViews();

            services.AddControllers();
            services.AddInfrastructureHandler();
            services.AddInfrastructureCloudManager();
            services.AddInfrastructureNotifications();
            services.AddInfrastructureMonitor();
            services.AddInfrastructureVideoMonitor();
            services.AddInfrastructureDatabase();
            //services.AddInfrastructureMonitoringDomens();
            //services.AddInfrastructureMetrika();

            //services.AddHostedService<VideoControlBackgroundWorker>();
            //services.AddHostedService<CloudUploadingBackgroundWorker>();
            //services.AddHostedService<StatPcBackgroundWorker>();
            //services.AddHostedService<ExternalMonitorBackgroundWorker>();
            //services.AddHostedService<PcMonitoringMessageBackgroundWorker>();
            //services.AddHostedService<DomenMonitorBackgroundWorker>();
            //services.AddHostedService<MetrikaBackgroundWorker>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClientMonitor", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClientMonitor v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Home}/{id?}");
            });


            #region [WorkBehind]
            app.UseCloudUploading(cloudHandler =>
            {
                cloudHandler.Handle();
            });

            app.UseCheckFile(checkFileHandler =>
            {
                checkFileHandler.CheckFileHandle();
            });

            app.UseFile(checkHandler =>
            {
                checkHandler.CheckHandle();
            });

            app.UseCheckYandexDisk(check1Handler =>
            {
                check1Handler.CheckYandexHandle();
            });


            //app.UseExternalMonitor(externalMonitorHandler =>
            //{
            //    externalMonitorHandler.Handle();
            //});

            //app.UsePcMonitoring(
            //cpuHandler =>
            //{
            //    cpuHandler.HandleCpu();
            //},
            //ramHandler =>
            //{
            //    ramHandler.HandleRam();
            //},
            //procHandler =>
            //{
            //    procHandler.HandleProc();
            //},
            // httpHandler =>
            // {
            //     httpHandler.HandleHttp();
            // }
            //);

            //app.UsePcMonitoringMessage(messageHandler =>
            //{
            //    messageHandler.HandleMessageMonitoringPc();
            //}
            //);

            app.UseVideoControl(videoControlHandler =>
            {
                videoControlHandler.Handle();
            }
            );

            //app.UseMetrika(metrikaHandler =>
            //{
            //    metrikaHandler.Handle();
            //}
            //);

            //app.UseMonitoringDomens(domensHandler =>
            //{
            //    domensHandler.Handle();
            //}
            //);
            #endregion
        }
    }
}
