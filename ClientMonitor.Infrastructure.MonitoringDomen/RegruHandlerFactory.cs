using ClientMonitor.Application.Abstractions;

using ClientMonitor.Application.Domanes.Response.Regru;
using ClientMonitor.Application.Handler.JsonHandlers;

using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientMonitor.Infrastructure.MonitoringDomen
{
    public sealed class RegruHandlerFactory : IRegruHandlerFactory
    {
        public ISingleMessageHandler<GetInfoResponse> CreateGetInfoResponse() => new BaseJsonHandler<GetInfoResponse>();
    }
}
