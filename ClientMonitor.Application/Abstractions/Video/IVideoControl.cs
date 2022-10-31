using System;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Abstractions
{
    public interface IVideoControl
    {
        /// <summary>
        /// Начало записи видо файла по потоку из камеры
        /// </summary>
        Task StartMonitoring();

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler ConnectionErrorEvent;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler InfoAboutLog;
    }
}
