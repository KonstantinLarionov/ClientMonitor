using ClientMonitor.Application.Abstractions.Metrika;
using ClientMonitor.Application.Domanes.Response.Metrika;
using ClientMonitor.Application.Handler.JsonHandlers;


namespace ClientMonitor.Infrastructure.Metrika
{
    public sealed class MetrikaFactory : IMetrikaFactory
    {
        public ISingleMessageHandler<GetDataByTimeResponse> CreateGetDataByTimeResponse() => new BaseJsonHandler<GetDataByTimeResponse>();
    }
}
