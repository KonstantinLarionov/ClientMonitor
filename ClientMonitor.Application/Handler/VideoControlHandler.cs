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
        /// Лист с параметрами камер, stream1 - 1080p (1920×1080) stream2 - 360p (640×360)
        /// </summary>
        private readonly List<ControlVideoInfo> _listReceiveVideoInfoIp = new List<ControlVideoInfo>()
        {
            new ControlVideoInfo
            {
                Name="Озон-ПГ-Зал",
                PathStream=new Uri("rtsp://Goldencat:123456@192.168.1.7:554/stream2"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ТестКамер\Зал"
            },
            new ControlVideoInfo
            {
                Name="Озон-ПГ-Тамбур",
                PathStream=new Uri("rtsp://Goldencat1:123456@192.168.1.5:554/stream2"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ТестКамер\Тамбур"
            },
            new ControlVideoInfo
            {
                Name="Озон-ПГ-Выдача",
                PathStream=new Uri("rtsp://PoligonnayaZal:123456@192.168.1.9:554/stream2"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ТестКамер\Выдача"
            },
            new ControlVideoInfo
            {
                Name="Озон-ПГ-Склад",
                PathStream=new Uri("rtsp://PoligonnayaSklad:123456@192.168.1.11:554/stream2"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ТестКамер\Склад"
            },
            new ControlVideoInfo
            {
                Name="Озон-ПГ-Склад-2",
                PathStream=new Uri("rtsp://PoligonnayaSklad1:123456@192.168.1.10:554/stream2"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ТестКамер\Склад2"
            },
            new ControlVideoInfo
            {
                Name="Озон-ПГ-Тамбур-2",
                PathStream=new Uri("rtsp://PoligonnayaVhod1:123456@192.168.1.12:554/stream2"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ТестКамер\Тамбур2"
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
                    {
                        notifyer.SendMessage("-742266994", $"{item.Name} : Ошибка подключения к камере");
                        AddInLog($"Ошибка подключения к камере: {item.Name}");
                    };
                    item.InfoAboutLog += (obj, message) =>
                    {
                        notifyer.SendMessage("-742266994", $"{item.Name} : Ошибка подключения к камере");
                        AddInLog($"Ошибка подключения к камере: {item.Name}");
                    };
                    while (true)
                    {
                        item.StartMonitoring();
                        Thread.Sleep(300000);
                        item.StopMonitoring();
                    }
                });
                _threads.Add(thread);
            }
            _threads.ForEach(x => x.Start());
        }

        /// <summary>
        /// Метод добавления в бд
        /// </summary>
        /// <param name="message">Содержание лога</param>
        private void AddInLog(string message)
        {
            LogInfo log = new LogInfo
            {
                TypeLog = LogTypes.Error,
                Text = message,
                DateTime = DateTime.Now
            };
            _dbLog.AddInDb(log);
        }
    }
}
