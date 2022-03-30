using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;

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

                return Path.Combine(_videoInfo.PathDownload+"\\" + MonthStats(dt), $"{_videoInfo.Name}_{dt.Year}_{dt.Month}_{dt.Day}_{dt.Hour}_{dt.Minute}_{dt.Second}.avi");
            }
        }
        public event EventHandler ConnectionErrorEvent;
        public event EventHandler InfoAboutLog;

        private readonly ControlVideoInfo _videoInfo;
        private readonly string _currentDirectory;
        private readonly DirectoryInfo _libDirectory;
        private readonly Vlc.DotNet.Core.VlcMediaPlayer _mediaPlayer;
        private string VideoName;
        /// <summary>
        /// Настройка плеера, подгрузка библиотек
        /// </summary>
        /// <param name="info"> Параметры камеры</param>
        public IpCamAdaptor(ControlVideoInfo info)
        {
            _videoInfo = info;
            _currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            _libDirectory = new DirectoryInfo(Path.Combine(_currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));
            _mediaPlayer = new Vlc.DotNet.Core.VlcMediaPlayer(_libDirectory);
            //_mediaPlayer.EncounteredError += (objec, message) =>
            //    ConnectionErrorEvent?.Invoke(objec, new ErrorEventArgs(new Exception(message.ToString())));
            _mediaPlayer.EncounteredError += Error;
            _mediaPlayer.Log += Log;
        }

        private void Error(object sender, Vlc.DotNet.Core.VlcMediaPlayerEncounteredErrorEventArgs e)
        {
            ConnectionErrorEvent?.Invoke(sender, new ErrorEventArgs(new Exception(e.ToString())));
            //StopMonitoring();
            //Thread.Sleep(20000);
            //StartMonitoring();
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

        private bool Check=false;
        /// <summary>
        /// проверка на размер файла через логи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Log(object sender, Vlc.DotNet.Core.VlcMediaPlayerLogEventArgs e)
        {
            //if (Check)
            //{
            //    Thread.Sleep(30000);
            //    FileInfo file = new FileInfo(VideoName);
            //    long size = file.Length / 1024;
            //    if (size < 50)
            //    {
            //        _mediaPlayer.Stop();
            //        _mediaPlayer.Play();
            //    }
            //    Check = false;

                
            //}
        }

        /// <summary>
        /// запуск плеера
        /// </summary>
        public void StartMonitoring()
        {
            if (SetInfo())
            {
                _mediaPlayer.Play();
                Check = true;
            }
        }

        /// <summary>
        /// остановка плеера
        /// </summary>
        public void StopMonitoring()
        {
            if (_mediaPlayer.CouldPlay == true)
            {
                _mediaPlayer.Stop();
            }
        }

        /// <summary>
        /// Получение настроек для запуска плеера
        /// </summary>
        private bool SetInfo()
        {
            VideoName = NameFile;
            var mediaOptions = new[]
            {
                ":sout=#file{dst=" + NameFile + "}",
                ":sout-keep"
            };
            var result = _mediaPlayer.SetMedia(_videoInfo.PathStream, mediaOptions);
            return result.State == Vlc.DotNet.Core.Interops.Signatures.MediaStates.NothingSpecial;
        }

        /// <summary>
        /// Проверка размера видеофайла
        /// </summary>
        /// <returns></returns>
        //private bool GetSizeVideo()
        //{
        //    bool checkVideo = true;
        //    try
        //    {
        //        string newName = VideoName.Replace('\\', '/');
        //        ShellObject shell = ShellObject.FromParsingName(newName);
        //        IShellProperty prop = shell.Properties.System.Media.Duration;
        //        // Duration will be formatted as 00:44:08
        //        string duration = prop.FormatForDisplay(PropertyDescriptionFormatOptions.None);

        //        //если размерм меньше 2кБ
        //        if (duration == "00:00:01" || duration == "00:00:02")
        //        {
        //            checkVideo = false;
        //        }
        //    }
        //    catch { }
        //    return checkVideo;
        //}
    }
}