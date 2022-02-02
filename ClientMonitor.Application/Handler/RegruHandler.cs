using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
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
    /// Бизнес-логика? регру
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
            string domains = "Список доменов:";
            string hosting = "Список хостингов:";
            var notifyer = _notificationFactory.GetNotification(NotificationTypes.Telegram);
            foreach (var list in _listUser)
            {
                var response = _getGetInfoResponse.HandleSingle(SendRequest(new GetInfoRequest(list.Username, list.Password,list.Servtype)));
                var dateTime = response.Answer.Services[0].ExpirationDate.ToShortDateString();
                if (list.Servtype== "domain")
                {
                    domains = domains+ "\n" + list.Username +" истечёт : "+ dateTime;
                }
                if (list.Servtype == "srv_hosting_plesk")
                {
                    hosting = hosting + "\n" + list.Username + " истечёт : " + dateTime;
                }
                TimeSpan dt = response.Answer.Services[0].ExpirationDate - DateTime.Now;
                if(dt.Days<=7)
                {
                    notifyer.SendMessage("-693501604", list.Username + " истечёт : " + dateTime);
                }
            }
            string message = domains + "\n" + hosting;
            
            notifyer.SendMessage("-693501604", message);


        }

        private static List<UserRegruData> _listUser = new List<UserRegruData>()
        {
            new UserRegruData
            {
                Name = "Памятники",
                Username="orenburgranit@yandex.ru",
                Password="F8keu!r2",
                Servtype="domain",
            },
            new UserRegruData
            {
                Name = "Памятники",
                Username="orenburgranit@yandex.ru",
                Password="F8keu!r2",
                Servtype="srv_hosting_plesk",
            },
            new UserRegruData
            {
                Name = "Ормек",
                Username="ormek-zm@mail.ru",
                Password="156156MD156156",
                Servtype="domain",
            },
            new UserRegruData
            {
                Name = "Ормек",
                Username="ormek-zm@mail.ru",
                Password="156156MD156156",
                Servtype="srv_hosting_plesk",
            },
            new UserRegruData
            {
                Name = "Korall",
                Username="r.lesnoff@yandex.ru",
                Password="V28eNnJ_",
                Servtype="domain",
            },
            new UserRegruData
            {
                Name = "Korall",
                Username="r.lesnoff@yandex.ru",
                Password="V28eNnJ_",
                Servtype="srv_hosting_plesk",
            },
            new UserRegruData
            {
                Name = "AFC",
                Username="s9033655614@gmail.com",
                Password="214614ntk",
                Servtype="domain",
            },
            new UserRegruData
            {
                Name = "AFC",
                Username="s9033655614@gmail.com",
                Password="214614ntk",
                Servtype="srv_hosting_plesk",
            },
        };

        private static string SendRequest(CommonRequest request)
        {
            var req = new RestRequest(request.EndPoint, (Method)request.Method);
            var res = client.Execute(req);
            return res.Content;
        }
    }
}
