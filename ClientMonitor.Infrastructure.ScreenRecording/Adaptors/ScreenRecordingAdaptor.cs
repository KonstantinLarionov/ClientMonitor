using ClientMonitor.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.ScreenRecording.Adaptors
{
    public class ScreenRecordingAdaptor : IScreenRecording
    {
        public ScreenRecordingAdaptor(string filename)
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
            Bitmap printscreen = new Bitmap(1280, 1024);
            Graphics graphics = Graphics.FromImage(printscreen as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
            printscreen.Save(DateTime.Now.ToString(), System.Drawing.Imaging.ImageFormat.Jpeg);
            throw new NotImplementedException();
        }
    }
}
