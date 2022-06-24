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
    public class CloudUploadHandler : ICludUploadHandler
    {
        /// <summary>
        /// Подключение библиотек
        /// </summary>
        /// <param name="cloud">Фабрика</param>
        /// <param name="notification">Уведомления</param>
        /// <param name="repositoryLog">Репоз логов</param>
        /// <param name="repositoryData">Репоз параметров</param>
        public CloudUploadHandler()
        {
        }

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
                Name="WB-ПГ-Склад",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Склад",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Wildberries/Склад",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Ломбард1-ПГ",
                LocDownloadVideo=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ломбард\Ломбард1",
                LocDownloadCloud="Компьютер DESKTOP-UCI85FS/ЗаписиКамер/Ломбард/Ломбард1",
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

                                    summ++;
                                }
                                catch (Exception e)
                                {
                                    Thread.Sleep(60000);
                                }
                            }
                        }
                        else
                        {
                            Thread.Sleep(10000);
                        }
                    }
                    catch (Exception e) {  }
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
    }
}