

namespace ClientMonitor.Application.Handler.JsonHandlers
{
    public interface ISingleMessageHandler<out T>
    {
        T HandleSingle(string message);
    }
}
