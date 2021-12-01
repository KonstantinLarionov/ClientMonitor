using AutoMapper;
using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.CloudManager.Adaptors;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace ClientMonitor.Infrastructure.CloudManager
{
    public class CloudsFactory : ICloudFactory
    {
        private readonly Dictionary<CloudTypes, ICloud> _adaptors;
        public CloudsFactory(IMapper mapper, IOptions<CloudOptions> options)
        {
            _adaptors = new Dictionary<CloudTypes, ICloud>()
            {
                {CloudTypes.YandexCloud, new YandexAdaptor(options.Value, mapper) }
            };
        }
        public ICloud GetCloud(CloudTypes type) => _adaptors.FirstOrDefault(x => x.Key == type).Value;
    }
}
