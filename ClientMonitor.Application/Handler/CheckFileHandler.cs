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
                        else
                        {
                            fileInf.Delete();
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
                Name="Озон-МГ-Вход",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер2\Ozon\Вход",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Вход",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-МГ-Зал",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер2\Ozon\Зал",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Зал",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-МГ-Склад",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер2\Ozon\Склад",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Склад",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-МГ-Склад2",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер2\Ozon\Склад2",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Склад2",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-МГ-Зал",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер2\Wb\Зал",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Зал",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-МГ-Склад",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер2\Wb\Склад",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Склад",
                FormatFiles="*.avi",
            },

            new ListDownloadCloud
            {
                Name="WB-МГ-Зал3",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер2\Wb\Зал3",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Зал3",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-МГ-Кухня",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер2\Wb\Кухня",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Кухня",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-МГ-Тамбур",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер2\Wb\Тамбур",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Тамбур",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-МГ-Склад2",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер2\Wb\Склад2",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Склад2",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-МГ-Парковка",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер2\Wb\Парковка",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Парковка",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-МГ-Склад3",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер2\Wb\Склад3",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Склад3",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-МГ-Выгрузка",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер2\Wb\Выгрузка",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Выгрузка",
                FormatFiles="*.avi",
            }
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