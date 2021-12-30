﻿using ClientMonitor.Application.Abstractions;
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
        readonly IRepository<LogInfo> _dbLog;
        readonly IRepository<DataForEditInfo> _dbData;

        /// <summary>
        /// Подключение библиотек
        /// </summary>
        /// <param name="cloud">Фабрика</param>
        /// <param name="notification">Уведомления</param>
        /// <param name="repositoryLog">Репоз логов</param>
        /// <param name="repositoryData">Репоз параметров</param>
        public CloudUploadHendler(ICloudFactory cloud, INotificationFactory notification, IRepository<LogInfo> repositoryLog, IRepository<DataForEditInfo> repositoryData)
        {
            _cloud = cloud.GetCloud(Application.Domanes.Enums.CloudTypes.YandexCloud);
            _telegramNotification = notification.GetNotification(Domanes.Enums.NotificationTypes.Telegram);
            _maileNotification = notification.GetNotification(Domanes.Enums.NotificationTypes.Mail);
            _dbLog = repositoryLog;
            _dbData = repositoryData;
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
            //    FormatFiles="*.mp4",
            //},
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Зал",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ТестКамер\Ozon\Зал",
                LocDownloadCloud="ЗаписиКамерыПГ/Ozon/Зал",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Тамбур",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ТестКамер\Ozon\Тамбур",
                LocDownloadCloud="ЗаписиКамерыПГ/Ozon/Тамбур",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Выдача",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ТестКамер\Ozon\Выдача",
                LocDownloadCloud="ЗаписиКамерыПГ/Ozon/Выдача1",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Склад",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ТестКамер\Ozon\Склад",
                LocDownloadCloud="ЗаписиКамерыПГ/Ozon/Склад1",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Склад-2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ТестКамер\Ozon\Склад2",
                LocDownloadCloud="ЗаписиКамерыПГ/Ozon/Склад2",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Тамбур-2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ТестКамер\Ozon\Тамбур2",
                LocDownloadCloud="ЗаписиКамерыПГ/Ozon/Тамбур2",
                FormatFiles="*.mp4",
            },

            new ListDownloadCloud
            {
                Name="WB-ПГ-Выдача",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ТестКамер\Wildberries\Выдача",
                LocDownloadCloud="ЗаписиКамерыПГ/Wildberries/Выдача",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="WB-ПГ-Выдача2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ТестКамер\Wildberries\Выдача2",
                LocDownloadCloud="ЗаписиКамерыПГ/Wildberries/Выдача2",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="WB-ПГ-Склад",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ТестКамер\Wildberries\Склад",
                LocDownloadCloud="ЗаписиКамерыПГ/Wildberries/Склад",
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
            MonthTypes monthTypes = (MonthTypes)Enum.GetValues(typeof(MonthTypes)).GetValue(dateTime.Month);
            string data = $"{dateTime.Year}/{monthTypes}";
            return data;
        }

        public int summ = 0;
        /// <summary>
        /// Логика загрузки в облако
        /// </summary>
        /// <returns></returns>
        public async Task Handle()
        {
            string idChatTg = "-742266994";
            if (_dbData.GetData("IdChatServer") != "")
            {
                idChatTg = _dbData.GetData("IdChatServer");
            }
            //await _telegramNotification.SendMessage("-742266994", "~~~Приложение ClientMonitor было запущено~~~");
            foreach (var listClouds in _listClouds)
            {
                if (Directory.Exists(listClouds.LocDownloadVideo))
                {
                    try
                    {
                        DateTime dt = DateTime.Now;
                        string[] getFilesFromHall = Directory.GetFiles(listClouds.LocDownloadVideo + "/" + MonthStats(dt), listClouds.FormatFiles);
                        if (getFilesFromHall.Length != 0)
                        {
                            string[] files = GetWitoutLastElement(getFilesFromHall, getFilesFromHall.Length);
                            foreach (var file in files)
                            {
                                FileInfo fileInf = new FileInfo(file);
                                var month = fileInf.CreationTime.Month;
                                var year = fileInf.CreationTime.Year;
                                MonthTypes day = (MonthTypes)Enum.GetValues(typeof(MonthTypes)).GetValue(month);
                                var uploadFile = GetUploadFile(fileInf, listClouds.LocDownloadCloud + "/" + MonthStats(fileInf.CreationTime));
                                await _cloud.UploadFiles(uploadFile);
                                AddInBd($"Файл: {uploadFile.Name} загружен: {DateTime.Now}", 2);
                                fileInf.Delete();
                                summ++;
                            }
                        }
                        else
                        {
                            AddInBd($"!~~~ОЗОН/Wb_ПГ_Файлы не были отправлены из папки: {listClouds.Name} так как она пуста.~~~!", 1);
                        }
                    }
                    catch (Exception e) { AddInBd($"{e.Message} : {DateTime.Now}", 1); }
                }
            }
            await _telegramNotification.SendMessage(idChatTg, $"!~~~ОЗОН/Wb_ПГ_Файлов отправлено на диск: {summ} Время: {DateTime.Now}~~~!");
            summ = 0;
        }

        /// <summary>
        /// Информация о загружаемом файле
        /// </summary>
        /// <param name="fileInf">Параметры файла</param>
        /// <param name="pathToLoad">Путь загрузки</param>
        /// <returns></returns>
        private UploadedFilesInfo GetUploadFile(FileInfo fileInf, string pathToLoad)
        {
            UploadedFilesInfo uploadedFiles = new UploadedFilesInfo();
            uploadedFiles.Name = fileInf.Name;
            uploadedFiles.Extension = fileInf.Extension;
            uploadedFiles.Create = fileInf.CreationTime;
            uploadedFiles.Path = fileInf.DirectoryName;
            uploadedFiles.FolderName = pathToLoad;
            return uploadedFiles;
        }

        /// <summary>
        /// Загрузка в облако
        /// </summary>
        /// <param name="mas">Массив</param>
        /// <param name="leght">Длина</param>
        /// <returns></returns>
        private string[] GetWitoutLastElement(string[] mas, int leght)
        {
            string[] files = new string[leght - 1];
            for (int i = 0; i < leght - 1; i++)
                files[i] = mas[i];
            return files;
        }

        /// <summary>
        /// Добавление логов в бд
        /// </summary>
        /// <param name="message"></param>
        private void AddInBd(string message, int error)
        {
            LogTypes type = LogTypes.Information;
            if (error == 1)
            {
                type = LogTypes.Error;
            }

            LogInfo log = new LogInfo
            {
                TypeLog = type,
                Text = message,
                DateTime = DateTime.Now
            };
            _dbLog.AddInDb(log);
        }
    }
}