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
using ClientMonitor.Infrastructure.Database;
using ClientMonitor.Application.Abstractions;
using ClientMonitor.Infrastructure.StreamingRecording;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.VideoControl;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace ClientMonitor
{
    public class Startup
    {
        private LoggerContext db;
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
            services.AddInfrastructureCloudManager();
            services.AddInfrastructureNotifications();
            services.AddInfrastructureScreenRecording();
            services.AddInfrastructureHandler();
            services.AddInfrastructureMonitor();
            services.AddInfrastructureVideoMonitor();
            services.AddInfrastructureDatabase();
            services.AddInfrastructureStreamingRecording();
            //services.AddInfrastructureInternalConnector();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClientMonitor", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStreamingRecording streamingRecording)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClientMonitor v1"));
            }

            streamingRecording.StartStreamingRecording();

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

            app.UseExternalMonitor(externalMonitorHandler =>
            {
                externalMonitorHandler.Handle();
            });

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

            app.UsePcMonitoringMessage(messageHandler =>
            {
                messageHandler.HandleMessageMonitoringPc();
            }
            );

            app.UseVideoControl(testCamHandler =>
            {
                testCamHandler.Handle();
            }
            );
            #endregion
        }
    }
}
