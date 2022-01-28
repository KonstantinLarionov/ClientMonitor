using ClientMonitor.Application.Domanes.Objects.Regru;

namespace ClientMonitor.Application.Domanes.Request.Regru
{
    public sealed class GetInfoRequest : CommonRequest
    {
        public GetInfoRequest(string userName, string password)
        {
            Username = userName;
            Password = password;
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public override string EndPoint => "/service/get_info?input_data={\"services\":[{\"domain_name\":\"pamiatnikigm.ru\"},{\"service_id\":\"52803619\"}]}&input_format=json&output_content_type=plain&" + $"password=F8keu!r2&username=orenburgranit@yandex.ru";
        public override RequestMethod Method => RequestMethod.Get;
    }
}
