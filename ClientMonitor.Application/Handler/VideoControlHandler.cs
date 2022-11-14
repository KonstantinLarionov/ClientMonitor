﻿using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.IO;
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
                Name="Озон-МГ-Вход",
                PathStream=new Uri("rtsp://Mgvhod:123456@192.168.2.4:554/stream1"),
                PathDownload=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Вход"
            },
            new ControlVideoInfo
            {
                Name="Озон-МГ-Зал",
                PathStream=new Uri("rtsp://Mgzall:123456@192.168.2.6:554/stream1"),
                PathDownload=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Зал"
            },
            new ControlVideoInfo
            {
                Name="Озон-МГ-Склад",
                PathStream=new Uri("rtsp://Mgsklad:123456@192.168.2.11:554/stream1"),
                PathDownload=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Склад"
            },
            new ControlVideoInfo
            {
                Name="Озон-МГ-Склад2",
                PathStream=new Uri("rtsp://Mgsklad2:123456@192.168.2.5:554/stream1"),
                PathDownload=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Склад2"
            },

            new ControlVideoInfo
            {
                Name="Wb-МГ-Зал",
                PathStream=new Uri("rtsp://WbMgZal:123456@192.168.2.3:554/stream1"),
                PathDownload=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Зал"
            },
            new ControlVideoInfo
            {
                Name="Wb-МГ-Склад",
                PathStream=new Uri("rtsp://WbMgZal2:123456@192.168.2.12:554/stream1"),
                PathDownload=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Склад"
            },
            new ControlVideoInfo
            {
                Name="Wb-МГ-Зал3",
                PathStream=new Uri("rtsp://WbMgZal3:123456@192.168.2.16:554/stream1"),
                PathDownload=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Зал3"
            },
            new ControlVideoInfo
            {
                Name="Wb-МГ-Кухня",
                PathStream=new Uri("rtsp://WbMgKyh:123456@192.168.2.15:554/stream1"),
                PathDownload=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Кухня"
            },
            new ControlVideoInfo
            {
                Name="Wb-МГ-Тамбур",
                PathStream=new Uri("rtsp://WbMgTambur:123456@192.168.2.14:554/stream1"),
                PathDownload=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Тамбур"
            },
            new ControlVideoInfo
            {
                Name="Wb-МГ-Склад2",
                PathStream=new Uri("rtsp://MgWbSklad:123456@192.168.2.19:554/stream1"),
                PathDownload=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Склад2"
            }
        };

    /// <summary>
    /// Подключение библиотек
    /// </summary>
    /// <param name="videoFactory">Видео</param>
    /// <param name="repositoryLog">Репозиторий логов</param>
    /// <param name="notificationFactory">Уведомления</param>
    public VideoControlHandler(IVideoControlFactory videoFactory, INotificationFactory notificationFactory)
    {
      _videoControlFactory = videoFactory;
      _threads = new List<Thread>();
      _listCam = new List<IVideoControl>();
      NotificationFactory = notificationFactory;

      using (StreamReader reader = new StreamReader("camers.txt"))
      {
        string line;
        int i = 0;
        while ((line = reader.ReadLine()) != null)
        {
          string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
          _listReceiveVideoInfoIp[i].PathStream = new Uri(words[1]);
          i++;
        }
      }
    }

    /// <summary>
    /// Бизнес-логика? 
    /// </summary>
    public void Handle()
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
        Thread thread = new Thread(() =>
        {
          while (true)
          {
            item.StartMonitoring();
            Thread.Sleep(480000);
            item.StopMonitoring();
            Thread.Sleep(2000);
          }
        });
        _threads.Add(thread);
        i++;
      }
      _threads.ForEach(x => x.Start());
    }
  }
}
