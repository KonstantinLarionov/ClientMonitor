using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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
        private readonly string Path = @"C:\Users\kosty\Desktop\PatternRepository.txt";

        /// <summary>
        /// Базовый адрес API
        /// </summary>
        private readonly string BaseUrl = "https://webdav.yandex.ru";

        /// <summary>
        /// Путь файла в облаке (Так класть в корень)
        /// </summary>
        private readonly string PathOnCloud = "PatternRepository.txt";

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
            // Заводим клиента
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            // Берем файл в поток
            StreamContent sr = new StreamContent(new FileStream(Path, FileMode.Open));

            // Заводим запрос
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, PathOnCloud);
            // передаем файл в запрос
            request.Content = sr;
            // Указываем заголовки запроса
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
            request.Headers.Add("Authorization", $"Basic {BasicAuth}");
            request.Headers.Add("Transfer-Encoding", TransferEncoding);

            // Отправляем
            var response = await client.SendAsync(request);

            // Проверяем результат
            // Тут не 200 код приходит а 201 что говорит об успешном создании файла в облаке 
            Assert.True((int)response.StatusCode == 201);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
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
