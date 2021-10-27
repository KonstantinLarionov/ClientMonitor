using ClientMonitor.Application.Abstractions;
using System;
using System.Collections.Generic;


namespace ClientMonitor.Infrastructure.ScreenRecording.Adaptors
{
    public class ScreenRecordingAdaptor : IScreenRecording
    {
        public ScreenRecordingAdaptor()
        {

        }

        public bool StartScreenRecording()
        {
            //SourceSettings
            //var a = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + "\\output.avi";
            throw new NotImplementedException();
        }

        public bool StopScreenRecording()
        {
            //bool createVideo = CreateVideo();
            //return (createVideo == true) ? true : false;
            throw new NotImplementedException();
        }

        private List<byte[]> CreateScreenToByte()
        {
            List<byte[]> screens = null;
            return screens;
        }

        private bool CreateVideo(List<byte[]> screens)
        {
            
            return true;
        }
    }
}
