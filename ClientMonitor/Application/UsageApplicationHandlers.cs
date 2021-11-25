using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Threading;

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
                    var repository = application.ApplicationServices.GetRequiredService<IRepository<DataForEditInfo>>();
                    bool isEnable = false;
                    //if (repository.GetData("onOff") != "")
                    //{
                    //    isEnable = ParseBool(repository.GetData("onOff"));
                    //}

                    if (isEnable == false)
                    {
                        if (DateTime.Now.Hour == 18 && DateTime.Now.Minute <= 2)
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
                    var repository = application.ApplicationServices.GetRequiredService<IRepository<DataForEditInfo>>();
                    bool isEnable = false;
                    int time = 3600000;
                    //if (repository.GetData("onOff") != "" && repository.GetData("PeriodMonitoring") != "")
                    //{
                    //    isEnable = ParseBool(repository.GetData("onOff"));
                    //    time = Convert.ToInt32(repository.GetData("PeriodMonitoring"));
                    //    Thread.Sleep(10000);
                    //}
                    if (isEnable == false)
                    {
                        handle.Invoke(service);
                        Thread.Sleep(time);
                    }
                    else { Thread.Sleep(1000); }
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
                        var repository = application.ApplicationServices.GetRequiredService<IRepository<DataForEditInfo>>();
                        bool isEnable = false;
                        //if (repository.GetData("onOff") != "")
                        //{
                        //    isEnable = ParseBool(repository.GetData("onOff"));
                        //    Thread.Sleep(10000);
                        //}
                        if (isEnable == false)
                        {
                            i.Invoke(service);
                            Thread.Sleep(1000);
                        }
                        else { Thread.Sleep(1000); }

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
                DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 30, 0);
                DateTime date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 30, 0);
                bool isEnable = false;
                while (true)
                {
                    var repository = application.ApplicationServices.GetRequiredService<IRepository<DataForEditInfo>>();
                    //if (repository.GetData("TimeFirst") != "" && repository.GetData("TimeSecond") != "" && repository.GetData("onOff") != "")
                    //{
                    //    isEnable = ParseBool(repository.GetData("onOff"));
                    //    Thread.Sleep(10000);
                    //}
                    if (isEnable == false)
                    {
                        DateTime dateTime = DateTime.Now;

                        if (date.Hour == dateTime.Hour && date.Minute == dateTime.Minute)
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
                }
            });
            thread.Start();
        }


        public static bool ParseBool(string input)
        {
            if (input == "True")
                return true;
            if (input == "False")
                return false;
            else return false;
        }
    }
}
