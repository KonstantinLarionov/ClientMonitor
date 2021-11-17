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
            try
            {
                var proverka = db.EDataForEdit.FirstOrDefault(p => p.Id == 14);

                if (proverka == null)
                {
                    db.Database.EnsureCreated();
                    db.Database.Migrate();
                    var mon = new DataForEdit
                    {
                        Name=info.Name,
                        Note = info.Note,
                        Date = info.Date,
                    };
                    db.EDataForEdit.Add(mon);
                    db.SaveChanges();
                }
            }

            catch {
                db.Database.EnsureCreated();
                db.Database.Migrate();
                var mon = new DataForEdit
                {
                    Name = info.Name,
                    Note = info.Note,
                    Date = info.Date,
                };
                db.EDataForEdit.Add(mon);
                db.SaveChanges();
            }
        }

        //получить по Name параметр Date
        public string GetData(string old)
        {
            var editdata = db.EDataForEdit.Where(c => c.Name == old).Select(x=>x.Note).FirstOrDefault();
            return editdata;
        }

        public List<string> StatDb(DateTime dateTime)
        {
            throw new System.NotImplementedException();
        }
        //обновление записи в бд
        public void Update(string key, string news)
        {
            
            var editdata = db.EDataForEdit.Where(c => c.Name == key).FirstOrDefault();
            editdata.Note = news;
            db.SaveChanges();
        }
    }
}

