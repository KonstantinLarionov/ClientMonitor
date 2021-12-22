using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
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
                return Path.Combine(_videoInfo.PathDownload+"\\" + MonthStats(dt), $"{_videoInfo.Name}_{dt.Year}_{dt.Month}_{dt.Day}_{dt.Hour}_{dt.Minute}_{dt.Second}.mp4");
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
            _mediaPlayer.EncounteredError += (objec, message) =>
                ConnectionErrorEvent?.Invoke(objec, new ErrorEventArgs(new Exception(message.ToString())));
            _mediaPlayer.Log += Log;
        }

        /// <summary>
        /// Получение названия папки по дате
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static string MonthStats(DateTime dateTime)
        {
            MonthTypes monthTypes = (MonthTypes)Enum.GetValues(typeof(MonthTypes)).GetValue(dateTime.Month);
            string data = $"{dateTime.Year}\\{monthTypes}";
            return data;
        }

        private void Log(object sender, Vlc.DotNet.Core.VlcMediaPlayerLogEventArgs e)
        {

        }

        /// <summary>
        /// запуск плеера
        /// </summary>
        public void StartMonitoring()
        {
            if (SetInfo())
            {
                _mediaPlayer.Play();
                //слип на 10 секунд
                Thread.Sleep(10000);
                //проверка размера видоса, если не устраивает то перезапускаем старт
                if (GetSizeVideo() == false)
                {
                    _mediaPlayer.Stop();
                    StartMonitoring();
                }
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
        private bool GetSizeVideo()
        {
            bool checkVideo = true;
            try
            {
                string newName = VideoName.Replace('\\', '/');
                ShellObject shell = ShellObject.FromParsingName(newName);
                IShellProperty prop = shell.Properties.System.Media.Duration;
                // Duration will be formatted as 00:44:08
                string duration = prop.FormatForDisplay(PropertyDescriptionFormatOptions.None);

                //если размерм меньше 2кБ
                if (duration == "00:00:01" || duration == "00:00:02")
                {
                    checkVideo = false;
                }
            }
            catch { }
            return checkVideo;
        }
    }
}