using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ClientMonitor.Application.Handler
{
    public class VideoControlHandler : IVideoControlHandler
    {
        IVideoControlFactory videoControlFactory;
        IRepository<LogInfo> dbLog;

        public VideoControlHandler(IVideoControlFactory videoFactory, IRepository<LogInfo> repositoryLog)
        {
            videoControlFactory = videoFactory;
            dbLog = repositoryLog;
        }

        private readonly static List<ControlVideoInfo> listReceiveVideoInfo = new List<ControlVideoInfo>()
        {
            new ControlVideoInfo
            {
                Name="Stream1",
                PathStream=new Uri("rtsp://TestCam:123456@192.168.89.30:554/stream1"),
                PathDownload=@"C:\Test\Test1"
            },
            //new ReceiveVideoInfo
            //{
            //    Name="Stream2",
            //    PathStream=new Uri("http://158.58.130.148/mjpg/video.mjpg"),
            //    PathDownload=@"C:\Test\Test2"
            //}
        };

        public void Handle()
        {
            foreach (var i in listReceiveVideoInfo)
            {
                Thread thread = new Thread(() =>
                {
                List<ResultVideoControl> results = new List<ResultVideoControl>();
                var monitor = videoControlFactory.GetVideoMonitoring(VideoMonitoringTypes.IpCamera);
                monitor.StartMonitoring(i);
                });
                thread.Start();
            }
        }
    }
}
