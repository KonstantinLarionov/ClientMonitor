using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Abstractions.Regru;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Application.Domanes.Request;
using ClientMonitor.Application.Domanes.Request.Regru;
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
        IRegruHandlerFactory _regruFactory;
        INotificationFactory _notificationFactory;
        private List<IRegru> _listDomen;

        public RegruHandler(IRegruHandlerFactory iRegruFactory, INotificationFactory notificationFactory)
        {
            _regruFactory = iRegruFactory;
            _notificationFactory = notificationFactory;
            _listDomen = new List<IRegru>();
        }


        public void Handle()
        {
            if (_regruFactory.CreateAdaptors(_listUser, DomenTypes.Regru))
            {
                _listDomen = _regruFactory
                    .GetAdaptors(DomenTypes.Regru)
                    .ToList();
            }
            var notifyer = _notificationFactory.GetNotification(NotificationTypes.Telegram);

            foreach (var list in _listUser)
            {
                list.
            }

            throw new NotImplementedException();
        }

        private static List<UserRegruData> _listUser = new List<UserRegruData>()
        {
            new UserRegruData
            {
                Username="test",
                Password="test",
            }
        };
    }
}
