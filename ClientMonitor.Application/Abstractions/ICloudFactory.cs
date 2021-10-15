using ClientMonitor.Application.Domanes;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Abstractions
{
    public interface ICloudFactory
    {
        ICloud GetCloud(CloudTypes type);
    }
}
