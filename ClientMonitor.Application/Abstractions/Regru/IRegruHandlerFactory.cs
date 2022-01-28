using ClientMonitor.Application.Domanes.Response.Regru;
using ClientMonitor.Application.Handler.JsonHandlers;
using JetBrains.Annotations;

namespace ClientMonitor.Application.Abstractions
{
    /// <summary>
    /// Интерфейс фабрики
    /// </summary>
    public interface IRegruHandlerFactory
    {
        [NotNull]
        ISingleMessageHandler<GetInfoResponse> CreateGetInfoResponse();
    }
}
