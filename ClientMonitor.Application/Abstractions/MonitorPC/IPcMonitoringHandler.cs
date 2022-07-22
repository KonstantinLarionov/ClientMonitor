using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Abstractions
{
    public interface IPcMonitoringHandler
    {
        void HandleCpu();
        void HandleRam();
        void HandleProc();
        void HandleHttp();
        void HandleMessageMonitoringPc();
        //void HandleSettings();
    }
}
