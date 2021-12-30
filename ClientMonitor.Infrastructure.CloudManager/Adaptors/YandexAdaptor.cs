using AutoMapper;
using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;


namespace ClientMonitor.Infrastructure.CloudManager.Adaptors
{
    public class YandexAdaptor : ICloud
    {
        readonly CloudOptions CloudOptions;
        readonly IMapper _мapper;

        public YandexAdaptor(CloudOptions cloudOptions, IMapper mapper)
        {
            CloudOptions = cloudOptions;
            _мapper = mapper;
        }

        public async Task<List<CloudFilesInfo>> GetFilesAndFoldersAsync()
        {

            var conect = new DiskHttpApi(CloudOptions.Token);
            var rootFolderData = await conect.MetaInfo.GetInfoAsync(new ResourceRequest { Path = CloudOptions.Path });
            List<CloudFilesInfo> filesAndFoldersList = new List<CloudFilesInfo>();
            var items = rootFolderData.Embedded.Items;
            foreach (var item in items)
            {
                filesAndFoldersList.Add(new CloudFilesInfo()
                {
                    Name = item.Name,
                    MimeType = item.MimeType,
                    Type = (Application.Domanes.Enums.FilesTypes)item.Type,
                    Created = item.Created,
                    Size = item.Size,
                    PublicUrl = item.PublicUrl,
                    Path = item.Path
                });
            }
            return filesAndFoldersList;
        }

        readonly string _token = "AQAAAAA0xXEYAAdv3jbmZQ52CEQyv4Hw3ibzF_o";

        /// <summary>
        /// Загрузка файла
        /// </summary>
        /// <param name="uploadedFilesInfo"></param>
        /// <returns></returns>
        public async Task UploadFiles(UploadedFilesInfo uploadedFilesInfo)
        {
            //var rootFolderData = await GetFilesAndFoldersAsync();

            var conect = new DiskHttpApi(_token);
            var link = await conect.Files.GetUploadLinkAsync(CloudOptions.Path + uploadedFilesInfo.FolderName + "/" + uploadedFilesInfo.Name, overwrite: false);
            using (var fs = File.OpenRead(uploadedFilesInfo.Path + "/" + uploadedFilesInfo.Name))
            {
                await conect.Files.UploadAsync(link, fs);
            }
        }

        /// <summary>
        /// Скачивание файла
        /// </summary>
        /// <param name="cloudpath">Путь в облаке</param>
        /// <param name="name">Название</param>
        /// <param name="downloadpath">Путь выгрузки</param>
        /// <returns></returns>
        public async Task<bool> DawnloadFiles(string cloudpath, string name, string downloadpath)
        {
            var conect = new DiskHttpApi(CloudOptions.Token);
            await conect.Files.DownloadFileAsync(path: Path.Combine(cloudpath, name), Path.Combine(downloadpath, name));
            return true;
        }
    }
}
