using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;


namespace ClientMonitor.Application.Handler
{
    public class VideoControlHandler : IVideoControlHandler
    {
        IVideoControlFactory videoControlFactory;
        IRepository<LogInfo> dbLog;

        public VideoControlHandler(IVideoControlFactory videoFactory, IRepository<LogInfo> repositoryLog)
        {
            videoControlFactory = videoFactory;
            dbLog = repositoryLog;
        }

        public void HandleTestCam()
        {
            List<ResultVideoControl> results = new List<ResultVideoControl>();
            var monitor = videoControlFactory.GetVideoMonitoring(VideoMonitoringTypes.TestCam);
            var resultMonitoring = monitor.StartMonitoring() as List<ResultVideoControl>;
            results.AddRange(resultMonitoring);
            string test1 = "";

            foreach (var result in results)
            {
                if (!result.Success)
                {
                    test1 = "!Ошибка проверки!\r\n" + result.DateTime + "\r\n";
                    AddInLog(test1);
                }
                else
                {
                    test1 = "!Проверка успешна!\r\n" + result.DateTime + "\r\n";
                    AddInLog(test1);
                }
            }
        }

        private void AddInLog(string k)
        {
            LogInfo log = new LogInfo
            {
                TypeLog = LogTypes.Information,
                Text = k,
                DateTime = DateTime.Now
            };
            dbLog.AddInDb(log);
        }
    }
}
