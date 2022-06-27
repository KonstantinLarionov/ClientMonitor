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
                    if (i>10)
                    {
                        break;
                    }
                }
                if (i>10)
                {
                    notifyer.SendMessage("-742266994", $"{listClouds.Name} : Слишком много дефектных файлов");
                }
            }
        }

        /// <summary>
        /// Список параметров для выгрузки в облако
        /// </summary>
        private readonly static List<ListDownloadCloud> _listClouds = new List<ListDownloadCloud>()
        {
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Зал",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Зал",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Ozon/Зал",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Тамбур",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Тамбур",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Ozon/Тамбур",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Выдача",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Выдача",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Ozon/Выдача1",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Склад",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Склад",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Ozon/Склад1",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Склад-2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Склад2",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Ozon/Склад2",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Тамбур-2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Тамбур2",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Ozon/Тамбур2",
                FormatFiles="*.avi",
            },

            new ListDownloadCloud
            {
                Name="WB-ПГ-Выдача",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Выдача",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Wildberries/Выдача",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-ПГ-Выдача2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Выдача2",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Wildberries/Выдача2",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-ПГ-Склад",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Склад",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Wildberries/Склад",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Ломбард1-ПГ",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ломбард\Ломбард1",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Ломбард/Ломбард1",
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
            MonthTypes monthTypes = (MonthTypes)Enum.GetValues(typeof(MonthTypes)).GetValue(dateTime.Month);
            string data = $"{dateTime.Year}\\{monthTypes}\\{dateTime.Day}";
            return data;
        }
    }
}