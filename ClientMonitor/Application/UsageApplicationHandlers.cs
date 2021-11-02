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

        public static void UsePcMonitoring(this IApplicationBuilder application, params Action<IPcMonitoringHandler>[] handlers)
        {
            foreach (var i in handlers)
            {
                Thread thread = new Thread(() =>
                {
                    var service = application.ApplicationServices.GetRequiredService<IPcMonitoringHandler>();
                    while (true)
                    {
                        i.Invoke(service);
                        Thread.Sleep(1000);
                    }
                });
                thread.Start();
            }
        }

        public static void UsePcMonitoringMessage(this IApplicationBuilder application, Action<IPcMonitoringHandler> handle)
        {
            Thread thread = new Thread(() =>
            {
                var service = application.ApplicationServices.GetRequiredService<IPcMonitoringHandler>();
                DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
                DateTime date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0);

                while (true)
                {
                        DateTime dateTime = DateTime.Now;

                        if (date.Hour== dateTime.Hour && date.Minute == dateTime.Minute)
                        {
                            handle.Invoke(service);
                            Thread.Sleep(32400000);
                        }
                        else if (date1.Hour == dateTime.Hour && date1.Minute == dateTime.Minute)
                        {
                            handle.Invoke(service);
                            Thread.Sleep(32400000);
                        }
                    Thread.Sleep(10000);
                }
            });
            thread.Start();
        }
    }
}
