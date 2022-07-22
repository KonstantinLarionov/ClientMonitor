

namespace ClientMonitor.Application.Handler.JsonHandlers
{
    public interface IDataHandler<out T> : ISingleMessageHandler<T>
    {
    }
}
