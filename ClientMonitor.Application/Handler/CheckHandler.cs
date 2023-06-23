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
            if (i > 5)
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
            //new ListDownloadCloud
            //{
            //    Name="Озон-ПГ-Зал",
            //    LocDownloadVideo=@"C:\Test\Баг2",
            //    LocDownloadCloud=@"C:\Test\Баг",
            //    FormatFiles="*.avi",
            //},
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Зал",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Ozon\Зал",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Ozon\Зал",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Тамбур",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Ozon\Тамбур",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Ozon\Тамбур",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Выдача",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Ozon\Выдача",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Ozon\Выдача",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Wb-ПГ-Склад",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Wildberries\Склад",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Wildberries\Склад",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Wb-ПГ-Склад-2",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Wildberries\Склад2",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Wildberries\Склад2",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Тамбур-2",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Ozon\Тамбур2",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Ozon\Тамбур2",
                FormatFiles="*.mp4",
            },

            new ListDownloadCloud
            {
                Name="WB-ПГ-Выдача",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Wildberries\Выдача",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Wildberries\Выдача",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="WB-ПГ-Выдача2",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Wildberries\Выдача2",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Wildberries\Выдача2",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Склад",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Ozon\Склад",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Ozon\Склад",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Ломбард1-ПГ",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Ломбард\Ломбард1",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Ломбард\Ломбард1",
                FormatFiles="*.mp4",
            },
            new ListDownloadCloud
            {
                Name="Озон-ПГ-Склад2",
                LocDownloadVideo=@"E:\ЗаписиКамерNew\Ozon\Склад2",
                LocDownloadCloud=@"E:\ЗаписиКамерNew\Ozon\Склад2",
                FormatFiles="*.mp4",
            },
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
