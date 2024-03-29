﻿using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Abstractions.Metrika;
using ClientMonitor.Application.Domanes;

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
                    try
                    {
                        if (DateTime.Now.Hour == 8)
                        {
                            handle.Invoke(service);
                            Thread.Sleep(3600000);
                        }
                        else
                        {
                            Thread.Sleep(600000);
                        }
                    }
                    catch { }
                }
            });
            thread.Start();
        }

        /// <summary>
        /// проверка на запуск яндекс диска
        /// </summary>
        /// <param name="application"></param>
        /// <param name="handle"></param>
        public static void UseCheckYandexDisk(this IApplicationBuilder application, Action<ICheckYandexDiskHandler> handle)
        {
            Thread thread = new Thread(() =>
            {
                var service = application.ApplicationServices.GetRequiredService<ICheckYandexDiskHandler>();
                while (true)
                {
                    handle.Invoke(service);
                    Thread.Sleep(3600000);
                }
            });
            thread.Start();
        }

        /// <summary>
        /// Загрузка в облако
        /// </summary>
        /// <param name="application"></param>
        /// <param name="handle"></param>
        public static void UseCheckFile(this IApplicationBuilder application, Action<ICheckFileHandler> handle)
        {
            Thread thread = new Thread(() =>
            {
                var service = application.ApplicationServices.GetRequiredService<ICheckFileHandler>();
                while (true)
                {
                    try
                    {
                        if (DateTime.Now.Hour == 0)
                        {
                            if (DateTime.Now.Minute > 10)
                            {
                                handle.Invoke(service);
                                Thread.Sleep(60000);
                            }
                        }
                        else
                        {
                            Thread.Sleep(60000);
                        }
                    }
                    catch { }
                }
            });
            thread.Start();
        }

        /// <summary>
        /// Проверка записи
        /// </summary>
        /// <param name="application"></param>
        /// <param name="handle"></param>
        public static void UseFile(this IApplicationBuilder application, Action<ICheckHandler> handle)
        {
            Thread thread = new Thread(() =>
            {
                var service = application.ApplicationServices.GetRequiredService<ICheckHandler>();
                while (true)
                {
                    try
                    {
                        if (DateTime.Now.Hour > 2)
                        {
                            handle.Invoke(service);
                            Thread.Sleep(3600000);
                        }
                        else
                        {
                            Thread.Sleep(3600000);
                        }
                    }
                    catch { }
                }
            });
            thread.Start();
        }

        /// <summary>
        /// Ежечасовая проверка сайтов и серверов
        /// </summary>
        /// <param name="application"></param>
        /// <param name="handle"></param>
        //public static void UseExternalMonitor(this IApplicationBuilder application, Action<IExternalMonitorHandler> handle)
        //{
        //    Thread thread = new Thread(() =>
        //    {
        //        var service = application.ApplicationServices.GetRequiredService<IExternalMonitorHandler>();
        //        while (true)
        //        {
        //            int time = 3600000;
        //            handle.Invoke(service);
        //            Thread.Sleep(time);

        //        }
        //    });
        //    thread.Start();
        //}

        /// <summary>
        /// Ежесекундная статистика ПК по ЦП, РАМ, ПАКЕТАМ HTTP, ПРОЦЕССАМ
        /// </summary>
        /// <param name="application"></param>
        /// <param name="handlers"></param>
        //public static void UsePcMonitoring(this IApplicationBuilder application, params Action<IPcMonitoringHandler>[] handlers)
        //{
        //    foreach (var i in handlers)
        //    {
        //        Thread thread = new Thread(() =>
        //        {
        //            var service = application.ApplicationServices.GetRequiredService<IPcMonitoringHandler>();
        //            while (true)
        //            {
        //                i.Invoke(service);
        //                Thread.Sleep(1000);
        //            }
        //        });
        //        thread.Start();
        //    }
        //}

        /// <summary>
        /// Ежедневная статистика утром и вечером за день
        /// </summary>
        /// <param name="application"></param>
        /// <param name="handle"></param>
        //public static void UsePcMonitoringMessage(this IApplicationBuilder application, Action<IPcMonitoringHandler> handle)
        //{
        //    Thread thread = new Thread(() =>
        //    {
        //        var service = application.ApplicationServices.GetRequiredService<IPcMonitoringHandler>();
        //        DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 30, 0);
        //        DateTime date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 30, 0);
        //        while (true)
        //        {
        //            var repository = application.ApplicationServices.GetRequiredService<IRepository<DataForEditInfo>>();

        //            DateTime dateTime = DateTime.Now;
        //            if (date.Hour == dateTime.Hour && dateTime.Minute > 30)
        //            {
        //                handle.Invoke(service);
        //                Thread.Sleep(32400000);
        //            }
        //            else if (date1.Hour == dateTime.Hour && dateTime.Minute > 30)
        //            {
        //                handle.Invoke(service);
        //                Thread.Sleep(32400000);
        //            }
        //            Thread.Sleep(10000);

        //        }
        //    });
        //    thread.Start();
        //}


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
        /// Видеопоток
        /// </summary>
        /// <param name="application"></param>
        /// <param name="handle"></param>
        //public static void UseMonitoringDomens(this IApplicationBuilder application, Action<IRegruHandler> handle)
        //{
        //    Thread thread = new Thread(() =>
        //    {
        //        var service = application.ApplicationServices.GetRequiredService<IRegruHandler>();
        //        while (true)
        //        {
        //            if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour<=14)
        //            {
        //                handle.Invoke(service);
        //                Thread.Sleep(3600000);
        //            }
        //            Thread.Sleep(2400000);
        //        }
        //    });
        //    thread.Start();
        //}

        /// <summary>
        /// Видеопоток
        /// </summary>
        /// <param name="application"></param>
        /// <param name="handle"></param>
        //public static void UseMetrika(this IApplicationBuilder application, Action<IMetrikaHandler> handle)
        //{
        //    Thread thread = new Thread(() =>
        //    {
        //        var service = application.ApplicationServices.GetRequiredService<IMetrikaHandler>();
        //        while (true)
        //        {
        //            if (DateTime.Now.Hour >= 22)
        //            {
        //                handle.Invoke(service);
        //                Thread.Sleep(3600000);
        //            }
        //            Thread.Sleep(2400000);
        //        }
        //    });
        //    thread.Start();
        //}
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
