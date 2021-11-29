using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.IO;
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
        INotificationFactory NotificationFactory;

        private readonly List<ControlVideoInfo> listReceiveVideoInfoIp = new List<ControlVideoInfo>()
        {
            new ControlVideoInfo
            {
                Name="Озон-ПГ-Выдача",
                PathStream=new Uri("rtsp://Goldencat1:123456@192.168.1.7:554/stream1"),
                //PathDownload=@"C:\Users\Big Lolipop\Desktop\ТестКамер\ZLOSE"
                PathDownload=@"C:\Test\Test1"
            },
            new ControlVideoInfo
            {
                Name="Озон-ПГ-Склад",
                //PathStream=new Uri("rtsp://Goldencat:123456@192.168.1.5:554/stream1"),
                PathStream=new Uri("http://158.58.130.148/mjpg/video.mjpg"),
                //PathDownload=@"C:\Users\Big Lolipop\Desktop\ТестКамер\KMXLM"
                PathDownload=@"C:\Test\Test2"
            }
        };
        public VideoControlHandler(IVideoControlFactory videoFactory, IRepository<LogInfo> repositoryLog, INotificationFactory notificationFactory)
        {
            videoControlFactory = videoFactory;
            dbLog = repositoryLog;
            _threads = new List<Thread>();
            _listCam = new List<IVideoControl>();
            NotificationFactory = notificationFactory;
        }


        public void Handle()
        {
            if (videoControlFactory.CreateAdaptors(listReceiveVideoInfoIp, VideoMonitoringTypes.IpCamera))
            {
                _listCam = videoControlFactory
                    .GetAdaptors(VideoMonitoringTypes.IpCamera)
                    .ToList();
            }
            var notifyer = NotificationFactory.GetNotification(NotificationTypes.Telegram);

            foreach (var item in _listCam)
            {
                Thread thread = new Thread(() =>
                {
                    item.ConnectionErrorEvent += (obj, error) =>
                        notifyer.SendMessage("-742266994", $"{item.Name} : Ошибка подключения к камере");

                    while (true)
                    {
                        item.StartMonitoring();
                        Thread.Sleep(20000);
                        item.StopMonitoring();
                    }
                });
                _threads.Add(thread);
            }
            _threads.ForEach(x => x.Start());
        }

    }
}
