using ClientMonitor.Application.Abstractions;
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

        public CloudUploadHendler(ICloudFactory cloud, INotificationFactory notification)
        {
            Cloud = cloud.GetCloud(Application.Domanes.Enums.CloudTypes.YandexCloud);
            TelegramNotification = notification.GetNotification(Domanes.Enums.NotificationTypes.Telegram);
            MaileNotification = notification.GetNotification(Domanes.Enums.NotificationTypes.Mail);
        }
        public async Task Handle()
        {
            await TelegramNotification.SendMessage("-742266994", "~~~Приложение ClientMonitor было запущено~~~");
            string[] files = Directory.GetFiles(@"C:\Users\Big Lolipop\Desktop\Записи с камер\video\ZLOSE", "*.mp4");
            foreach (var file in files)
            {
                FileInfo fileInf = new FileInfo(file);
                var uploadFile = GetUploadFile(fileInf, "Записи/Выдача");
                await Cloud.UploadFiles(uploadFile);
                await TelegramNotification.SendMessage("-742266994", $"Файл: {uploadFile.Name} загружен: {DateTime.Now}" );
                fileInf.Delete();
            }
            string[] files2 = Directory.GetFiles(@"C:\Users\Big Lolipop\Desktop\Записи с камер\video\KMXLM", "*.mp4");
            foreach (var file in files2)
            {
                FileInfo fileInf = new FileInfo(file);
                var uploadFile = GetUploadFile(fileInf, "Записи/Склад");
                await Cloud.UploadFiles(uploadFile);
                await TelegramNotification.SendMessage("-742266994", $"Файл: {uploadFile.Name} загружен: {DateTime.Now}");
                fileInf.Delete();
            }
            await TelegramNotification.SendMessage("-742266994", $"~~~Отправка файлов завершена. Файлов отправленно: {files.Length} Время: {DateTime.Now}~~~");
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
    }
}
