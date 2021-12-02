﻿using AutoMapper;
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
            //#region [Запрос на облако]
            // Заводим клиента
            HttpClient client = new HttpClient();
            //Expect: 100-continue
            client.DefaultRequestHeaders.ExpectContinue = false;
            client.BaseAddress = new Uri(_сloudOptions.BaseAddress);
            // Берем файл в поток
            //StreamContent sr = new StreamContent(new FileStream(uploadedFilesInfo.Path+@"\\"+ uploadedFilesInfo.Name, FileMode.Open));
            StreamContent sr = new StreamContent(new FileStream(@"C:\Test\Test1\Озон-ПГ-Склад_2021_11_29_13_7_24 — копия (3).mp4", FileMode.Open));
            // Заводим запрос
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "Озон-ПГ-Склад_2021_11_29_13_7_24 — копия (3).mp4");
            // передаем файл в запрос
            request.Content = sr;
            // Указываем заголовки запроса
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/binary");
            request.Headers.Add("Authorization", $"Basic {BasicAuth}");
            request.Headers.Add("Transfer-Encoding", "chunked");

            // Отправляем
            try
            {
                var response = await client.SendAsync(request);
                Console.WriteLine(response);
            }
            catch (Exception e) 
            {
                //Console.WriteLine(e.Message);
            }
            //if ((int)response.StatusCode == 201)
            //{

            //    //чето написать, можно в логи добавить
            //}

            //#endregion

            //var rootFolderData = await GetFilesAndFoldersAsync();
            //var conect = new DiskHttpApi(_сloudOptions.Token);
            //var link = await conect.Files.GetUploadLinkAsync(_сloudOptions.Path + uploadedFilesInfo.FolderName + "/" + uploadedFilesInfo.Name, overwrite: false);

            //using (var fs = File.OpenRead(uploadedFilesInfo.Path + "/" + uploadedFilesInfo.Name))
            //{
            //    await conect.Files.UploadAsync(link, fs);
            //}
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
