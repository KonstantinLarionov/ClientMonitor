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
            string[] files = Directory.GetFiles(@"C:\Users\79123\Desktop\Новая папка", "*.txt");
            foreach (var file in files)
            {
                FileInfo fileInf = new FileInfo(file);
                var uploadFile = GetUploadFile(fileInf, "Тест");
                await Cloud.UploadFiles(uploadFile);
                await TelegramNotification.SendMessage("-742266994", $"Файл {uploadFile.Name} загружен {DateTime.Now}" );
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
