using ClientMonitor.BckgrndWorker;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace ClientMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices(services =>
                {
                    services.AddHostedService<VideoControlBackgroundWorker>();
                    services.AddHostedService<CloudUploadingBackgroundWorker>();
                    services.AddHostedService<StatPcBackgroundWorker>();
                    services.AddHostedService<ExternalMonitorBackgroundWorker>();
                    services.AddHostedService<PcMonitoringMessageBackgroundWorker>();
                });
    }
}
