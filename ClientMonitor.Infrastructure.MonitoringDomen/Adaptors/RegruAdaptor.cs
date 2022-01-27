using ClientMonitor.Application.Abstractions.Regru;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Application.Domanes.Request;
using ClientMonitor.Application.Domanes.Request.Regru;
using ClientMonitor.Application.Domanes.Response.Regru;
using ClientMonitor.Application.Handler.JsonHandlers;
using RestSharp;
using System;

namespace ClientMonitor.Infrastructure.MonitoringDomen.Adaptors
{
    public class RegruAdaptor : IRegru
    {
        private readonly UserRegruData _videoInfo;

        private static RestClient client = new RestClient("https://api.reg.ru/api/regru2/");

        private readonly ISingleMessageHandler<GetInfoResponse> _getGetInfoResponse;
        public GetInfoResponse HandleGetInfoResponse(string message) => _getGetInfoResponse.HandleSingle(message);

        public RegruAdaptor(UserRegruData info)
        {
            _videoInfo = info;
        }
        
        public object ReceiveInfoMonitoring()
        {
            var k = HandleGetInfoResponse(SendRequest(new GetInfoRequest(_videoInfo.Username, _videoInfo.Password)));
            
            throw new NotImplementedException();
        }

        private static string SendRequest(CommonRequest request)
        {
            var req = new RestRequest(request.EndPoint, (Method)request.Method);
            var res = client.Execute(req);
            return res.Content;
        }
    }
}
