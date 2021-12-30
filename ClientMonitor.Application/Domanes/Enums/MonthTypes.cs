using System.Runtime.Serialization;

namespace ClientMonitor.Application.Domanes.Enums
{
    public enum MonthTypes
    {
        [EnumMember(Value = "")]
        None,
        [EnumMember(Value = "Январь")]
        January,
        [EnumMember(Value = "Февраль")]
        February,
        [EnumMember(Value = "Март")]
        March,
        [EnumMember(Value = "Апрель")]
        April,
        [EnumMember(Value = "Май")]
        May,
        [EnumMember(Value = "Июнь")]
        June,
        [EnumMember(Value = "Июль")]
        July,
        [EnumMember(Value = "Август")]
        August,
        [EnumMember(Value = "Сентябрь")]
        September,
        [EnumMember(Value = "Октябрь")]
        October,
        [EnumMember(Value = "Ноябрь")]
        November,
        [EnumMember(Value = "Декабрь")]
        December,
    }
}
