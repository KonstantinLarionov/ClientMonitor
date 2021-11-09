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
        List<double> StatDb(DateTime dateTime);
    }
}
