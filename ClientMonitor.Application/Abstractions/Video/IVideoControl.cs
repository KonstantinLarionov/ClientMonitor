﻿using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Abstractions
{
    public interface IVideoControl
    {
        /// <summary>
        /// Начало записи видо файла по потоку из камеры
        /// </summary>
        void StartMonitoring();

        /// <summary>
        /// 
        /// </summary>
        void StopMonitoring();

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
