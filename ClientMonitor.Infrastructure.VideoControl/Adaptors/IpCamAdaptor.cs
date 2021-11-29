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
        public string Name
        {
            get
            {
                return _videoInfo.Name;
            }
        }
        private string NameFile
        {
            get
            {
                DateTime dt = DateTime.Now;
                return Path.Combine(_videoInfo.PathDownload, $"{_videoInfo.Name}_{dt.Year}_{dt.Month}_{dt.Day}_{dt.Hour}_{dt.Minute}_{dt.Second}.avi");
            }
        }
        public event EventHandler ConnectionErrorEvent;

        private readonly ControlVideoInfo _videoInfo;
        private readonly string _currentDirectory;
        private readonly DirectoryInfo _libDirectory;
        private readonly Vlc.DotNet.Core.VlcMediaPlayer _mediaPlayer;
        public IpCamAdaptor(ControlVideoInfo info)
        {
            _videoInfo = info;
            _currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            _libDirectory = new DirectoryInfo(Path.Combine(_currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));
            _mediaPlayer = new Vlc.DotNet.Core.VlcMediaPlayer(_libDirectory);
            _mediaPlayer.EncounteredError += (objec, message) =>
                ConnectionErrorEvent?.Invoke(objec, new ErrorEventArgs(new Exception(message.ToString())));
        }

        /// <summary>
        /// 
        /// </summary>
        public void StartMonitoring()
        {
            if (SetInfo())
            {
                _mediaPlayer.Play();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void StopMonitoring()
        {   
            if (_mediaPlayer.CouldPlay == true)
            {
                _mediaPlayer.Stop();
            }
        }

        /// <summary>
        /// 
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
