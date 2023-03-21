using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using LibVLCSharp.Shared;
using System;
using System.IO;
using System.Threading;

namespace ClientMonitor.Infrastructure.VideoControl.Adaptors
{
  /// <summary>
  /// Адаптор для подключения трансляции к плееру VLC и настройка
  /// </summary>
  public class IpCamAdaptor : IVideoControl
  {
    /// <summary>
    /// Получение имени параметров камеры
    /// </summary>
    public string Name
    {
      get
      {
        return _videoInfo.Name;
      }
    }

    /// <summary>
    /// Метод построения названия файла
    /// </summary>
    private string NameFile
    {
      get
      {
        DateTime dt = DateTime.Now;

        DirectoryInfo dirInfo = new DirectoryInfo(_videoInfo.PathDownload + "\\" + MonthStats(dt));
        if (!dirInfo.Exists)
        {
          dirInfo.Create();
        }
        Pathfile = _videoInfo.PathDownload + "\\" + MonthStats(dt) + $"\\{_videoInfo.Name}_{dt.Year}.{dt.Month}.{dt.Day}__{dt.Hour}-{dt.Minute}-{dt.Second}.mp4";
        return Path.Combine(_videoInfo.PathDownload + "\\" + MonthStats(dt), $"{_videoInfo.Name}_{dt.Year}.{dt.Month}.{dt.Day}__{dt.Hour}-{dt.Minute}-{dt.Second}.mp4");
      }
    }
    public event EventHandler ConnectionErrorEvent;
    public event EventHandler InfoAboutLog;

    private readonly ControlVideoInfo _videoInfo;
    private readonly MediaPlayer _mediaPlayer;
    private Media _media;
    LibVLC _libVLC;
    private string Pathfile;
    /// <summary>
    /// Настройка плеера, подгрузка библиотек
    /// </summary>
    /// <param name="info"> Параметры камеры</param>
    public IpCamAdaptor(ControlVideoInfo info)
    {
      _videoInfo = info;
      _libVLC = new LibVLC();
      _mediaPlayer = new MediaPlayer(_libVLC);

      ////_mediaPlayer.EndReached += (_2, _3) => DelayRestartMediaPlayer(_libVLC, _mediaPlayer);
      ////_mediaPlayer.EncounteredError+=(_2, _3) => DelayRestartMediaPlayer(_libVLC, _mediaPlayer);
      //_mediaPlayer.Stopped += (_2, _3) => DelayRestartMediaPlayer(_libVLC, _mediaPlayer);
      ////_mediaPlayer.Playing += (_2, _3) => RestartMediaPlayer(_libVLC, _mediaPlayer);
      //_mediaPlayer.Paused += (_2, _3) => DelayRestartMediaPlayer(_libVLC, _mediaPlayer);
      //_mediaPlayer.LengthChanged+= CheckSize;
    }
    private bool Check = false;

    private void DelayRestartMediaPlayer(LibVLC libVLC, MediaPlayer mediaPlayer)
    {
      Thread.Sleep(10000);
      _ = ThreadPool.QueueUserWorkItem(_ => StartMonitoring());
    }

    /// <summary>
    /// Получение названия папки по дате
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    private static string MonthStats(DateTime dateTime)
    {
      MonthTypes monthTypes = (MonthTypes)Enum.GetValues(typeof(MonthTypes)).GetValue(dateTime.Month);
      string data = $"{dateTime.Year}\\{monthTypes}\\{dateTime.Day}";
      return data;
    }


    /// <summary>
    /// запуск плеера
    /// </summary>
    public void StartMonitoring()
    {
      _media = new Media(_libVLC, _videoInfo.PathStream.ToString(), FromType.FromLocation);
      _media.AddOption(":sout=#transcode{acodec=mp3,vcodec=x264}:file{dst=" + NameFile + "}");
      ////_media.AddOption(":sout=#transcode{acodec=mp4a, vcodec=h265}:file{dst=" + NameFile + "}");
      _media.AddOption(":sout-keep");
      _media.AddOption(":live-caching=3000");
      _media.AddOption(":loop");
      _media.AddOption(":network-caching=300");
      _media.AddOption(":http-continuous");

      //_media.AddOption(":sout-transcode-venc=x264");

      //_media.AddOption(":sout-x264-tune=stillimage");
      //_media.AddOption(":sout-x264-lookahead=1");

      // _media = new Media(_libVLC, _videoInfo.PathStream.ToString(), FromType.FromLocation);
      // ////_media.AddOption(":sout=#transcode{width=1920,canvas-height=1080.vcodec=avi,vb=800,scale=1,acodec=avia,ab=128,channels=1,samplerate=44100}:gather:file{dst=" + NameFile + "}");
      ///// _media.AddOption(":sout=#transcode{width=1920,canvas-height=1080.vcodec=h264,vb=800,scale=1,acodec=mp3,ab=128,channels=1,samplerate=44100}:gather:file{dst=" + NameFile + "}");
      // ////_media.AddOption(":sout=#gather:file{dst=" + NameFile + "}");
      // _media.AddOption(":sout-keep");
      // ///_media.AddOption(":live-caching=60000");
      // _media.AddOption(":loop");
      // ///_media.AddOption(":network-caching=60000");
      // ///
      // _media.AddOption(":http-reconnect");
      // _media.AddOption(":http-continuous");
      //--http-reconnect, --no-http-reconnect
      _mediaPlayer.Play(_media);
    }

    /// <summary>
    /// остановка плеера
    /// </summary>
    public void StopMonitoring()
    {
      if (_mediaPlayer.IsPlaying == true)
      {
        _mediaPlayer.Stop();
      }
    }
  }
}