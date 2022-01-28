using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Application.Domanes.Request;
using ClientMonitor.Application.Domanes.Request.Regru;
using ClientMonitor.Application.Domanes.Response.Regru;
using ClientMonitor.Application.Handler.JsonHandlers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientMonitor.Application.Handler
{
    /// <summary>
    /// Бизнес-логика?
    /// </summary>
    public class RegruHandler : IRegruHandler
    {
        private readonly ISingleMessageHandler<GetInfoResponse> _getGetInfoResponse;

        IRegruHandlerFactory _regruFactory;
        INotificationFactory _notificationFactory;

        public RegruHandler(IRegruHandlerFactory iRegruFactory, INotificationFactory notificationFactory, IRegruHandlerFactory factory)
        {
            _getGetInfoResponse = factory.CreateGetInfoResponse();
            _regruFactory = iRegruFactory;
            _notificationFactory = notificationFactory;
        }

        public GetInfoResponse HandleGetInfoResponse(string message) => _getGetInfoResponse.HandleSingle(message);
        
        private static RestClient client = new RestClient("https://api.reg.ru/api/regru2/");


        public void Handle()
        {
            foreach (var list in _listUser)
            {
                var k = _getGetInfoResponse.HandleSingle(SendRequest(new GetInfoRequest(list.Username, list.Password)));
            }
        }

        private static List<UserRegruData> _listUser = new List<UserRegruData>()
        {
            new UserRegruData
            {
                Username="test",
                Password="test",
            }
        };

        private static string SendRequest(CommonRequest request)
        {
            var req = new RestRequest(request.EndPoint, (Method)request.Method);
            var res = client.Execute(req);
            return res.Content;
        }
    }
}
