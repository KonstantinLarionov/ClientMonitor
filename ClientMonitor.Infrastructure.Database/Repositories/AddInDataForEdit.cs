using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.Database.Repositories
{

    public class AddInDataForEdit : IRepository<DataForEditInfo>
    {
        private LoggerContext db;

        public AddInDataForEdit()
        {
            db = new LoggerContext();
        }
        public void AddInDb(DataForEditInfo info)
        {
            var proverka = db.EDataForEdit.FirstOrDefault(p => p.Id == 12);

            if (proverka == null)
            {
                db.Database.EnsureCreated();
                db.Database.Migrate();
                var mon = new DataForEdit
                {
                    Note = info.Note,
                    Date = info.Date,
                };
                db.EDataForEdit.Add(mon);
                db.SaveChanges();
            }


            //try
            //{
            //    if (!db.EDataForEdit.Any()) { }
            //}
            //catch
            //{
            //    #region [SomeData]

            //    var a = new DataForEdit { Date = "Путь выгрузки файлов ~Выдача", Note = @"C:\Users\Big Lolipop\Desktop\Записи с камер\video\ZLOSE" };
            //    db.EDataForEdit.Add(a);
            //    var asklad = new DataForEdit { Date = "Путь выгрузки файлов ~Склад", Note = @"C:\Users\Big Lolipop\Desktop\Записи с камер\video\KMXLM" };
            //    db.EDataForEdit.Add(asklad);
            //    var a1 = new DataForEdit { Date = "Формат выгрузки файлов", Note = "" };
            //    db.EDataForEdit.Add(a1);
            //    var a2 = new DataForEdit { Date = "Путь загрузки файлов в облаке", Note = "" };
            //    db.EDataForEdit.Add(a2);
            //    var amail = new DataForEdit { Date = "Почта для входа в облако", Note = "afc.studio@yandex.ru" };
            //    db.EDataForEdit.Add(amail);
            //    var apas = new DataForEdit { Date = "Пароль для входа в облако", Note = "lollipop321123" };
            //    db.EDataForEdit.Add(apas);

            //    DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);
            //    DateTime date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 30, 0);
            //    DateTime date2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 0, 0);

            //    var timecloud = new DataForEdit { Date = "Время начала загрузки в облако", Note = date2.ToString() };
            //    db.EDataForEdit.Add(timecloud);

            //    var timestart = new DataForEdit { Date = "Время первой проверки мониторинга характеристик ПК", Note = date2.ToString() };
            //    db.EDataForEdit.Add(timestart);
            //    var timeend = new DataForEdit { Date = "Время второй проверки мониторинга характеристик ПК", Note = date2.ToString() };
            //    db.EDataForEdit.Add(timeend);

            //    var a5 = new DataForEdit { Date = "Периодичность мониторинга сайтов/серверов", Note = "" };
            //    db.EDataForEdit.Add(a5);

            //    var a7 = new DataForEdit { Date = "Id чата в телеграме для отправки сообщений по мониторингу сайтов и серверов ", Note = "-742266994" };
            //    db.EDataForEdit.Add(a7);
            //    var a8 = new DataForEdit { Date = "Id чата в телеграме для отправки сообщений по мониторингу характеристик ПК", Note = "-693501604" };
            //    db.EDataForEdit.Add(a8);
            //    #endregion
            //}
           // db.SaveChanges();

        }

        public List<string> StatDb(DateTime dateTime)
        {
            throw new System.NotImplementedException();
        }
    }
}

