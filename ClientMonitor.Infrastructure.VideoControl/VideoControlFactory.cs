using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.VideoControl.Adaptors;
using System;
using System.Collections.Generic;
using System.Linq;


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
        /// Создание адапторов для списка параметры камеры по каждому типу камер
        /// </summary>
        /// <param name="infos">Список параметров камер (название, ссылка на трансляцию)</param>
        /// <param name="type">Тип камеры (Ip,WEB etc)</param>
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
        /// Выбор адаптора в соответсвии с типом
        /// </summary>
        /// <param name="type">Тип камеры(IP, WEB etc)</param>
        /// <returns></returns>
        public IEnumerable<IVideoControl> GetAdaptors(VideoMonitoringTypes type)
        {
            if (_adaptors.Any(x => x.Item1 == type))
            {
                return _adaptors.Where(x => x.Item1 == type).Select(x => x.Item2);
            }
            return null;
        }
    }
}
