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
            //mediaPlayer.Log += (objec, message) => 
            //ConnectionErrorEvent?.Invoke(objec, new ErrorEventArgs(new Exception(message.Message)));
            mediaPlayer.EncounteredError += (objec, message) =>
            ConnectionErrorEvent?.Invoke(objec, new ErrorEventArgs(new Exception(message.ToString())));
        }

        public bool Connection()
        {
            DateTime dt = DateTime.Now;
            var destination = Path.Combine(_videoInfo.PathDownload, $"{_videoInfo.Name}_{dt.Year}_{dt.Month}_{dt.Day}_{dt.Hour}_{dt.Minute}_{dt.Second}.avi");
            //var mediaPlayer = new Vlc.DotNet.Core.VlcMediaPlayer(libDirectory);
            var mediaOptions = new[]
            {
                    ":sout=#file{dst=" + destination + "}",
                    ":sout-keep"
                };
            var result = mediaPlayer.SetMedia(_videoInfo.PathStream, mediaOptions);
            return result.State == Vlc.DotNet.Core.Interops.Signatures.MediaStates.NothingSpecial;
        }

        public void StartMonitoring() => mediaPlayer.Play();

        public void StopMonitoring()
        {
            
            if (mediaPlayer.CouldPlay == true)
            {
                mediaPlayer.Stop();
            }
        }
    }
}
