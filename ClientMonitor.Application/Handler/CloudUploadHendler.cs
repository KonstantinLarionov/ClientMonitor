using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Handler
{
    public class CloudUploadHendler : ICludUploadHendler
    {
        readonly ICloud Cloud;
        readonly INotification TelegramNotification;
        readonly INotification MaileNotification;
        readonly IRepository<LogInfo> dbLog;

        public CloudUploadHendler(ICloudFactory cloud, INotificationFactory notification, IRepository<LogInfo> repositoryLog)
        {
            Cloud = cloud.GetCloud(Application.Domanes.Enums.CloudTypes.YandexCloud);
            TelegramNotification = notification.GetNotification(Domanes.Enums.NotificationTypes.Telegram);
            MaileNotification = notification.GetNotification(Domanes.Enums.NotificationTypes.Mail);
            dbLog = repositoryLog;
        }
        public async Task Handle()
        {
            if (TelegramNotification==null)
            {
                LogInfo log = new LogInfo
                {
                    TypeLog = LogTypes.Error,
                    Text = "Ошибка соединения",
                    DateTime = DateTime.Now
                };
                dbLog.AddInDb(log);
                return;
            }
            await TelegramNotification.SendMessage("-742266994", "~~~Приложение ClientMonitor было запущено~~~");
            string[] getFilesFromHall = Directory.GetFiles(@"C:\Users\Big Lolipop\Desktop\Записи с камер\video\ZLOSE", "*.mp4");
            if (getFilesFromHall.Length != 0)
            {
                string[] files = GetWitoutLastElement(getFilesFromHall, getFilesFromHall.Length);

                foreach (var file in files)
                {
                    FileInfo fileInf = new FileInfo(file);
                    var uploadFile = GetUploadFile(fileInf, "Записи/Выдача");
                    await Cloud.UploadFiles(uploadFile);
                    await TelegramNotification.SendMessage("-742266994", $"Файл: {uploadFile.Name} загружен: {DateTime.Now}");
                    fileInf.Delete();
                }
                await TelegramNotification.SendMessage("-742266994", $"~~~Отправка файлов из папки: \"Выдача\" завершена. Файлов отправленно: {getFilesFromHall.Length - 1} Время: {DateTime.Now}~~~");
            }
            else
            {
                await TelegramNotification.SendMessage("-742266994", $"!~~~Файлы не были отправленны из папки: \"Выдача\" так как она пуста.~~~!");
            }

            string[] getFilesFromtorage = Directory.GetFiles(@"C:\Users\Big Lolipop\Desktop\Записи с камер\video\KMXLM", "*.mp4");
            if (getFilesFromtorage.Length != 0)
            {
                string[] files2 = GetWitoutLastElement(getFilesFromtorage, getFilesFromtorage.Length);
                foreach (var file in files2)
                {
                    FileInfo fileInf = new FileInfo(file);
                    var uploadFile = GetUploadFile(fileInf, "Записи/Склад");
                    await Cloud.UploadFiles(uploadFile);
                    await TelegramNotification.SendMessage("-742266994", $"Файл: {uploadFile.Name} загружен: {DateTime.Now}");
                    fileInf.Delete();
                }
                await TelegramNotification.SendMessage("-742266994", $"~~~Отправка файлов из папки: \"Склад\" завершена. Файлов отправленно: {getFilesFromtorage.Length-1} Время: {DateTime.Now}~~~");
            }
            else
            {
                await TelegramNotification.SendMessage("-742266994", $"!~~~Файлы не были отправленны из папки: \"Склад\" так как она пуста.~~~!");
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
            string[] files = new string[leght-1];
            for (int i = 0; i < leght - 1; i++)
                files[i] = mas[i];
            return files;
        }
    }
}
