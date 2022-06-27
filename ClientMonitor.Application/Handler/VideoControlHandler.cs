using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

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
            //new ControlVideoInfo
            //{
            //    Name="Баг",
            //    PathStream=new Uri("rtsp://PoligonnayaZal:123456@92.255.240.7:9095/stream2"),
            //    PathDownload=@"C:\Test\Баг"
            //},
            new ControlVideoInfo
            {
                Name="Озон-ПГ-Зал",
                PathStream=new Uri("rtsp://Goldencat:123456@192.168.1.3:554/stream1"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Зал"
            },
            new ControlVideoInfo
            {
                Name="Озон-ПГ-Тамбур",
                PathStream=new Uri("rtsp://Goldencat1:123456@192.168.1.10:554/stream1"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Тамбур"
            },
            new ControlVideoInfo
            {
                Name="Озон-ПГ-Выдача",
                PathStream=new Uri("rtsp://PoligonnayaZal:123456@192.168.1.9:554/stream1"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Выдача"
            },
            new ControlVideoInfo
            {
                Name="Озон-ПГ-Склад",
                PathStream=new Uri("rtsp://PoligonnayaSklad:123456@192.168.1.11:554/stream1"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Склад"
            },
            new ControlVideoInfo
            {
                Name="Озон-ПГ-Склад-2",
                PathStream=new Uri("rtsp://PoligonnayaSklad1:123456@192.168.1.12:554/stream1"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Склад2"
            },
            new ControlVideoInfo
            {
                Name="Озон-ПГ-Тамбур-2",
                PathStream=new Uri("rtsp://PoligonnayaVhod1:123456@188.186.238.120:554/stream1"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ozon\Тамбур2"
            },
            new ControlVideoInfo
            {
                Name="WB-ПГ-Выдача",
                PathStream=new Uri("rtsp://WbPgVidacha1:123456@192.168.1.4:554/stream1"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Выдача"
            },
            new ControlVideoInfo
            {
                Name="WB-ПГ-Выдача-2",
                PathStream=new Uri("rtsp://WbPgVidacha2:123456@192.168.1.5:554/stream1"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Выдача2"
            },
            new ControlVideoInfo
            {
                Name="WB-ПГ-Склад",
                PathStream=new Uri("rtsp://WbPgSklad:123456@192.168.1.6:554/stream1"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Wildberries\Склад"
            },
            new ControlVideoInfo
            {
                Name="Ломбард1-ПГ",
                PathStream=new Uri("rtsp://LombardPg:123456@188.186.238.120:6060/stream1"),
                PathDownload=@"C:\Users\Big Lolipop\Desktop\ЗаписиКамер\Ломбард\Ломбард1"
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
        public async Task Handle()
        {
            DateTime dt = DateTime.Now;
            if (_videoControlFactory.CreateAdaptors(_listReceiveVideoInfoIp, VideoMonitoringTypes.IpCamera))
            {
                _listCam = _videoControlFactory
                    .GetAdaptors(VideoMonitoringTypes.IpCamera)
                    .ToList();
            }
            var notifyer = NotificationFactory.GetNotification(NotificationTypes.Telegram);
            int i = -1;
            foreach (var item in _listCam)
            {
                Thread thread = new Thread(async () =>
                {
                    var tokenSource = new CancellationTokenSource();
                    //item.ConnectionErrorEvent += (obj, error) => notifyer.SendMessage("-742266994", $"{item.Name} : Ошибка подключения к камере");
                    //item.InfoAboutLog += (obj, message) => notifyer.SendMessage("-742266994", $"{item.Name} : Ошибка подключения к камере");

                    while (true)
                    {
                        item.StartMonitoring();
                        Thread.Sleep(60000);
                        item.StopMonitoring();
                        Thread.Sleep(2000);
                    }
                });
                _threads.Add(thread);
                i++;
            }
            _threads.ForEach(x => x.Start());
        }

        //private void Error(int i)
        //{
        //    _threads[i].();
        //    Thread.Sleep(20000);
        //    _threads[i].Start();
        //}
    }
}
