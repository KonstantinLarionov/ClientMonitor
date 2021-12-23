using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using ClientMonitor.BckgrndWorker;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;

namespace ClientMonitor.Application
{
    public static class UsageApplicationHandlers
    {
        private static bool isEnable = false;

        /// <summary>
        /// Загрузка в облако
        /// </summary>
        /// <param name="application"></param>
        /// <param name="handle"></param>
        public static void UseCloudUploading(this IApplicationBuilder application, Action<ICludUploadHendler> handle)
        {
            Thread thread = new Thread(() =>
            {
                var service = application.ApplicationServices.GetRequiredService<ICludUploadHendler>();
                while (true)
                {
                    var repository = application.ApplicationServices.GetRequiredService<IRepository<DataForEditInfo>>();
                    if (repository.GetData("onOff") != "")
                    {
                        isEnable = ParseBool(repository.GetData("onOff"));
                    }
                    if (isEnable == false)
                    {
                        handle.Invoke(service);
                        Thread.Sleep(10000);
                    }
                    else
                    {
                        Thread.Sleep(60000);
                    }
                }
            });
            thread.Start();
        }
        
        /// <summary>
        /// Ежечасовая проверка сайтов и серверов
        /// </summary>
        /// <param name="application"></param>
        /// <param name="handle"></param>
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
                    if (repository.GetData("onOff") != "")
                    {
                        isEnable = ParseBool(repository.GetData("onOff"));
                        Thread.Sleep(10000);
                    }
                    if (isEnable == false)
                    {
                        //получение времени с БД
                        if (repository.GetData("PeriodMonitoring") != "")
                        {
                            time = Convert.ToInt32(repository.GetData("PeriodMonitoring"));
                        }
                        handle.Invoke(service);
                        Thread.Sleep(time);
                    }
                    else { Thread.Sleep(1000); }
                }
            });
            thread.Start();
        }

        /// <summary>
        /// Ежесекундная статистика ПК по ЦП, РАМ, ПАКЕТАМ HTTP, ПРОЦЕССАМ
        /// </summary>
        /// <param name="application"></param>
        /// <param name="handlers"></param>
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

        /// <summary>
        /// Ежедневная статистика утром и вечером за день
        /// </summary>
        /// <param name="application"></param>
        /// <param name="handle"></param>
        public static void UsePcMonitoringMessage(this IApplicationBuilder application, Action<IPcMonitoringHandler> handle)
        {
            Thread thread = new Thread(() =>
            {
                var service = application.ApplicationServices.GetRequiredService<IPcMonitoringHandler>();
                DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 30, 0);
                DateTime date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 30, 0);
                while (true)
                {
                    var repository = application.ApplicationServices.GetRequiredService<IRepository<DataForEditInfo>>();
                    if (isEnable == false)
                    {
                        //получение времени с БД
                        if (repository.GetData("TimeFirst") != "" && repository.GetData("TimeSecond") != "")
                        {
                            date = Convert.ToDateTime(repository.GetData("TimeFirst"));
                            date1 = Convert.ToDateTime(repository.GetData("TimeSecond"));
                        }
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


        /// <summary>
        /// Видеопоток
        /// </summary>
        /// <param name="application"></param>
        /// <param name="handle"></param>
        public static void UseVideoControl(this IApplicationBuilder application, Action<IVideoControlHandler> handle)
        {
            Thread thread = new Thread(() =>
            {
                var service = application.ApplicationServices.GetRequiredService<IVideoControlHandler>();
                handle.Invoke(service);
            });
            thread.Start();
        }

        /// <summary>
        /// Конвертирование string в bool
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
