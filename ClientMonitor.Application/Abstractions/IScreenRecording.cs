﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Abstractions
{
    public interface IScreenRecording
    {
        bool StartScreenRecording();

        bool StopScreenRecording();
    }
}
