﻿using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;

using System;
using System.Collections.Generic;
using System.IO;

namespace ClientMonitor.Application.Handler
{
    /// <summary>
    /// /Проверка размера файлов
    /// </summary>
    public class CheckFileHandler : ICheckFileHandler
    {
        INotificationFactory NotificationFactory;
        /// <summary>
        /// Подключение библиотек
        /// </summary>
        public CheckFileHandler(INotificationFactory notificationFactory) 
        {
            NotificationFactory = notificationFactory;
        }

        public void CheckFileHandle()
        {
            var notifyer = NotificationFactory.GetNotification(NotificationTypes.Telegram);
            foreach (var listClouds in _listClouds)
            {
                if (Directory.Exists(listClouds.LocDownloadVideo))
                {
                    DateTime dt = DateTime.Now;
                    string[] allFoundFiles = Directory.GetFiles(listClouds.LocDownloadVideo + "\\" + MonthStats(dt), "", SearchOption.AllDirectories);
                    int i = 0;
                    foreach (string file in allFoundFiles)
                    {
                        FileInfo fi = new FileInfo(file);
                        if (fi.Length < 3072)
                        {
                            i++;
                        }
                    }
                    if (i>30)
                    {
                        notifyer.SendMessage("-742266994", listClouds.Name+" Не пишет видосики");
                    }
                }
            }
        }


        /// <summary>
        /// Список параметров для выгрузки в облако
        /// </summary>
        private readonly static List<ListDownloadCloud> _listClouds = new List<ListDownloadCloud>()
        {
            //new ListDownloadCloud
            //{
            //    Name="Озон-ПГ-Зал",
            //    LocDownloadVideo=@"C:\Test\Баг2",
            //    LocDownloadCloud=@"C:\Test\Баг",
            //    FormatFiles="*.avi",
            //},
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Зал",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Ozon\Зал",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Зал",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Тамбур",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Ozon\Тамбур",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Тамбур",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Выдача",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Ozon\Выдача",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Выдача",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Склад",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Ozon\Склад",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Склад",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Склад-2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Ozon\Склад2",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Склад2",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Тамбур-2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Ozon\Тамбур2",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Тамбур2",
                FormatFiles="*.avi",
            },

            new ListDownloadCloud
            {
                Name="WB-ПГ-Выдача",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Wildberries\Выдача",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Выдача",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-ПГ-Выдача2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Wildberries\Выдача2",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Выдача2",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-ПГ-Склад",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Wildberries\Склад",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Склад",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Ломбард1-ПГ",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Ломбард\Ломбард1",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ломбард\Ломбард1",
                FormatFiles="*.avi",
            },
        };

        /// <summary>
        /// Получение названия папки по дате
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static string MonthStats(DateTime dateTime)
        {
            DateTime twoday = dateTime.AddDays(-1);
            MonthTypes monthTypes = (MonthTypes)Enum.GetValues(typeof(MonthTypes)).GetValue(twoday.Month);
            string data = $"{twoday.Year}\\{monthTypes}\\{twoday.Day}";
            return data;
        }
    }
}
