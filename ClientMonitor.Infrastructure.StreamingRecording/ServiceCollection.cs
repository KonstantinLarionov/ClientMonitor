using ClientMonitor.Application.Abstractions;
using ClientMonitor.Infrastructure.StreamingRecording.Adaptors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.StreamingRecording
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureStreamingRecording(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollection).GetTypeInfo().Assembly;

            services.AddSingleton<IStreamingRecording, StreamingRecordingAdaptor>();
        }
    }
}
