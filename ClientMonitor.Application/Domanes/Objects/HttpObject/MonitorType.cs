using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ClientMonitor.Application.Domanes.Objects.HttpObject
{
    public enum MonitorType
    {
        //[EnumMember(Value = "Cookies")]
        //COOKIES,
        //[EnumMember(Value = "DB")]
        //DATABASE,
        [EnumMember(Value = "Трафик")]
        HTTP,
        [EnumMember(Value = "PC нагрузка")]
        PC,
        [EnumMember(Value = "Пользователи")]
        USER,
        [EnumMember(Value = "Импорт Пользователи")]
        IMPORTUSER,
        [EnumMember(Value = "Импорт PC нагрузка")]
        IMPORTPC,
        [EnumMember(Value = "Импорт Трафик")]
        IMPORTHTTP
    }
}
