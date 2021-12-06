using AutoMapper;
using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
        private readonly string ContentType = "application/binary";

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

        /// <summary>
        /// Создание строки для авторизации
        /// </summary>
        private string BasicAuth
        {
            get
            {
                return Base64Encode($"{_сloudOptions.Login}:{_сloudOptions.Password}");
            }
        }

        /// <summary>
        /// Хелперс для кодинга строки
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

    }
}
