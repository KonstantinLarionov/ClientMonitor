using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects.Regru;
using System.Collections.Generic;

namespace ClientMonitor.Application.Domanes.Request.Regru
{
    /// <summary>
    /// Формат реквеста на получение информации о хостингах и доменах регру
    /// </summary>
    public sealed class GetInfoRequest : CommonRequest
    {
        public GetInfoRequest(string userName, string password, string servtype)
        {
            Username = userName;
            Password = password;
            Servtype = servtype;
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Servtype { get; set; }
        public override string EndPoint => "/service/get_list?input_data={\"servtype\":\""+Servtype+"\"}&input_format=json&output_content_type=plain&" + $"password={Password}&username={Username}";
        public override RequestMethod Method => RequestMethod.Get;
    }
}
