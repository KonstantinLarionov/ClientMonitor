using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Domanes.Objects
{
    public class UploadedFilesInfo
    {
        public string Name { get; set; }
        public string Extension { get; set; } = "*.avi";
        public DateTime Create { get; set; }
        public string Path { get; set; } 
        public string FolderName { get; set; }
    }
}
