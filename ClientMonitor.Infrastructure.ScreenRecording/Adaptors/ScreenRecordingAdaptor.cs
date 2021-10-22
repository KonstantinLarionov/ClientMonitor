using ClientMonitor.Application.Abstractions;
using System;
using VisioForge.Types.Sources;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.ScreenRecording.Adaptors
{
    public class ScreenRecordingAdaptor : IScreenRecording
    {
        ScreenCaptureSourceSettings SourceSettings;

        public ScreenRecordingAdaptor()
        {
            SourceSettings = new ScreenCaptureSourceSettings() { FullScreen = true };
            

        }

        public bool StartScreenRecording()
        {
            //SourceSettings
            //var a = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + "\\output.avi";
            throw new NotImplementedException();

        }

        public bool StopScreenRecording()
        {
            throw new NotImplementedException();
        }
    }
}
