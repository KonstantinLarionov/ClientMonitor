using System;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;

namespace ClientMonitor.Application.Abstractions
{
    public interface IMonitor
    {
        object ReceiveInfoMonitor();
    }
}
