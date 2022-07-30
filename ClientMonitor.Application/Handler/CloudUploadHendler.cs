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
        readonly ICloud _cloud;
        readonly INotification _telegramNotification;
        readonly INotification _maileNotification;

        /// <summary>
        /// Подключение библиотек
        /// </summary>
        /// <param name="cloud">Фабрика</param>
        /// <param name="notification">Уведомления</param>
        /// <param name="repositoryLog">Репоз логов</param>
        /// <param name="repositoryData">Репоз параметров</param>
        public CloudUploadHendler(ICloudFactory cloud, INotificationFactory notification)
        {
            _cloud = cloud.GetCloud(Application.Domanes.Enums.CloudTypes.YandexCloud);
            _telegramNotification = notification.GetNotification(Domanes.Enums.NotificationTypes.Telegram);
            _maileNotification = notification.GetNotification(Domanes.Enums.NotificationTypes.Mail);
        }

        //СДЕЛАТЬ ЕСЛИ ВЫЛЕТАЕТ ОШИБКА ПОДКЛЮЧЕНИЯ ТО ЧЕРЕЗ ЛОГИ ЧТОБЫ ВЫЗЫВАЛАСЬ ФУНКЦИЯ СТОПА ВИДЕО И НОВыЙ СТАРТ ЗАПИСИ
        /// <summary>
        /// Список параметров для выгрузки в облако
        /// </summary>
        private readonly static List<ListDownloadCloud> _listClouds = new List<ListDownloadCloud>()
        {
            new ListDownloadCloud
            {
                Name="Озон-МГ-Вход",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Вход",
                LocDownloadCloud="ЗаписиКамерыПГ/Ozon/Вход",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-МГ-Зал",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Зал",
                LocDownloadCloud="ЗаписиКамерыПГ/Ozon/Зал",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-МГ-Склад",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Склад",
                LocDownloadCloud="ЗаписиКамерыПГ/Ozon/Склад",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-МГ-Склад2",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Склад2",
                LocDownloadCloud="ЗаписиКамерыПГ/Ozon/Склад2",
                FormatFiles="*.mp4",
            },


            new ListDownloadCloud
            {
                Name="Озон-Wb-Зал",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Зал",
                LocDownloadCloud="ЗаписиКамерыПГ/Wb/Зал",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-Wb-Зал2",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Зал2",
                LocDownloadCloud="ЗаписиКамерыПГ/Wb/Зал2",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-Wb-Кухня",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Кухня",
                LocDownloadCloud="ЗаписиКамерыПГ/Wb/Кухня",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-Wb-Тамбур",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Тамбур",
                LocDownloadCloud="ЗаписиКамерыПГ/Wb/Тамбур",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-Wb-Склад2",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Тамбур",
                LocDownloadCloud="ЗаписиКамерыПГ/Wb/Склад2",
                FormatFiles="*.mp4",
            }
        };

        /// <summary>
        /// Получение названия папки по дате
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static string MonthStats(DateTime dateTime)
        {
            DateTime twoday = dateTime.AddDays(-2);
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
                        DirectoryInfo dirInfo = new DirectoryInfo(path);
                        dirInfo.Delete(true);

                    }
                    catch (Exception e) { }
                }
            }
        }
    }
}