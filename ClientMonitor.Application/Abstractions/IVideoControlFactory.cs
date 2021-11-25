using ClientMonitor.Application.Domanes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Abstractions
{
    public interface IVideoControlFactory
    {
        IVideoControl GetVideoMonitoring(VideoMonitoringTypes type);
    }
}
