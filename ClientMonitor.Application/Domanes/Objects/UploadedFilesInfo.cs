using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Domanes.Objects
{
    public class UploadedFilesInfo
    {
        public string Name { get; set; } = "__VSDeploymentFailure__.txt";
        public string Extension { get; set; } = "*.txt";
        public DateTime Create { get; set; }
        public string Path { get; set; } = @"C:\Users\79123\Documents";
        public string FolderName { get; set; } = "Тест";
    }
}
