using ClientMonitor.Application.Abstractions;
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
    public class CamTestAdaptor : IVideoControl
    {
        /// <summary>
        /// Запуск бесконечного стрима, для примера на 10 сек.
        /// </summary>
        /// <returns></returns>
        public object StartMonitoring()
        {
            while (true)
            {
                var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                //обязательно в папке должна быть эта библиотека libvlc, без неё не запустится стрим
                var libDirectory =
                    new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

                DateTime dt = DateTime.Now;
                var destination = Path.Combine(@"C:\Test\Test1", $"Stream1_{dt.Year}_{dt.Month}_{dt.Day}_{dt.Hour}_{dt.Minute}_{dt.Second}.ts");
                var mediaPlayer = new Vlc.DotNet.Core.VlcMediaPlayer(libDirectory);
                var mediaOptions = new[]
                {
                    ":sout=#file{dst=" + destination + "}",
                    ":sout-keep"
                };
                mediaPlayer.SetMedia("rtsp://TestCam:123456@192.168.89.29:554/stream1",
                    mediaOptions);
                mediaPlayer.Play();
                Thread.Sleep(10000);
                mediaPlayer.Stop();
            }
        }

        public object StopMonitoring()
        {
            throw new NotImplementedException();
        }
    }
}
