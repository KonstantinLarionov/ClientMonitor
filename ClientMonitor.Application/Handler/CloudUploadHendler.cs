using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Handler
{
    /// <summary>
    /// Логика загрузки в облако
    /// </summary>
    public class CloudUploadHendler : ICludUploadHendler
    {
        readonly INotification _telegramNotification;
        readonly INotification _maileNotification;

        /// <summary>
        /// Подключение библиотек
        /// </summary>
        /// <param name="cloud">Фабрика</param>
        /// <param name="notification">Уведомления</param>
        /// <param name="repositoryLog">Репоз логов</param>
        /// <param name="repositoryData">Репоз параметров</param>
        public CloudUploadHendler(INotificationFactory notification)
        {
            _telegramNotification = notification.GetNotification(Domanes.Enums.NotificationTypes.Telegram);
            _maileNotification = notification.GetNotification(Domanes.Enums.NotificationTypes.Mail);
        }

        //СДЕЛАТЬ ЕСЛИ ВЫЛЕТАЕТ ОШИБКА ПОДКЛЮЧЕНИЯ ТО ЧЕРЕЗ ЛОГИ ЧТОБЫ ВЫЗЫВАЛАСЬ ФУНКЦИЯ СТОПА ВИДЕО И НОВыЙ СТАРТ ЗАПИСИ
        /// <summary>
        /// Список параметров для выгрузки в облако
        /// </summary>
        private readonly static List<ListDownloadCloud> _listClouds = new List<ListDownloadCloud>()
        {
            //new ListDownloadCloud
            //{
            //    Name="Озон-ПГ-Зал",
            //    LocDownloadVideo=@"C:\Test\Баг",
            //    LocDownloadCloud="Тест/Склад",
            //    FormatFiles="*.avi",
            //},
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
                Name="Wb-ПГ-Склад",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Склад",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Ozon/Склад1",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Wb-ПГ-Склад-2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Склад2",
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
                Name="Озон-ПГ-Склад",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Склад",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Ozon/Склад",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Ломбард1-ПГ",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ломбард\Ломбард1",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Ломбард/Ломбард1",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Склад2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Склад2",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Ломбард/Ломбард2",
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

        public int summ = 0;
        /// <summary>
        /// Логика очистки
        /// </summary>
        /// <returns></returns>
        public async Task Handle()
        {
            foreach (var listClouds in _listClouds)
            {
                if (Directory.Exists(listClouds.LocDownloadVideo))
                {
                    try
                    {
                        DateTime dt = DateTime.Now;
                        string path = listClouds.LocDownloadVideo + "\\" + MonthStats(dt);

                        if (Directory.Exists(path))
                        {
                                DirectoryInfo dirInfo = new DirectoryInfo(path);
                                dirInfo.Delete(true);
                        }
                        else
                        {
                            Thread.Sleep(10000);
                        }
                    }
                    catch (Exception e) { }
                }
            }
        }
    }
}