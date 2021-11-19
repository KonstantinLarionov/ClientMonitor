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
        /// <summary>
        /// получить по Name параметру значение Value
        /// </summary>
        /// <param name="old"></param>
        /// <returns></returns>
        public string GetData(string name)
        {
            try {
                var editdata = db.EDataForEdit.Where(c => c.Name == name).Select(x => x.Value).FirstOrDefault();
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
        /// <summary>
        /// Редактирование параметров в бд(чтоб пользователь мог изменять начальные значения параметров). Key - ключ по названию параметра
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
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

