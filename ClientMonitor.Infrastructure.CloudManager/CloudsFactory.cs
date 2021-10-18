using AutoMapper;
using ClientMonitor.Application.Abstractions;

using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.CloudManager.Adaptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.CloudManager
{
    public class CloudsFactory : ICloudFactory
    {
        private readonly Dictionary<CloudTypes, ICloud> _adaptors;

        public CloudsFactory(IMapper mapper)
        {
            _adaptors = new Dictionary<CloudTypes, ICloud>()
            {
                {CloudTypes.YandexCloud, new YandexAdaptor(new CloudOptions(), mapper) }
            };
        }
        public ICloud GetCloud(CloudTypes type) => _adaptors.FirstOrDefault(x => x.Key == type).Value;

        public ICloud GetFilesAndFoldersAsync(List<CloudFilesInfo> type)
        {
            throw new NotImplementedException();
        }
    }
}
