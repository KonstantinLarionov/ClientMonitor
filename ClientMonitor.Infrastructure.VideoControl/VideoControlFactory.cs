using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
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
        private List<(VideoMonitoringTypes, IVideoControl)> _adaptors;

        public VideoControlFactory()
        {
            _adaptors = new List<(VideoMonitoringTypes, IVideoControl)>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="infos"></param>
        /// <param name="type"></param>
        public bool CreateAdaptors(List<ControlVideoInfo> infos, VideoMonitoringTypes type)
        {
            try
            {
                infos.ForEach(info =>
                {
                    _adaptors.Add((type, new IpCamAdaptor(info)));
                });
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<IVideoControl> GetAdaptors(VideoMonitoringTypes type)
        {
            if (_adaptors.Any(x => x.Item1 == type))
            {
                return _adaptors.Where(x => x.Item1 == type).Select(x=>x.Item2);
            }
            return null;
        }
    }
}
