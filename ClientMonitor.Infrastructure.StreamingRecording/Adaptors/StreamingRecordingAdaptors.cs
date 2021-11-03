using ClientMonitor.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Onvif.Core.Client;
using Onvif.Core.Client.Common;

namespace ClientMonitor.Infrastructure.StreamingRecording.Adaptors
{
    public class StreamingRecordingAdaptor : IStreamingRecording
    {
        public StreamingRecordingAdaptor()
        {

        }

        public bool StartStreamingRecording()
        {
            ConnectToCamera();
            return true;
        }

        public bool StopStreamingRecording()
        {
            throw new NotImplementedException();
        }

        private async void ConnectToCamera()
        {
            try
            {
                var account = new Account("192.168.89.28", "TestCam", "TestCam");
                var camera = Camera.Create(account, ex => 
                { 

                });
                if (camera != null)
                {
                    //move...
                    var vector1 = new PTZVector { PanTilt = new Vector2D { x = 0.5f } };
                    var speed1 = new PTZSpeed { PanTilt = new Vector2D { x = 1f, y = 1f } };
                    await camera.MoveAsync(MoveType.Absolute, vector1, speed1, 0);
                }
                    //_camera = IPCameraFactory.GetCamera("92.255.240.7:8080", "Goldencat", "Lollipop321123", true);


                }
            catch(Exception ex)
            {
                var mass = ex.Message;
            }
        }
    }
}
