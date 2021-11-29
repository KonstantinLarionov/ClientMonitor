using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ClientMonitor.Application.Handler
{
    /// <summary>
    /// Бизнес-логика?
    /// </summary>
    public class VideoControlHandler : IVideoControlHandler
    {

        private readonly IVideoControlFactory _videoControlFactory;
        private readonly IRepository<LogInfo> _dbLog;
        private readonly List<Thread> _threads;
        private List<IVideoControl> _listCam;
        INotificationFactory NotificationFactory;

        /// <summary>
        /// Лист с параметрами камер
        /// </summary>
        private readonly List<ControlVideoInfo> _listReceiveVideoInfoIp = new List<ControlVideoInfo>()
        {
            new ControlVideoInfo
            {
                Name="Озон-ПГ-Выдача",
                PathStream=new Uri("rtsp://Goldencat:123456@192.168.1.7:554/stream1"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ТестКамер\ZLOSE"
            },
            new ControlVideoInfo
            {
                Name="Озон-ПГ-Склад",
                PathStream=new Uri("rtsp://Goldencat1:123456@192.168.1.5:554/stream1"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ТестКамер\KMXLM"
            }
        };

        /// <summary>
        /// Подключение библиотек
        /// </summary>
        /// <param name="videoFactory">Видео</param>
        /// <param name="repositoryLog">Репозиторий логов</param>
        /// <param name="notificationFactory">Уведомления</param>
        public VideoControlHandler(IVideoControlFactory videoFactory, IRepository<LogInfo> repositoryLog, INotificationFactory notificationFactory)
        {
            _videoControlFactory = videoFactory;
            _dbLog = repositoryLog;
            _threads = new List<Thread>();
            _listCam = new List<IVideoControl>();
            NotificationFactory = notificationFactory;
        }

        /// <summary>
        /// Бизнес-логика? 
        /// </summary>
        public void Handle()
        {
            if (_videoControlFactory.CreateAdaptors(_listReceiveVideoInfoIp, VideoMonitoringTypes.IpCamera))
            {
                _listCam = _videoControlFactory
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
                        Thread.Sleep(900000);
                        item.StopMonitoring();
                    }
                });
                _threads.Add(thread);
            }
            _threads.ForEach(x => x.Start());
        }
    }
}
