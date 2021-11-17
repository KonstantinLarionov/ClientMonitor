using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Abstractions
{
    public interface IRepository<T>
    {
        void AddInDb(T info);
        void Update(string key,string news);
        List<string> StatDb(DateTime dateTime);
        string GetData(string old);
    }
}
