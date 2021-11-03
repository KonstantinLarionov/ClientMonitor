using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Domanes.Enums
{
    public enum ClientResourseType
    {
        [EnumMember(Value = "")]
        None,
        [EnumMember(Value = "site")]
        Site,
        [EnumMember(Value = "metric")]
        Metric,
    }
}
