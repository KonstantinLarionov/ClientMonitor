using AutoMapper;
using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace ClientMonitor.Infrastructure.CloudManager.Adaptors
{
    public class YandexAdaptor : ICloud
    {
        readonly CloudOptions CloudOptions;
        readonly IMapper Mapper;

        public YandexAdaptor(CloudOptions cloudOptions, IMapper mapper)
        {
            CloudOptions = cloudOptions;
            Mapper = mapper;
        }

        public async Task<List<CloudFilesInfo>> GetFilesAndFoldersAsync()
        {
            try
            {
                var conect = new DiskHttpApi(CloudOptions.Token);

                var rootFolderData = await conect.MetaInfo.GetInfoAsync(new ResourceRequest { Path = CloudOptions.Path });

                List<CloudFilesInfo> filesAndFoldersList = new List<CloudFilesInfo>();

                foreach (var item in rootFolderData.Embedded.Items)
                {
                    //filesAndFoldersList.Add(Mapper.Map<Resource,CloudFilesInfo>(item));

                    filesAndFoldersList.Add(new CloudFilesInfo() { 
                        Name = item.Name, 
                        MimeType =item.MimeType, 
                        Type = (Application.Domanes.Enums.FilesTypes)item.Type, 
                        Created = item.Created, Size = item.Size, 
                        PublicUrl = item.PublicUrl, 
                        Path = item.Path 
                    });
                }
                return filesAndFoldersList;
            }
            catch (Exception ex)
            {
                return null; 
            }
        }

        public async Task UploadFiles(UploadedFilesInfo uploadedFilesInfo)
        {
            try
            {
                var rootFolderData = await GetFilesAndFoldersAsync();

                var files = Directory.GetFiles(uploadedFilesInfo.Path, uploadedFilesInfo.Extension);

                var conect = new DiskHttpApi(CloudOptions.Token);

                if(!rootFolderData.Any(_=>_.Type == Application.Domanes.Enums.FilesTypes.Dir && _.Name == uploadedFilesInfo.FolderName))
                {
                    await conect.Commands.CreateDictionaryAsync("/" + uploadedFilesInfo.FolderName);
                }

                foreach (var file in files)
                {
                    var link = await conect.Files.GetUploadLinkAsync(CloudOptions.Path + uploadedFilesInfo.FolderName + "/" + uploadedFilesInfo.Name, overwrite: false);
                    using (var fs = File.OpenRead(file))
                    {
                        await conect.Files.UploadAsync(link, fs);
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        public async Task<bool> DawnloadFiles(string cloudpath, string name, string downloadpath)
        {
            try 
            {
                var conect = new DiskHttpApi(CloudOptions.Token);

                //var rootFolderData = await GetFilesAndFoldersAsync();

                //foreach (var item in rootFolderData)
                //{
                //    if (item.Type == Application.Domanes.Enums.FilesType.Dir) { }

                //}

                await conect.Files.DownloadFileAsync(path: Path.Combine(cloudpath, name), Path.Combine(downloadpath, name));

                return true;
            }
            catch(Exception ex) { return false; }
        }

        public async Task<List<CloudFilesInfo>> GetFilesIntoFolders(string path)
        {

            var conect = new DiskHttpApi(CloudOptions.Token);

            var rootFolderData = await conect.MetaInfo.GetInfoAsync(new ResourceRequest { Path = path });

            List<CloudFilesInfo> filesAndFoldersList = new List<CloudFilesInfo>();

            foreach (var item in rootFolderData.Embedded.Items)
            {
                //filesAndFoldersList.Add(Mapper.Map<Resource,CloudFilesInfo>(item));

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
    }
}
