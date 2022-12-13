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
            foreach (var listClouds in _listClouds)
            {
                try
                {
                    DateTime dt = DateTime.Now;

                    ////DirectoryInfo dirInfo = new DirectoryInfo(listClouds.LocDownloadVideo + "\\" + MonthStats(dt));
                    DirectoryInfo dirVideoInfo = new DirectoryInfo(listClouds.LocDownloadCloud + "\\" + MonthStats(dt));
                    if (!dirVideoInfo.Exists)
                    {
                        dirVideoInfo.Create();
                    }

                    string[] allFoundFiles = Directory.GetFiles(listClouds.LocDownloadVideo, "", SearchOption.AllDirectories);

                    foreach (var file in allFoundFiles)
                    {
                        FileInfo fileInf = new FileInfo(file);
                        if (fileInf.Extension == ".mp4" && fileInf.LastWriteTime.Date == DateTime.Now.AddDays(-1).Date)
                        {

                          if (fileInf.Length > 300000)
                          {
                            var k = listClouds.LocDownloadCloud + "\\" + MonthStats(dt) + "\\" + fileInf.Name;
                            fileInf.MoveTo(k);
                          }
                          else
                          {
                            fileInf.Delete();
                          }
                        }
                    }
                    ////dirInfo.Delete(true);
                }
                catch { }
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
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\video\Озон-ПГ-Зал",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Зал",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Тамбур",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\video\Озон-ПГ-Тамбур",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Тамбур",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Выдача",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\video\Озон-ПГ-Выдача",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Выдача",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Wb-ПГ-Склад",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\video\Wb-ПГ-Склад",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Склад",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Wb-ПГ-Склад-2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\video\Wb-ПГ-Склад-2",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Склад2",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Тамбур-2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\video\Озон-ПГ-Тамбур-2",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Тамбур2",
                FormatFiles="*.avi",
            },

            new ListDownloadCloud
            {
                Name="WB-ПГ-Выдача",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\video\WB-ПГ-Выдача",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Выдача",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-ПГ-Выдача2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\video\WB-ПГ-Выдача-2",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Выдача2",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Склад",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\video\Озон-ПГ-Склад",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Склад",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Ломбард1-ПГ",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\video\Ломбард1-ПГ",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ломбард\Ломбард1",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Склад2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\video\Озон-ПГ-Склад-2",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Склад2",
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