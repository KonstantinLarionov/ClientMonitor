using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.IO;

namespace ClientMonitor.Application.Handler
{
    public class CheckHandler : ICheckHandler
    {
        INotificationFactory NotificationFactory;

        /// <summary>
        /// Подключение библиотек
        /// </summary>
        public CheckHandler(INotificationFactory notificationFactory)
        {
            NotificationFactory = notificationFactory;
        }

        public void CheckHandle()
        {
            var notifyer = NotificationFactory.GetNotification(NotificationTypes.Telegram);
            foreach (var listClouds in _listClouds)
            {
                try
                {
                    DateTime dt = DateTime.Now;

                    DirectoryInfo dirInfo = new DirectoryInfo(listClouds.LocDownloadVideo + "\\" + MonthStats(dt));
                    string[] allFoundFiles = Directory.GetFiles(listClouds.LocDownloadVideo + "\\" + MonthStats(dt), "", SearchOption.AllDirectories);
                    int i = 0;
                    if (allFoundFiles.Length > 0)
                    {
                        string[] files = GetWitoutLastElement(allFoundFiles, allFoundFiles.Length);
                        foreach (var file in files)
                        {
                            FileInfo fileInf = new FileInfo(file);

                            if (fileInf.Length < 300000)
                            {
                                i++;
                            }
                        }
                        if (i > 20)
                        {
                            notifyer.SendMessage("-693501604", listClouds.Name + " Проверьте запись видео");
                            foreach (var file1 in files)
                            {
                                FileInfo fileInf = new FileInfo(file1);

                                if (fileInf.Length < 300000)
                                {
                                    fileInf.Delete();
                                }
                            }
                        }
                    }
                }
                catch (Exception e) { }
            }
        }

        private string[] GetWitoutLastElement(string[] mas, int leght)
        {
            string[] files = new string[leght - 2];
            for (int i = 0; i < leght - 2; i++)
                files[i] = mas[i];
            return files;
        }

        /// <summary>
        /// Список параметров для выгрузки в облако
        /// </summary>
        private readonly static List<ListDownloadCloud> _listClouds = new List<ListDownloadCloud>()
        {
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
            MonthTypes monthTypes = (MonthTypes)Enum.GetValues(typeof(MonthTypes)).GetValue(dateTime.Month);
            string data = $"{dateTime.Year}\\{monthTypes}\\{dateTime.Day}";
            return data;
        }
    }
}
