using ClientMonitor.Application.Domanes.Objects;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.CloudManager.Adaptors
{
    [Headers("Authorization: Basic AQAAAAA0xXEYAAdv3jbmZQ52CEQyv4Hw3ibzF_o")]
    public interface CloudManagerClient
    {

        Task<List<CloudFilesInfo>> GetFilesAndFoldersAsync();

        Task UploadFiles(UploadedFilesInfo uploadedFilesInfo);

        Task<bool> DawnloadFiles(string path, string name, string downloadpath);
    }
}
