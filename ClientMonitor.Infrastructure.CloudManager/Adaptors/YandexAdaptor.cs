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

        public YandexAdaptor(CloudOptions cloudOptions)
        {
            CloudOptions = cloudOptions;
        }

        public async Task<List<CloudFilesInfo>> GetFilesAndFoldersAsync()
        {
            try
            {

            var conect = new DiskHttpApi(CloudOptions.Token);

            var rootFolderData = await conect.MetaInfo.GetInfoAsync(new ResourceRequest { Path = CloudOptions.Path });

            List<CloudFilesInfo> filesAndFoldersList = new List<CloudFilesInfo>();

            CloudFilesInfo filesAndFolders = new CloudFilesInfo();

            foreach (var item in rootFolderData.Embedded.Items)
            {      
                
                filesAndFolders.Name = item.Name;
                filesAndFolders.MimeType = item.MimeType;
                filesAndFolders.FilesType = (Application.Domanes.Enums.FilesType)item.Type;
                filesAndFolders.Created = item.Created;
                filesAndFolders.Size = item.Size;
                filesAndFolders.PublicUrl = item.PublicUrl;
                filesAndFolders.Path = item.Path;
                filesAndFoldersList.Add(filesAndFolders);
            }

            return filesAndFoldersList;

            }
            catch(Exception ex) { return null; }
        }

        public async Task UploadFiles(UploadedFilesInfo uploadedFilesInfo)
        {
            try
            {

                var files = Directory.GetFiles(uploadedFilesInfo.Path, uploadedFilesInfo.Extension);

                var conect = new DiskHttpApi(CloudOptions.Token);

                foreach (var file in files)
                {
                    var link = await conect.Files.GetUploadLinkAsync(uploadedFilesInfo.Path + file, overwrite: false);
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

        public async Task<Task<Link>> DawnloadFiles()
        {
            try 
            { 
            var destDir = Path.Combine(Environment.CurrentDirectory, "Download");

            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            var conect = new DiskHttpApi(CloudOptions.Token);

            var rootFolderData = await conect.MetaInfo.GetInfoAsync(new ResourceRequest { Path = CloudOptions.Path });

            Task<Link> link;

            foreach (var item in rootFolderData.Embedded.Items)
            {
                await conect.Files.DownloadFileAsync(path: item.Path, Path.Combine(destDir, item.Name));
                link = conect.Files.GetDownloadLinkAsync(item.Path);
                return link;
            }
                return null;
            }
            catch(Exception ex) { return null; }
        }
    }
}
