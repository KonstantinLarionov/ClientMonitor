using System;
using System.Collections.Generic;
using System.Text;

namespace ClientMonitor.Application.Domanes.Objects.HttpObject
{
    public sealed class MonitorOptions
    {
        public MonitorOptions(int? hour, int? minute, int? second, int? millisecond = 0)
        {
            Minute = minute;
            Second = second;
            Hour = hour;
            Millisecond = millisecond;
        }

        public int? Minute { get; set; }
        public int? Second { get; set; }
        public int? Millisecond { get; set; }
        public int? Hour { get; set; }
    }
}
