﻿using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;

namespace ClientMonitor.Application
{
    public static class UsageApplicationHandlers
    {
        public static void UseDataEdit(this IApplicationBuilder application, Action<IPcMonitoringHandler> handle)
        {
            var service = application.ApplicationServices.GetRequiredService<IPcMonitoringHandler>();
            handle.Invoke(service);
        }
        public static void UseCloudUploading(this IApplicationBuilder application, Action<ICludUploadHendler> handle)
        {
            Thread thread = new Thread(() =>
            {

                var service = application.ApplicationServices.GetRequiredService<ICludUploadHendler>();
                var repository = application.ApplicationServices.GetRequiredService<IRepository<DataForEditInfo>>();

                while (true)
                {
                    var onOffData = repository.GetData("onOff");
                    bool isEnable = Convert.ToBoolean(onOffData);
                    if (isEnable)
                    {
                        DateTime dateTime = DateTime.Now;
                        if (dateTime.Hour == 20 && dateTime.Minute <= 2)
                        {
                            handle.Invoke(service);
                            Thread.Sleep(85800000);
                        }
                        else
                        {
                            Thread.Sleep(10000);
                        }
                    }
                    else { Thread.Sleep(10000); }
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
                    handle.Invoke(service);
                    Thread.Sleep(3600000);
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
                DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
                DateTime date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 30, 0);

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
