using AutoMapper;
using ClientMonitor.Application.Abstractions;

using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.CloudManager.Adaptors;
using Microsoft.Extensions.Configuration;
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
            var options = configuration.GetSection("") as CloudOptions;
            _adaptors = new Dictionary<CloudTypes, ICloud>()
            {
                {CloudTypes.YandexCloud, new YandexAdaptor(cloudOptions) }
            };
        }
        public ICloud GetCloud(CloudTypes type) => _adaptors.FirstOrDefault(x => x.Key == type).Value;
    }
}
