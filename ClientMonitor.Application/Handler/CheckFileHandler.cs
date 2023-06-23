using ClientMonitor.Application.Abstractions;
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

                    DirectoryInfo dirInfo = new DirectoryInfo(listClouds.LocDownloadVideo + "\\" + MonthStats(dt));
                    DirectoryInfo dirVideoInfo = new DirectoryInfo(listClouds.LocDownloadCloud + "\\" + MonthStats(dt));
                    if (!dirVideoInfo.Exists)
                    {
                        dirVideoInfo.Create();
                    }

                    string[] allFoundFiles = Directory.GetFiles(listClouds.LocDownloadVideo, "", SearchOption.AllDirectories);

                    foreach (var file in allFoundFiles)
                    {
                        FileInfo fileInf = new FileInfo(file);
                        if (fileInf.LastWriteTime.Date == DateTime.Now.AddDays(-2).Date)
                        {
                            fileInf.Delete();
                          //if (fileInf.Length > 300000)
                          //{
                          //  var k = listClouds.LocDownloadCloud + "\\" + MonthStats(dt) + "\\" + fileInf.Name;
                          //  fileInf.MoveTo(k);
                          //}
                          //else
                          //{
                          //  fileInf.Delete();
                          //}
                        }
                    }
                    dirInfo.Delete(true);
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
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Ozon\Зал",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Ozon\Зал",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Тамбур",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Ozon\Тамбур",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Ozon\Тамбур",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Выдача",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Ozon\Выдача",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Ozon\Выдача",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Wb-ПГ-Склад",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Wildberries\Склад",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Wildberries\Склад",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Wb-ПГ-Склад-2",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Wildberries\Склад2",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Wildberries\Склад2",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Тамбур-2",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Ozon\Тамбур2",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Ozon\Тамбур2",
                FormatFiles="*.mp4",
            },

            new ListDownloadCloud
            {
                Name="WB-ПГ-Выдача",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Wildberries\Выдача",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Wildberries\Выдача",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="WB-ПГ-Выдача2",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Wildberries\Выдача2",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Wildberries\Выдача2",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Склад",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Ozon\Склад",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Ozon\Склад",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Ломбард1-ПГ",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Ломбард\Ломбард1",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Ломбард\Ломбард1",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Склад2",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Ozon\Склад2",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Ozon\Склад2",
                FormatFiles="*.mp4",
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