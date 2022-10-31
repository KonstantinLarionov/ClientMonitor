using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using ColinChang.FFmpegHelper;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.VideoControl.Adaptors
{
    /// <summary>
    /// Адаптор для подключения трансляции к плееру VLC и настройка
    /// </summary>
    public class IpCamAdaptor : IVideoControl
    {
        /// <summary>
        /// Получение имени параметров камеры
        /// </summary>
        public string Name
        {
            get
            {
                return _videoInfo.Name;
            }
        }

        /// <summary>
        /// Метод построения названия файла
        /// </summary>
        private string NameFile
        {
            get
            {
                DateTime dt = DateTime.Now;

                DirectoryInfo dirInfo = new DirectoryInfo(_videoInfo.PathDownload + "\\" + MonthStats(dt));
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                Pathfile = _videoInfo.PathDownload + "\\" + MonthStats(dt) + $"\\{_videoInfo.Name}_{dt.Year}.{dt.Month}.{dt.Day}__{dt.Hour}-{dt.Minute}-{dt.Second}.avi";
                return Path.Combine(_videoInfo.PathDownload + "\\" + MonthStats(dt), $"{_videoInfo.Name}_{dt.Year}.{dt.Month}.{dt.Day}__{dt.Hour}-{dt.Minute}-{dt.Second}.avi");
            }
        }
        public event EventHandler ConnectionErrorEvent;
        public event EventHandler InfoAboutLog;

        private readonly ControlVideoInfo _videoInfo;
        private string Pathfile;
        /// <summary>
        /// Настройка плеера, подгрузка библиотек
        /// </summary>
        /// <param name="info"> Параметры камеры</param>
        public IpCamAdaptor(ControlVideoInfo info)
        {
            _videoInfo = info;
        }

        /// <summary>
        /// Получение названия папки по дате
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static string MonthStats(DateTime dateTime)
        {
            MonthTypes monthTypes = (MonthTypes)Enum.GetValues(typeof(MonthTypes)).GetValue(dateTime.Month);
            string data = $"{dateTime.Year}\\{monthTypes}\\{dateTime.Day}";
            return data;
        }

        /// <summary>
        /// запуск плеера
        /// </summary>
        public async Task StartMonitoring()
        {
            RtspHelper rtsp = new RtspHelper(_videoInfo.PathStream.ToString(), 3000);
            await rtsp.Record2VideoFileAsync(
                NameFile,
                TimeSpan.FromSeconds(480),
                null,
                Transport.Tcp
            );
        }
    }
}