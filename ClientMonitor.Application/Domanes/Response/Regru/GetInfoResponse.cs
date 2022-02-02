using ClientMonitor.Application.Domanes.Objects.Regru;

using Newtonsoft.Json;

using System.Collections.Generic;


namespace ClientMonitor.Application.Domanes.Response.Regru
{
    /// <summary>
    /// респонс регру
    /// </summary>
    public class GetInfoResponse
    {
        [JsonConstructor]
        public GetInfoResponse(AnswerData answer, string charset, string messageStore, string result)
        {
            Answer = answer;
            Charset = charset;
            MessageStore = messageStore;
            Result = result;
        }
        [JsonProperty("answer")]
        public AnswerData Answer { get; }
        [JsonProperty("charset")]
        public string Charset { get; }
        [JsonProperty("messagestore")]
        public string MessageStore { get; }
        [JsonProperty("result")]
        public string Result { get; }
    }

    public class AnswerData
    {
        [JsonConstructor]
        public AnswerData(List<GetInfoData> services)
        {
            Services = services;
        }
        [JsonProperty("services")]
        public List<GetInfoData> Services { get; }
    }
}
