using ClientMonitor.Application;
using ClientMonitor.Infrastructure.Notifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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
      services.AddInfrastructureNotifications();
      services.AddInfrastructureVideoMonitor();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();


      #region [WorkBehind]
      app.UseCloudUploading(cloudHandler =>
      {
        cloudHandler.Handle();
      });
      app.UseFile(checkHandler =>
      {
        checkHandler.CheckHandle();
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
