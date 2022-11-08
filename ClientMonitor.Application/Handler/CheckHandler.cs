using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.IO;

namespace ClientMonitor.Application.Handler
{
  public class CheckHandler : ICheckHandler
  {
    INotificationFactory NotificationFactory;

    /// <summary>
    /// Подключение библиотек
    /// </summary>
    public CheckHandler(INotificationFactory notificationFactory)
    {
      NotificationFactory = notificationFactory;
    }

    public void CheckHandle()
    {
      var notifyer = NotificationFactory.GetNotification(NotificationTypes.Telegram);
      foreach (var listClouds in _listClouds)
      {
        try
        {
          DateTime dt = DateTime.Now;

          DirectoryInfo dirInfo = new DirectoryInfo(listClouds.LocDownloadVideo + "\\" + MonthStats(dt));
          string[] allFoundFiles = Directory.GetFiles(listClouds.LocDownloadVideo + "\\" + MonthStats(dt), "", SearchOption.AllDirectories);
          int i = 0;
          if (allFoundFiles.Length > 0)
          {
            string[] files = GetWitoutLastElement(allFoundFiles, allFoundFiles.Length);
            foreach (var file in files)
            {
              FileInfo fileInf = new FileInfo(file);

              if (fileInf.Length < 300000)
              {
                i++;
              }
            }
            if (i > 20)
            {
              notifyer.SendMessage("-693501604", listClouds.Name + " Проверьте запись видео");
              foreach (var file1 in files)
              {
                FileInfo fileInf = new FileInfo(file1);

                if (fileInf.Length < 300000)
                {
                  fileInf.Delete();
                }
              }
            }
          }
        }
        catch (Exception e) { }
      }
    }

    private string[] GetWitoutLastElement(string[] mas, int leght)
    {
      string[] files = new string[leght - 2];
      for (int i = 0; i < leght - 2; i++)
        files[i] = mas[i];
      return files;
    }

    /// <summary>
    /// Список параметров для выгрузки в облако
    /// </summary>
    private readonly static List<ListDownloadCloud> _listClouds = new List<ListDownloadCloud>()
        {
            new ListDownloadCloud
            {
                Name="Озон-МГ-Вход",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Вход",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Вход",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-МГ-Зал",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Зал",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Зал",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-МГ-Склад",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Склад",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Склад",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="Озон-МГ-Склад2",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Склад2",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Ozon\Склад2",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-МГ-Зал",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Зал",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Зал",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-МГ-Склад",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Склад",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Склад",
                FormatFiles="*.avi",
            },

            new ListDownloadCloud
            {
                Name="WB-МГ-Зал3",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Зал3",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Зал3",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-МГ-Кухня",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Кухня",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Кухня",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-МГ-Тамбур",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Тамбур",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Тамбур",
                FormatFiles="*.avi",
            },
            new ListDownloadCloud
            {
                Name="WB-МГ-Склад2",
                LocDownloadVideo=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Склад2",
                LocDownloadCloud=@"C:\Users\BigLollipop\Documents\Записи с камер\Wb\Склад2",
                FormatFiles="*.avi",
            }
        };

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
  }
}