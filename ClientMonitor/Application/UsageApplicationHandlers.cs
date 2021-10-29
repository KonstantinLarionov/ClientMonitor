using ClientMonitor.Application.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClientMonitor.Application
{
    public static class UsageApplicationHandlers
    {
        public static void UseCloudUploading(this IApplicationBuilder application, Action<ICludUploadHendler> handle)
        {
            Thread thread = new Thread(() =>
            {

                var service = application.ApplicationServices.GetRequiredService<ICludUploadHendler>();

                while (true)
                {
                    try
                    {
                        DateTime dateTime = DateTime.Now;
                        if (dateTime.Hour == 17 && dateTime.Minute <= 50)
                        {
                            handle.Invoke(service);
                            Thread.Sleep(85800000);
                        }
                        else
                        {
                            Thread.Sleep(10000);
                        }
                    }
                    catch (Exception ex)
                    {
                        //TODO: Send message telegram mb wait mb app off
                    }
                }
            });
            thread.Start();
        }
        public static void UseExternalMonitor(this IApplicationBuilder application, Action<IExternalMonitorHandler> handle)
        {
            Thread thread = new Thread(() =>
            {

                var service = application.ApplicationServices.GetRequiredService<IExternalMonitorHandler>();

                while (true)
                {
                    var dateTime = DateTime.Now;
                    if (dateTime.Hour >= 0)
                    {
                        handle.Invoke(service);
                    }
                    Thread.Sleep(600000);
                }
            });
            thread.Start();
        }

        public static void UsePcMonitoring(this IApplicationBuilder application, Action<IPcMonitoringHandler> handle)
        {
            Thread thread = new Thread(() =>
            {
                var service = application.ApplicationServices.GetRequiredService<IPcMonitoringHandler>();

                while (true)
                {
                    handle.Invoke(service);
                }
                Thread.Sleep(1000);
            });

        }
    }
}
