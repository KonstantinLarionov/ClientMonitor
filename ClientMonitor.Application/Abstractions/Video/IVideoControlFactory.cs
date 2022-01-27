using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Abstractions
{
    public interface IVideoControlFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="infos"></param>
        /// <param name="type"></param>
        public bool CreateAdaptors(List<ControlVideoInfo> infos, VideoMonitoringTypes type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<IVideoControl> GetAdaptors(VideoMonitoringTypes type);
    }
}
