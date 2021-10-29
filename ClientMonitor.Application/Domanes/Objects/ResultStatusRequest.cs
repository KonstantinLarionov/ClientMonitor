using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Domanes.Objects
{
    public class ResultStatusRequest
    {
        public ResultStatusRequest(string message, DateTime dateTime, bool success)
        {
            Message = message;
            DateTime = dateTime;
            Success = success;
        }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public bool Success { get; set; }
    }
}
