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
                Name="Озон-ПГ-Склад",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Склад",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Ozon/Склад1",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Склад-2",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Склад2",
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
                LocDownloadVideo=@"Компьютер DESKTOP-UCI85FS/C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Выдача",
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
                Name="WB-ПГ-Склад",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Склад",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Wildberries/Склад",
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
        /// Логика загрузки в облако
        /// </summary>
        /// <returns></returns>
        public async Task Handle()
        {
            string idChatTg = "-742266994";
            //if (_dbData.GetData("IdChatServer") != "")
            //{
            //    idChatTg = _dbData.GetData("IdChatServer");
            //}
            //await _telegramNotification.SendMessage("-742266994", "~~~Приложение ClientMonitor было запущено~~~");
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

                            string[] getFilesFromHall = Directory.GetFiles(path, listClouds.FormatFiles);

                            FileSystemInfo[] fileSystemInfo = new DirectoryInfo(path).GetFileSystemInfos();
                            DateTime dt1 = new DateTime(1990, 1, 1);
                            string fileName = "";
                            //поиск последнего файла
                            foreach (FileSystemInfo fileSI in fileSystemInfo)
                            {
                                if (fileSI.Extension == ".avi")//добавить нужные форматы
                                {
                                    if (dt1 < Convert.ToDateTime(fileSI.CreationTime))
                                    {
                                        dt1 = Convert.ToDateTime(fileSI.CreationTime);
                                        fileName = fileSI.Name;
                                    }
                                }
                            }
                            if (getFilesFromHall.Length != 0)
                            {
                                FileInfo fileInf = new FileInfo(path + "\\" + fileName);
                                var month = fileInf.CreationTime.Month;
                                var year = fileInf.CreationTime.Year;
                                MonthTypes day = (MonthTypes)Enum.GetValues(typeof(MonthTypes)).GetValue(month);
                                var uploadFile = GetUploadFile(fileInf, listClouds.LocDownloadCloud + "/" + MonthStats(fileInf.CreationTime));
                                try
                                {
                                    //await _cloud.UploadFiles(uploadFile);
                                    
                                    DirectoryInfo dirInfo = new DirectoryInfo(path);
                                    dirInfo.Delete(true);
                                    AddInBd($"Директория: {path} удалена: {DateTime.Now}", 2);
                                    //fileInf.Delete();
                                    summ++;
                                }
                                catch (Exception e)
                                {
                                    AddInBd($"Какая-то фигня с проверкой файла Дата: {DateTime.Now}", 2);
                                    Thread.Sleep(60000);
                                }
                            }
                        }
                        //if (getFilesFromHall.Length != 0)
                        //{
                        //    string[] files = GetWitoutLastElement(getFilesFromHall, getFilesFromHall.Length);
                        //    foreach (var file in files)
                        //    {
                        //        FileInfo fileInf = new FileInfo(file);
                        //        var month = fileInf.CreationTime.Month;
                        //        var year = fileInf.CreationTime.Year;
                        //        MonthTypes day = (MonthTypes)Enum.GetValues(typeof(MonthTypes)).GetValue(month);
                        //        var uploadFile = GetUploadFile(fileInf, listClouds.LocDownloadCloud + "/" + MonthStats(fileInf.CreationTime));
                        //        try
                        //        {
                        //            await _cloud.UploadFiles(uploadFile);
                        //            AddInBd($"Файл: {uploadFile.Name} загружен: {DateTime.Now}", 2);
                        //            fileInf.Delete();
                        //            summ++;
                        //        }
                        //        catch (Exception e)
                        //        {
                        //            AddInBd($"Какая-то фигня с проверкой файла Дата: {DateTime.Now}", 2);
                        //            Thread.Sleep(60000);
                        //        }
                        //    }
                        //}
                        else
                        {
                            AddInBd($"!~~~ОЗОН/Wb_ПГ_Файлы не были отправлены из папки: {listClouds.Name} так как она пуста.~~~!", 1);
                            Thread.Sleep(10000);
                        }
                    }
                    catch (Exception e) { AddInBd($"Какая-то фигня в целом с хендлером проверки файлов {e.Message} : {DateTime.Now}", 1); }
                }
            }
            //if (summ > 10)
            //{
            //    try
            //    {
            //        await _telegramNotification.SendMessage(idChatTg, $"!~~~ОЗОН/Wb_ПГ_Файлов отправлено на диск: {summ} Время: {DateTime.Now}~~~!");
            //        summ = 0;
            //    }
            //    catch (Exception e)
            //    {
            //        AddInBd($"Уведы в телеге : {e.Message} : {DateTime.Now}", 1);
            //    }
            //}
            //if (summ == 0)
            //{
            //    Thread.Sleep(10000);
            //}
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
            string[] files = new string[leght - 2];
            for (int i = 0; i < leght - 2; i++)
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