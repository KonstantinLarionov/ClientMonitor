﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.Database.Entities
{
    public class EntitiesProc
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Process { get; set; }
    }
}
