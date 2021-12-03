using ClientMonitor.Application.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;


namespace ClientMonitor.BckgrndWorker
{
    public class VideoControlBackgroundWorker
    {
        readonly IApplicationBuilder _application;
        readonly Action<IVideoControlHandler> _handle;

        public VideoControlBackgroundWorker(IApplicationBuilder application, Action<IVideoControlHandler> handle)
        {
            _application = application;
            _handle = handle;
        }

        /// <summary>
        /// Старт
        /// </summary>
        public void Start()
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
        }

        /// <summary>
        /// Запуск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var service = _application.ApplicationServices.GetRequiredService<IVideoControlHandler>();
            _handle.Invoke(service);
        }

        /// <summary>
        /// Если остановится/завершится
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
