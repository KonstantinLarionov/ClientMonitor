using ClientMonitor.Application.Domanes.Objects.Regru;
using System;

namespace ClientMonitor.Application.Domanes.Request.Metrika
{
    /// <summary>
    /// Формат реквеста на получение данных по времени счётчика яндекс метрики
    /// </summary>
    public sealed class GetDataByTimeRequest : CommonRequest
    {
        public GetDataByTimeRequest(int ids, DateTime date1, DateTime date2)
        {
            Ids = ids;
            Date1 = date1;
            Date2 = date2;
        }

        public int Ids { get; set; }
        public DateTime Date1 { get; set; }
        public DateTime Date2 { get; set; }
        public override string EndPoint => $"/data?metrics=ym:s:pageviews&id={Ids}&date1={GetDateTime(Date1)}&date2={GetDateTime(Date2)}";
        public override RequestMethod Method => RequestMethod.Get;
        private string GetDateTime (DateTime dt)
        {
            string dateTime = dt.Year+"-"+dt.Month+"-"+dt.Day;
            return dateTime;
        }
    }
}
