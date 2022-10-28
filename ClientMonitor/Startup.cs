using ClientMonitor.Application;
using ClientMonitor.Infrastructure.Notifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ClientMonitor.Infrastructure.CloudManager;
using ClientMonitor.Infrastructure.VideoControl;

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
            services.AddInfrastructureVideoMonitor();

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

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Home}/{id?}");
            //});


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

            app.UseVideoControl(videoControlHandler =>
            {
                videoControlHandler.Handle();
            }
            );
            #endregion
        }
    }
}
