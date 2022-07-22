using Newtonsoft.Json;

using System;

namespace ClientMonitor.Application.Domanes.Objects.Regru
{
    public class GetInfoData
    {
        [JsonConstructor]
        public GetInfoData(DateTime creationDate, string dname, DateTime expirationDate, string result, long serviceId, string servtype, string state,
            string subtype, string userServid)
        {
            CreationDate = creationDate;
            Dname = dname;
            ExpirationDate = expirationDate;
            Result = result;
            ServiceId = serviceId;
            Servtype = servtype;
            State = state;
            Subtype = subtype;
            UserServid = userServid;
        }

        [JsonProperty("creation_date")]
        public DateTime CreationDate { get; }
        [JsonProperty("dname")]
        public string Dname { get; }
        [JsonProperty("expiration_date")]
        public DateTime ExpirationDate { get; }
        [JsonProperty("result")]
        public string Result { get; }
        [JsonProperty("service_id")]
        public long ServiceId { get; }
        [JsonProperty("servtype")]
        public string Servtype { get; }
        [JsonProperty("state")]
        public string State { get; }
        [JsonProperty("subtype")]
        public string Subtype { get; }
        [JsonProperty("user_servid")]
        public string UserServid { get; }
    }
}
