using ClientMonitor.Application.Domanes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Application.Domanes.Objects
{
    public class CloudFilesInfo
    {
        public string Name { get; set; }
        public string MimeType { get; set; }
        public FilesTypes Type { get; set; }
        public DateTime Created { get; set; }
        public long Size { get; set; }
        public string PublicUrl { get; set; }
        public string Path { get; set; }
    }
}
