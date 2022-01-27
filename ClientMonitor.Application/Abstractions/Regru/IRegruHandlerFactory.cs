using ClientMonitor.Application.Abstractions.Regru;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Application.Domanes.Objects.Regru;

using System.Collections.Generic;

namespace ClientMonitor.Application.Abstractions
{
    /// <summary>
    /// Интерфейс фабрики
    /// </summary>
    public interface IRegruHandlerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="infos"></param>
        /// <param name="type"></param>
        public bool CreateAdaptors(List<UserRegruData> infos, DomenTypes type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<IRegru> GetAdaptors(DomenTypes type);
    }
}
