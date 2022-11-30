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
            var notifyer = NotificationFactory.GetNotification(NotificationTypes.Telegram);
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

                    string[] allFoundFiles = Directory.GetFiles(listClouds.LocDownloadVideo + "\\" + MonthStats(dt), "", SearchOption.AllDirectories);

                    foreach (var file in allFoundFiles)
                    {
                        FileInfo fileInf = new FileInfo(file);

                        if (fileInf.Length > 300000)
                        {
                            fileInf.MoveTo(listClouds.LocDownloadCloud + "\\" + MonthStats(dt) + "\\" + fileInf.Name);
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
            //    FormatFiles="*.mp4",
            //},
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Зал",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Ozon\Зал",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Зал",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Тамбур",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Ozon\Тамбур",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Тамбур",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Выдача",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Ozon\Выдача",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Выдача",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Склад",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Ozon\Склад",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Склад",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Склад-2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Ozon\Склад2",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Склад2",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Тамбур-2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Ozon\Тамбур2",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Тамбур2",
                FormatFiles="*.mp4",
            },

            new ListDownloadCloud
            {
                Name="WB-ПГ-Выдача",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Wildberries\Выдача",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Выдача",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="WB-ПГ-Выдача2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Wildberries\Выдача2",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Выдача2",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="WB-ПГ-Склад",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Wildberries\Склад",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Склад",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Ломбард1-ПГ",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер2\Ломбард\Ломбард1",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ломбард\Ломбард1",
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