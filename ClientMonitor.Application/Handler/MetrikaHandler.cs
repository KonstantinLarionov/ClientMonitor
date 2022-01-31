using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Abstractions.Metrika;
using ClientMonitor.Application.Domanes.Request;
using ClientMonitor.Application.Handler.JsonHandlers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Handler
{
    public class MetrikaHandler : IMetrikaHandler
    {
        //private readonly ISingleMessageHandler<GetInfoResponse> _getGetInfoResponse;

        IMetrikaFactory _regruFactory;
        INotificationFactory _notificationFactory;

        public MetrikaHandler(IMetrikaFactory metrikaFactory, INotificationFactory notificationFactory)
        {
            //_getGetInfoResponse = metrikaFactory.CreateGetInfoResponse();
            _regruFactory = metrikaFactory;
            _notificationFactory = notificationFactory;
        }

        //public GetInfoResponse HandleGetInfoResponse(string message) => _getGetInfoResponse.HandleSingle(message);

        private static RestClient client = new RestClient("https://rest-api.display.yandex.net/rest/v0.2/counters");


        public void Handle()
        {
            var req = new RestRequest(".json?line_id=9F53A0C7-A68C-461A-8D41-87112288DBBB&include=goals", Method.GET);
            req.AddHeader("Authorization", "OAuth AQAAAAA0xXEYAAdv3jbmZQ52CEQyv4Hw3ibzF_o");
            var res = client.Execute(req);
            var fg = res;

            //foreach (var list in _listUser)
            //{
            //    var k = _getGetInfoResponse.HandleSingle(SendRequest(new GetInfoRequest(list.Username, list.Password, list.Servtype)));
            //    var l = 10;
            //}
        }
        /*
        private static List<UserRegruData> _listUser = new List<UserRegruData>()
        {
            new UserRegruData
            {
                Username="orenburgranit@yandex.ru",
                Password="F8keu!r2",
                Servtype="domain",
            },
            new UserRegruData
            {
                Username="orenburgranit@yandex.ru",
                Password="F8keu!r2",
                Servtype="srv_hosting_plesk",
            },
            new UserRegruData
            {
                Username="ormek-zm@mail.ru",
                Password="156156MD156156",
                Servtype="domain",
            },
            new UserRegruData
            {
                Username="ormek-zm@mail.ru",
                Password="156156MD156156",
                Servtype="srv_hosting_plesk",
            },
            new UserRegruData
            {
                Username="r.lesnoff@yandex.ru",
                Password="V28eNnJ_",
                Servtype="domain",
            },
            new UserRegruData
            {
                Username="r.lesnoff@yandex.ru",
                Password="V28eNnJ_",
                Servtype="srv_hosting_plesk",
            },
            new UserRegruData
            {
                Username="s9033655614@gmail.com",
                Password="214614ntk",
                Servtype="domain",
            },
            new UserRegruData
            {
                Username="s9033655614@gmail.com",
                Password="214614ntk",
                Servtype="srv_hosting_plesk",
            },
        };*/

        private static string SendRequest(CommonRequest request)
        {
            var req = new RestRequest(request.EndPoint, (Method)request.Method);
            var res = client.Execute(req);
            return res.Content;
        }
    }
}
