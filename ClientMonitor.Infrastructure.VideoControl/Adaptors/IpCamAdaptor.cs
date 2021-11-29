using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.VideoControl.Adaptors
{
    public class IpCamAdaptor : IVideoControl
    {
        private readonly ControlVideoInfo _videoInfo;
        //public delegate void ConnectionError(string message);
        public event EventHandler ConnectionErrorEvent;
        public string Name { get { return _videoInfo.Name; } }
        private bool IsError { get; set; } = false;
        string currentDirectory;
        DirectoryInfo libDirectory;
        Vlc.DotNet.Core.VlcMediaPlayer mediaPlayer;
        public IpCamAdaptor(ControlVideoInfo info)
        {
            _videoInfo = info;
            currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            libDirectory = new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));
            mediaPlayer = new Vlc.DotNet.Core.VlcMediaPlayer(libDirectory);
        }

        /// <summary>
        /// Запуск бесконечного стрима на 15 мин.
        /// </summary>
        /// <returns></returns>
        public void StartMonitoring()
        {
            //while (!IsError)
            //{
                //var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                //обязательно в папке должна быть эта библиотека libvlc, без неё не запустится стрим
                //var libDirectory =
                //    new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

                DateTime dt = DateTime.Now;
                var destination = Path.Combine(_videoInfo.PathDownload, $"{_videoInfo.Name}_{dt.Year}_{dt.Month}_{dt.Day}_{dt.Hour}_{dt.Minute}_{dt.Second}.avi");
                //var mediaPlayer = new Vlc.DotNet.Core.VlcMediaPlayer(libDirectory);
                var mediaOptions = new[]
                {
                    ":sout=#file{dst=" + destination + "}",
                    ":sout-keep"
                };
                mediaPlayer.SetMedia(_videoInfo.PathStream, mediaOptions);
                //Если вылетает эта ошибка, то запись рип
                mediaPlayer.Log += (sender, e) =>
                {
                    string error = "Failed to connect to RTSP server";
                    if (e.Message.Contains(error))
                    {
                        IsError = true;
                        var message = $"{DateTime.Now} : {_videoInfo.Name} : {e.Message}";
                        ConnectionErrorEvent?.Invoke(this, new ErrorEventArgs(new Exception(message)));
                    }
                };
                mediaPlayer.Play();
                //Thread.Sleep(900000);
                //mediaPlayer.Stop();
            //}
            //IsError = false;
            //Thread.Sleep(60000);
            //StartMonitoring();
        }

        public void StopMonitoring()
        {
            mediaPlayer.Stop();
        }
    }
}
