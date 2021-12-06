using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace ClientMonitor.Infrastructure.Test
{
    public class CloudYandexTest
    {
        /// <summary>
        /// Логин для авторизации в облаке
        /// </summary>
        private readonly string Login = "afc.studio@yandex.ru";

        /// <summary>
        /// Пароль для авторизации в облаке != основному паролю в аккаунте (настраивается тут: https://yandex.ru/support/id/authorization/app-passwords.html)
        /// </summary>
        private readonly string Password = "ayamnlgjgoqvlpit";

        /// <summary>
        /// Путь локального файла
        /// </summary>
        private readonly string Path = @"C:\Test\Test1\Озон-ПГ-Склад_2021_11_29_13_7_24 — копия (3).mp4";

        /// <summary>
        /// Базовый адрес API
        /// </summary>
        private readonly string BaseUrl = "https://webdav.yandex.ru";

        /// <summary>
        /// Путь файла в облаке (Так класть в корень)
        /// </summary>
        private readonly string PathOnCloud = "Озон-ПГ-Склад_2021_11_29_13_7_24 — копия (3).mp4";

        /// <summary>
        /// Заголовок запроса без указания размера файла
        /// </summary>
        private readonly string TransferEncoding = "chunked";

        /// <summary>
        /// Заголовок типа контента в теле запроса
        /// </summary>
        private readonly string ContentType = "application/binary";

        /// <summary>
        /// Создание строки для авторизации
        /// </summary>
        private string BasicAuth
        {
            get
            {
                return Base64Encode($"{Login}:{Password}");
            }
        }

        /// <summary>
        /// Тест на загрузку файла в облако
        /// </summary>
        [Fact]
        public async void UploadToCloudYandex()
        {
            #region [Первый запрос]

            // Заводим клиента
            HttpClient client = new HttpClient();

            //убрать Expect: 100-continue
            client.DefaultRequestHeaders.ExpectContinue = false;
            client.BaseAddress = new Uri(BaseUrl);
            // Берем файл в поток
            StreamContent sr = new StreamContent(new FileStream(Path, FileMode.Open));
            // Заводим запрос
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, PathOnCloud);
            request.Headers.Add("Authorization", $"Basic {BasicAuth}");
            request.Headers.Add("Transfer-Encoding", TransferEncoding);

            // передаем файл в запрос
            request.Content = sr;
            // Указываем заголовки запроса
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);


            //Отправляем
            var response = await client.SendAsync(request);





            //Task<HttpResponseMessage> response = client.SendAsync(request);
            Assert.True((int)response.StatusCode == 201);

            //var response = await client.SendAsync(request);
            // Проверяем результат
            // Тут не 200 код приходит а 201 что говорит об успешном создании файла в облаке 
            //Assert.True((int)response.Result.StatusCode == 201);

            //Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);

            #endregion

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
