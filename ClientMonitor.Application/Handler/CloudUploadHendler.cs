using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
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

        public async Task Handle()
        {
            if (TelegramNotification == null)
            {
                AddInBd("Ошибка соединения с телеграмом");
            }
            await TelegramNotification.SendMessage("-742266994", "~~~Приложение ClientMonitor было запущено~~~");
            string k0 = @"C:\Users\Big Lolipop\Desktop\Записи с камер\video\ZLOSE";
            string k1 = "*.mp4";
            string k2 = "Записи/Выдача";
            if (dbData.GetData("PathClaim") != "0")
            {
                k0 = dbData.GetData("PathClaim");
            }
            if (dbData.GetData("FormatFile") != "0")
            {
                k1 = dbData.GetData("FormatFile");
            }
            if (dbData.GetData("PathDownloadClaim") != "0")
            {
                k2 = dbData.GetData("PathDownloadClaim");
            }
            string[] getFilesFromHall = Directory.GetFiles(k0, k1);
            if (getFilesFromHall.Length != 0)
            {
                string[] files = GetWitoutLastElement(getFilesFromHall, getFilesFromHall.Length);
                foreach (var file in files)
                {
                    FileInfo fileInf = new FileInfo(file);
                    var uploadFile = GetUploadFile(fileInf, k2);
                    await Cloud.UploadFiles(uploadFile);
                    await TelegramNotification.SendMessage("-742266994", $"Файл: {uploadFile.Name} загружен: {DateTime.Now}");
                    fileInf.Delete();
                }
                await TelegramNotification.SendMessage("-742266994", $"~~~Отправка файлов из папки: \"Выдача\" завершена. Файлов отправлено: {getFilesFromHall.Length - 1} Время: {DateTime.Now}~~~");
            }
            else
            {
                await TelegramNotification.SendMessage("-742266994", $"!~~~Файлы не были отправлены из папки: \"Выдача\" так как она пуста.~~~!");
                AddInBd($"!~~~Файлы не были отправлены из папки: \"Выдача\" так как она пуста.~~~!");
            }
            string k3 = @"C:\Users\Big Lolipop\Desktop\Записи с камер\video\KMXLM";
            string k4 = "Записи/Склад";
            if (dbData.GetData("PathStorage") != "0")
            {
                k3 = dbData.GetData("PathStorage");
            }
            if (dbData.GetData("PathDownloadStorage") != "0")
            {
                k4 = dbData.GetData("PathDownloadStorage");
            }
            string idtelegram = "-742266994";
            if (dbData.GetData("IdChatServer") != "0")
            {
                idtelegram = dbData.GetData("IdChatServer");
            }
            string[] getFilesFromtorage = Directory.GetFiles(@k3, k1);
            if (getFilesFromtorage.Length != 0)
            {
                string[] files2 = GetWitoutLastElement(getFilesFromtorage, getFilesFromtorage.Length);
                foreach (var file in files2)
                {
                    FileInfo fileInf = new FileInfo(file);
                    var uploadFile = GetUploadFile(fileInf, k4);
                    await Cloud.UploadFiles(uploadFile);
                    await TelegramNotification.SendMessage(idtelegram, $"Файл: {uploadFile.Name} загружен: {DateTime.Now}");
                    fileInf.Delete();
                }
                await TelegramNotification.SendMessage(idtelegram, $"~~~Отправка файлов из папки: \"Склад\" завершена. Файлов отправлено: {getFilesFromtorage.Length - 1} Время: {DateTime.Now}~~~");
            }
            else
            {
                await TelegramNotification.SendMessage(idtelegram, $"!~~~Файлы не были отправлены из папки: \"Склад\" так как она пуста.~~~!");
                AddInBd($"!~~~Файлы не были отправленны из папки: \"Склад\" так как она пуста.~~~!");
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
