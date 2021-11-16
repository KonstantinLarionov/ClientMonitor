using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.Database.Entities
{
    public class DataForEdit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Date { get; set; }
    }
}
