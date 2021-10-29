using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Infrastructure.ScreenRecording.Adaptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.ScreenRecording
{
    public class ScreenRecordingFactory : IScreenRecordingFactory
    {
        private readonly Dictionary<ScreenRecordingTypes, IScreenRecording> _adaptors;

        public ScreenRecordingFactory()
        {
            _adaptors = new Dictionary<ScreenRecordingTypes, IScreenRecording>()
            {
                {ScreenRecordingTypes.ScreenRecording, new ScreenRecordingAdaptor("")}
            };
        }
        public IScreenRecording GetScreenRecording(ScreenRecordingTypes type) => _adaptors.FirstOrDefault(x => x.Key == type).Value;
    }
}
