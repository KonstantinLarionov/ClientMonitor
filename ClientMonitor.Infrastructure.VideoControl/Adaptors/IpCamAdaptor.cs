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
        public string Name { get { return _videoInfo.Name; } }
        public IpCamAdaptor(ControlVideoInfo info)
        {
            _videoInfo = info;
        }

        /// <summary>
        /// Запуск бесконечного стрима, для примера на 1 мин.
        /// </summary>
        /// <returns></returns>
        public void StartMonitoring()
        {
            while (true)
            {
                var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                //обязательно в папке должна быть эта библиотека libvlc, без неё не запустится стрим
                var libDirectory =
                    new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

                DateTime dt = DateTime.Now;
                var destination = Path.Combine(_videoInfo.PathDownload, $"{_videoInfo.Name}_{dt.Year}_{dt.Month}_{dt.Day}_{dt.Hour}_{dt.Minute}_{dt.Second}.ts");
                var mediaPlayer = new Vlc.DotNet.Core.VlcMediaPlayer(libDirectory);
                var mediaOptions = new[]
                {
                    ":sout=#file{dst=" + destination + "}",
                    ":sout-keep"
                };
                mediaPlayer.SetMedia(_videoInfo.PathStream, mediaOptions);
                mediaPlayer.Play();
                Thread.Sleep(60000);
                mediaPlayer.Stop();
            }
        }
    }
}
