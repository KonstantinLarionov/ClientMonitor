using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace ClientMonitor.Infrastructure.Database
{

    //public class InitialCreate : DbMigration
    //{
    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
    //        DateTime date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 30, 0);
    //        DateTime date2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 0, 0);

    //        modelBuilder.Entity<DataForEdit>().HasData(
    //            new DataForEdit[]
    //            {
    //            new DataForEdit { Name = "PathClaim", Date = "Путь выгрузки файлов ~Выдача", Value = @"C:\Users\Big Lolipop\Desktop\Записи с камер\video\ZLOSE"},
    //            new DataForEdit { Name = "PathStorage", Date = "Путь выгрузки файлов ~Склад", Value = @"C:\Users\Big Lolipop\Desktop\Записи с камер\video\KMXLM"},
    //            new DataForEdit { Name = "FormatFile", Date = "Формат выгрузки файлов", Value = "*mp4"},
    //            new DataForEdit { Name = "PathDownloadClaim", Date = "Путь хранения файлов в облаке ~Выдача", Value = "Записи/Выдача" },
    //            new DataForEdit { Name = "PathDownloadStorage", Date = "Путь хранения файлов в облаке ~Склад", Value = "Записи/Склад"},
    //            new DataForEdit { Name = "Mail", Date = "Почта для входа в облако", Value = "afc.studio@yandex.ru"},
    //            new DataForEdit { Name = "Pas", Date = "Пароль для входа в облако", Value = "lollipop321123"},
    //            new DataForEdit { Name = "TimeCloud", Date = "Время начала загрузки в облако~~Обновляется со следующей проверки!!!", Value = date2.ToString()},
    //            new DataForEdit { Name = "TimeFirst", Date = "Время первой проверки мониторинга характеристик ПК~~Обновляется со следующей проверки!!!", Value = date.ToString()},
    //            new DataForEdit { Name = "TimeSecond", Date = "Время второй проверки мониторинга характеристик ПК~~Обновляется со следующей проверки!!!", Value = date1.ToString()},
    //            new DataForEdit { Name = "PeriodMonitoring", Date = "Периодичность мониторинга сайтов/серверов", Value = "3600000"},
    //            new DataForEdit { Name = "IdChatServer", Date = "Id чата в телеграме для отправки сообщений по мониторингу сайтов и серверов ", Value = "-742266994"},
    //            new DataForEdit { Name = "IdChatMonitoring", Date = "Id чата в телеграме для отправки сообщений по мониторингу характеристик ПК", Value = "-693501604"},
    //            new DataForEdit { Name = "onOff", Date = "Проверка для остановки/запуска приложения", Value = "False"},
    //            });
    //    }


    //}



    //internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.Database.Contexts.LoggerContext>
    //{
    //    public Configuration()
    //    {
    //        AutomaticMigrationsEnabled = false;
    //        ContextKey = "MigrationApp.Models.UserContext";
    //    }

    //    protected override void Seed(MigrationApp.Models.UserContext context)
    //    {
    //    }
    //}
}
