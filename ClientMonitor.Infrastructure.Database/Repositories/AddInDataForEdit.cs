using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using ClientMonitor.Infrastructure.Database.Contexts;
using ClientMonitor.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            throw new System.NotImplementedException();
        }

        //получить по Name параметр Date
        public string GetData(string old)
        {
            try {
                var editdata = db.EDataForEdit.Where(c => c.Name == old).Select(x => x.Value).FirstOrDefault();
                return editdata;
            }
            catch
            {
                return "";
            }
        }

        public List<string> StatDb(DateTime dateTime)
        {
            throw new System.NotImplementedException();
        }
        //обновление записи в бд
        public void Update(string key, string value)
        {
            db.Database.EnsureCreated();
            //db.Database.Migrate();
            var editdata = db.EDataForEdit.Where(c => c.Name == key).FirstOrDefault();
            editdata.Value = value;
            db.SaveChanges();
        }
    }
}

