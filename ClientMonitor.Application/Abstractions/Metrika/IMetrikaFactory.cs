using ClientMonitor.Application.Domanes.Response.Metrika;
using ClientMonitor.Application.Handler.JsonHandlers;

using JetBrains.Annotations;

namespace ClientMonitor.Application.Abstractions.Metrika
{
    /// <summary>
    /// Интерфейс респонса яндекс метрики
    /// </summary>
    public interface IMetrikaFactory
    {
        [NotNull]
        ISingleMessageHandler<GetDataByTimeResponse> CreateGetDataByTimeResponse();
    }
}
