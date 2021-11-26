using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ClientMonitor.Application.Handler
{
    public class VideoControlHandler : IVideoControlHandler
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IVideoControlFactory videoControlFactory;
        private readonly IRepository<LogInfo> dbLog;
        private readonly List<Thread> _threads;
        private List<IVideoControl> _listCam;

        private readonly List<ControlVideoInfo> listReceiveVideoInfoIp = new List<ControlVideoInfo>()
        {
            new ControlVideoInfo
            {
                Name="Stream1",
                PathStream=new Uri("rtsp://TestCam:123456@192.168.89.30:554/stream1"),
                PathDownload=@"C:\Test\Test1"
            },
            new ControlVideoInfo
            {
                Name="Stream2",
                PathStream=new Uri("http://158.58.130.148/mjpg/video.mjpg"),
                PathDownload=@"C:\Test\Test2"
            }
        };
        public VideoControlHandler(IVideoControlFactory videoFactory, IRepository<LogInfo> repositoryLog)
        {
            videoControlFactory = videoFactory;
            dbLog = repositoryLog;
            _threads = new List<Thread>();
            _listCam = new List<IVideoControl>();
        }


        public void Handle()
        {
            if (videoControlFactory.CreateAdaptors(listReceiveVideoInfoIp, VideoMonitoringTypes.IpCamera))
            {
                _listCam = videoControlFactory
                    .GetAdaptors(VideoMonitoringTypes.IpCamera)
                    .ToList();
            }

            foreach (var item in _listCam)
            {
                Thread thread = new Thread(() =>
                {
                    try
                    { item.StartMonitoring(); }
                    catch 
                    { 
                        //TODO : Telegram
                        //TODO : item.name
                    }
                });
                _threads.Add(thread);
            }

            _threads.ForEach(x => x.Start());
        }
    }
}
