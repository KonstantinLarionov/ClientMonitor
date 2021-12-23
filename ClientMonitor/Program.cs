using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


//using ClientMonitor.BckgrndWorker;
//using Microsoft.Extensions.DependencyInjection;
//using ClientMonitor.Infrastructure.CloudManager;
//using ClientMonitor.Application;
//using ClientMonitor.Infrastructure.Notifications;
//using ClientMonitor.Infrastructure.Monitor;
//using ClientMonitor.Infrastructure.VideoControl;
//using ClientMonitor.Infrastructure.Database;

//using IHost host = Host.CreateDefaultBuilder(args)
//    .UseWindowsService(options =>
//    {
//        options.ServiceName = "ClientMonitor";
//    })
//    .ConfigureServices(services =>
//    {
//        services.AddControllersWithViews();

//        services.AddControllers();
//        services.AddInfrastructureCloudManager();
//        services.AddInfrastructureNotifications();
//        services.AddInfrastructureHandler();
//        services.AddInfrastructureMonitor();
//        services.AddInfrastructureVideoMonitor();
//        services.AddInfrastructureDatabase();

//        services.AddHostedService<VideoControlBackgroundWorker>();
//        services.AddHostedService<CloudUploadingBackgroundWorker>();
//        services.AddHostedService<StatPcBackgroundWorker>();
//        services.AddHostedService<ExternalMonitorBackgroundWorker>();
//        services.AddHostedService<PcMonitoringMessageBackgroundWorker>();
//    })
//    .Build();

//await host.RunAsync();

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
                });

        //// получаем путь к файлу 
        //public static void Main(string[] args)
        //{
        //    var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
        //    // путь к каталогу проекта
        //    var pathToContentRoot = Path.GetDirectoryName(pathToExe);
        //    // создаем хост
        //    var host = WebHost.CreateDefaultBuilder(args)
        //            .UseContentRoot(pathToContentRoot)
        //            .UseStartup<Startup>()
        //            .Build();
        //    // запускаем в виде службы
        //    host.RunAsService();
        //}
    }
}
