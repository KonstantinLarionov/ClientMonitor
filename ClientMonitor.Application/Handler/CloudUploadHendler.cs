using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
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

        /// <summary>
        /// Список параметров для выгрузки в облако
        /// </summary>
        private readonly static List<ListDownloadCloud> _listClouds = new List<ListDownloadCloud>()
        {
            new ListDownloadCloud
            {
                Name="ОзонПГ выдача",
                //LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ТестКамер\ZLOSE",
                LocDownloadVideo=@"C:\Test\Test1",
                //LocDownloadCloud="Записи/Выдача",
                LocDownloadCloud="Тест/Выдача",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="ОзонПГ склад",
                //LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ТестКамер\KMXLM",
                LocDownloadVideo=@"C:\Test\Test2",
                //LocDownloadCloud="Записи/Склад",
                LocDownloadCloud="Тест/Склад",
                FormatFiles="*.mp4",
            },
        };

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
                    string[] getFilesFromHall = Directory.GetFiles(listClouds.LocDownloadVideo, listClouds.FormatFiles);
                    if (getFilesFromHall.Length != 0)
                    {
                        string[] files = GetWitoutLastElement(getFilesFromHall, getFilesFromHall.Length);
                        foreach (var file in files)
                        {
                            FileInfo fileInf = new FileInfo(file);
                            var uploadFile = GetUploadFile(fileInf, listClouds.LocDownloadCloud);
                            await _cloud.UploadFiles(uploadFile);
                            //await _telegramNotification.SendMessage(idChatTg, $"Файл: {uploadFile.Name} загружен: {DateTime.Now}");
                            fileInf.Delete();
                        }
                        //await _telegramNotification.SendMessage(idChatTg, $"~~~Отправка файлов из папки: {listClouds.Name} завершена. Файлов отправлено: {getFilesFromHall.Length - 1} Время: {DateTime.Now}~~~");
                    }
                    else
                    {
                        //await _telegramNotification.SendMessage(idChatTg, $"!~~~Файлы не были отправлены из папки: {listClouds.Name} так как она пуста.~~~!");
                        AddInBd($"!~~~Файлы не были отправлены из папки: {listClouds.Name} так как она пуста.~~~!");
                    }
                }
            }
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
        private void AddInBd(string message)
        {
            LogInfo log = new LogInfo
            {
                TypeLog = LogTypes.Error,
                Text = message,
                DateTime = DateTime.Now
            };
            _dbLog.AddInDb(log);
        }
    }
}
