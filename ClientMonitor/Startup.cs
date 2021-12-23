using ClientMonitor.Application;
using ClientMonitor.Infrastructure.CloudManager;
using ClientMonitor.Infrastructure.Notifications;
using ClientMonitor.Infrastructure.Monitor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ClientMonitor.Infrastructure.Database;
using ClientMonitor.Infrastructure.VideoControl;
using ClientMonitor.BckgrndWorker;

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
            services.AddInfrastructureCloudManager();
            services.AddInfrastructureNotifications();
            services.AddInfrastructureHandler();
            services.AddInfrastructureMonitor();
            services.AddInfrastructureVideoMonitor();
            services.AddInfrastructureDatabase();

            services.AddHostedService<VideoControlBackgroundWorker>();
            //services.AddHostedService<CloudUploadingBackgroundWorker>();
            services.AddHostedService<StatPcBackgroundWorker>();
            services.AddHostedService<ExternalMonitorBackgroundWorker>();
            services.AddHostedService<PcMonitoringMessageBackgroundWorker>();

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

            app.UseCloudUploading(cloudHandler =>
            {
                cloudHandler.Handle();
            });
        }
    }
}
