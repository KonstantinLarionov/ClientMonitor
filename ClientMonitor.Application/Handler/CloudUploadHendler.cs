using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Handler
{
    public class CloudUploadHendler : ICludUploadHendler
    {
        readonly ICloud Cloud;
        readonly INotification TelegramNotification;
        readonly INotification MaileNotification;
        readonly IRepository<LogInfo> dbLog;
        readonly IRepository<DataForEditInfo> dbData;

        public CloudUploadHendler(ICloudFactory cloud, INotificationFactory notification, IRepository<LogInfo> repositoryLog, IRepository<DataForEditInfo> repositoryData)
        {
            Cloud = cloud.GetCloud(Application.Domanes.Enums.CloudTypes.YandexCloud);
            TelegramNotification = notification.GetNotification(Domanes.Enums.NotificationTypes.Telegram);
            MaileNotification = notification.GetNotification(Domanes.Enums.NotificationTypes.Mail);
            dbLog = repositoryLog;
            dbData = repositoryData;
        }
        private readonly static List<ListDownloadCloud> ListClouds = new List<ListDownloadCloud>()
        {
            new ListDownloadCloud
            {
                Name="ОзонПГ выдача",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\Записи с камер\video\ZLOSE",
                LocDownloadCloud="Записи/Выдача",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="ОзонПГ склад",
                LocDownloadCloud=@"C:\Users\Big Lolipop\Desktop\Записи с камер\video\KMXLM",
                LocDownloadVideo="Записи/Склад",
                FormatFiles="*.mp4",
            },
        };
        public async Task Handle()
        {
            string idChatTg = "-742266994";
            if (dbData.GetData("IdChatServer") != "")
            {
                idChatTg = dbData.GetData("IdChatServer");
            }
            await TelegramNotification.SendMessage("-742266994", "~~~Приложение ClientMonitor было запущено~~~");
            foreach (var listClouds in ListClouds)
            {
                string[] getFilesFromHall = Directory.GetFiles(listClouds.LocDownloadVideo, listClouds.FormatFiles);
                if (getFilesFromHall.Length != 0)
                {
                    string[] files = GetWitoutLastElement(getFilesFromHall, getFilesFromHall.Length);
                    foreach (var file in files)
                    {
                        FileInfo fileInf = new FileInfo(file);
                        var uploadFile = GetUploadFile(fileInf, listClouds.LocDownloadCloud);
                        await Cloud.UploadFiles(uploadFile);
                        await TelegramNotification.SendMessage(idChatTg, $"Файл: {uploadFile.Name} загружен: {DateTime.Now}");
                        fileInf.Delete();
                    }
                    await TelegramNotification.SendMessage(idChatTg, $"~~~Отправка файлов из папки: {listClouds.Name} завершена. Файлов отправлено: {getFilesFromHall.Length - 1} Время: {DateTime.Now}~~~");
                }
                else
                {
                    await TelegramNotification.SendMessage(idChatTg, $"!~~~Файлы не были отправлены из папки: {listClouds.Name} так как она пуста.~~~!");
                    AddInBd($"!~~~Файлы не были отправлены из папки: {listClouds.Name} так как она пуста.~~~!");
                }
            }
        }
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

        private string[] GetWitoutLastElement(string[] mas, int leght)
        {
            string[] files = new string[leght - 1];
            for (int i = 0; i < leght - 1; i++)
                files[i] = mas[i];
            return files;
        }

        private void AddInBd(string k)
        {
            LogInfo log = new LogInfo
            {
                TypeLog = LogTypes.Error,
                Text = k,
                DateTime = DateTime.Now
            };
            dbLog.AddInDb(log);
        }
    }
}
