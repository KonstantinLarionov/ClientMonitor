using AutoMapper;
using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;


namespace ClientMonitor.Infrastructure.CloudManager.Adaptors
{
    public class YandexAdaptor : ICloud
    {
        readonly CloudOptions _сloudOptions;
        readonly IMapper _мapper;

        public YandexAdaptor(CloudOptions cloudOptions, IMapper mapper)
        {
            _сloudOptions = cloudOptions;
            _мapper = mapper;
        }

        public async Task<List<CloudFilesInfo>> GetFilesAndFoldersAsync()
        {
            var conect = new DiskHttpApi(_сloudOptions.Token);
            var rootFolderData = await conect.MetaInfo.GetInfoAsync(new ResourceRequest { Path = _сloudOptions.Path });
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

        /// <summary>
        /// Загрузка файла
        /// </summary>
        /// <param name="uploadedFilesInfo"></param>
        /// <returns></returns>
        public async Task UploadFiles(UploadedFilesInfo uploadedFilesInfo)
        {
            //HttpClient client = new HttpClient();
            //HttpRequestMessage request = new HttpRequestMessage();
            //request.RequestUri = new Uri("https://webdav.yandex.ru");
            //request.Method = HttpMethod.Put;

            //StreamReader sr = new StreamReader("test.txt", System.Text.Encoding.Default);
            //FileStream sr = new FileStream("test.txt", FileMode.Create, System.IO.FileAccess.Write);
            //StreamContent sr = new StreamContent(new FileStream("test.txt", FileMode.Open));

            //StreamContent sr = new StreamContent(new FileStream("test.txt", FileMode.Open));
            //request.Content = sr;
            //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/binary");
            //request.Headers.Add("Authorization", "Basic AQAAAAA0xXEYAAdv3jbmZQ52CEQyv4Hw3ibzF_o");
            //var response = await client.SendAsync(request);
            //Console.WriteLine(response);

            var rootFolderData = await GetFilesAndFoldersAsync();
            var conect = new DiskHttpApi(_сloudOptions.Token);
            var link = await conect.Files.GetUploadLinkAsync(_сloudOptions.Path + uploadedFilesInfo.FolderName + "/" + uploadedFilesInfo.Name, overwrite: false);

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
            var conect = new DiskHttpApi(_сloudOptions.Token);
            await conect.Files.DownloadFileAsync(path: Path.Combine(cloudpath, name), Path.Combine(downloadpath, name));
            return true;
        }
    }
}
