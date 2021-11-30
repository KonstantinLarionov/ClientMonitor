using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.IO;
using System.Reflection;


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
                return Path.Combine(_videoInfo.PathDownload, $"{_videoInfo.Name}_{dt.Year}_{dt.Month}_{dt.Day}_{dt.Hour}_{dt.Minute}_{dt.Second}.mp4");
            }
        }
        public event EventHandler ConnectionErrorEvent;
        public event EventHandler InfoAboutLog;

        private readonly ControlVideoInfo _videoInfo;
        private readonly string _currentDirectory;
        private readonly DirectoryInfo _libDirectory;
        private readonly Vlc.DotNet.Core.VlcMediaPlayer _mediaPlayer;

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
            _mediaPlayer.Log += GetLogError;
        }

        private void GetLogError(object sender, Vlc.DotNet.Core.VlcMediaPlayerLogEventArgs e)
        {
            if (e.Message.Contains("live555 demux error"))
            {
                InfoAboutLog?.Invoke(sender, new ErrorEventArgs(new Exception(e.Message)));
            }
        }

        /// <summary>
        /// запуск плеера
        /// </summary>
        public void StartMonitoring()
        {
            if (SetInfo())
            {
                _mediaPlayer.Play();
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
        /// Полчение настроек для запуска плеера
        /// </summary>
        private bool SetInfo()
        {
            var mediaOptions = new[]
            {
                ":sout=#file{dst=" + NameFile + "}",
                ":sout-keep"
            };
            var result = _mediaPlayer.SetMedia(_videoInfo.PathStream, mediaOptions);
            return result.State == Vlc.DotNet.Core.Interops.Signatures.MediaStates.NothingSpecial;
        }
    }
}
