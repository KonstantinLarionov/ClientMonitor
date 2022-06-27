using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using LibVLCSharp.Shared;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

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

                return Path.Combine(_videoInfo.PathDownload + "\\" + MonthStats(dt), $"{_videoInfo.Name}_{dt.Year}.{dt.Month}.{dt.Day}__{dt.Hour}-{dt.Minute}-{dt.Second}.avi");
            }
        }
        public event EventHandler ConnectionErrorEvent;
        public event EventHandler InfoAboutLog;

        private readonly ControlVideoInfo _videoInfo;
        private readonly MediaPlayer _mediaPlayer;
        private Media _media;
        LibVLC _libVLC;

        /// <summary>
        /// Настройка плеера, подгрузка библиотек
        /// </summary>
        /// <param name="info"> Параметры камеры</param>
        public IpCamAdaptor(ControlVideoInfo info)
        {
            _videoInfo = info;
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            _mediaPlayer.EndReached += (_2, _3) => DelayRestartMediaPlayer(_libVLC, _mediaPlayer);
            _mediaPlayer.EncounteredError+=(_2, _3) => DelayRestartMediaPlayer(_libVLC, _mediaPlayer);
        }

        private void DelayRestartMediaPlayer(LibVLC libVLC, MediaPlayer mediaPlayer)
        {
            Thread.Sleep(5000);
            System.Diagnostics.Debug.WriteLine("Retrying to connect.");

            _ = ThreadPool.QueueUserWorkItem(_ => StartMonitoring());
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
        public void StartMonitoring()
        {
            //_mediaPlayer.Play();
            _media = new Media(_libVLC, _videoInfo.PathStream.ToString(), FromType.FromLocation);
            _media.AddOption(":sout=#file{dst=" + NameFile + "}");
            _media.AddOption(":sout-keep");
            _media.AddOption(":live-caching=300");
            _mediaPlayer.Play(_media);
        }

        /// <summary>
        /// остановка плеера
        /// </summary>
        public void StopMonitoring()
        {
            if (_mediaPlayer.IsPlaying == true)
            {
                _mediaPlayer.Stop();
            }
        }
    }
}