using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Infrastructure.VideoControl.Adaptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.VideoControl
{
    public class VideoControlFactory : IVideoControlFactory
    {
        private readonly Dictionary<VideoMonitoringTypes, IVideoControl> _adaptors;

        public VideoControlFactory()
        {
            _adaptors = new Dictionary<VideoMonitoringTypes, IVideoControl>()
            {
                {VideoMonitoringTypes.TestCam, new CamTestAdaptor() },
            };
        }
        public IVideoControl GetVideoMonitoring(VideoMonitoringTypes type) => _adaptors.FirstOrDefault(x => x.Key == type).Value;
    }
}
