using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Abstractions.Metrika;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects.YandexMetrika;
using ClientMonitor.Application.Domanes.Request;
using ClientMonitor.Application.Domanes.Request.Metrika;
using ClientMonitor.Application.Domanes.Response.Metrika;
using ClientMonitor.Application.Handler.JsonHandlers;

using RestSharp;

using System;
using System.Collections.Generic;

namespace ClientMonitor.Application.Handler
{
    /// <summary>
    /// бизнес логика яндекс метрика
    /// </summary>
    public class MetrikaHandler : IMetrikaHandler
    {
        private readonly ISingleMessageHandler<GetDataByTimeResponse> _getDataByTimeResponse;

        IMetrikaFactory _regruFactory;
        INotificationFactory _notificationFactory;

        public MetrikaHandler(IMetrikaFactory metrikaFactory, INotificationFactory notificationFactory)
        {
            _getDataByTimeResponse = metrikaFactory.CreateGetDataByTimeResponse();
            _regruFactory = metrikaFactory;
            _notificationFactory = notificationFactory;
        }

        //public GetInfoResponse HandleGetInfoResponse(string message) => _getGetInfoResponse.HandleSingle(message);

        private static RestClient client = new RestClient("https://api-metrika.yandex.net/stat/v1");


        public void Handle()
        {
            string message = "Просмотры на "+DateTime.Now.ToShortDateString();
            foreach (var list in _listUser)
            {
                var request = _getDataByTimeResponse.HandleSingle(SendRequest(new GetDataByTimeRequest(list.Ids, list.Date1, list.Date2)));
                message = message+"\n"+ list.Name+" : "+request.Totals[0];
            }
            var notifyer = _notificationFactory.GetNotification(NotificationTypes.Telegram);
            notifyer.SendMessage("-693501604", message);
        }

        private static List<InfoCounterData> _listUser = new List<InfoCounterData>()
        {
            new InfoCounterData
            {
                Name="АФК: afcstudio.ru",
                Ids=62766436,
                Date1=DateTime.Now,
                Date2=DateTime.Now,
            },
            new InfoCounterData
            {
                Name="Давыдов: companyvd.ru",
                Ids=62766370,
                Date1=DateTime.Now,
                Date2=DateTime.Now,
            },
            new InfoCounterData
            {
                Name="Кот: goldencat.su",
                Ids=62766403,
                Date1=DateTime.Now,
                Date2=DateTime.Now,
            },
            new InfoCounterData
            {
                Name="Ормек: ormek.ru",
                Ids=62766265,
                Date1=DateTime.Now,
                Date2=DateTime.Now,
            },
            new InfoCounterData
            {
                Name="Памятники: pamiatnikigm.ru",
                Ids=62766295,
                Date1=DateTime.Now,
                Date2=DateTime.Now,
            }
        };

        private static string SendRequest(CommonRequest request)
        {
            var req = new RestRequest(request.EndPoint, (Method)request.Method);
            req.AddHeader("Authorization", $"OAuth AQAAAAA0xXEYAAelIhkKSSQZLknvuXOcS13olhE");
            var res = client.Execute(req);
            return res.Content;
        }
    }
}
