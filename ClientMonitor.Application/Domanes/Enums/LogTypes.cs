using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Domanes.Enums
{
    public enum LogTypes
    {
        [EnumMember(Value = "")]
        None,
        [EnumMember(Value = "Информация")]
        Information,
        [EnumMember(Value = "Ощибка")]
        Error,
        [EnumMember(Value = "Предупреждение")]
        Warning,
    }
}
