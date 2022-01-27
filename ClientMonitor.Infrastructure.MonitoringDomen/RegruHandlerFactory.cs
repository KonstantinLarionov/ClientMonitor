using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Abstractions.Regru;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.MonitoringDomen.Adaptors;

using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientMonitor.Infrastructure.MonitoringDomen
{
    public sealed class RegruHandlerFactory : IRegruHandlerFactory
    {
        public bool CreateAdaptors(List<UserRegruData> infos, DomenTypes type)
        {
            try
            {
                infos.ForEach(info =>
                {
                    _adaptors.Add((type, new RegruAdaptor(info)));
                });
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<IRegru> GetAdaptors(DomenTypes type)
        {
            if (_adaptors.Any(x => x.Item1 == type))
            {
                return _adaptors.Where(x => x.Item1 == type).Select(x => x.Item2);
            }
            return null;
        }


        private List<(DomenTypes, IRegru)> _adaptors;

    }
}
