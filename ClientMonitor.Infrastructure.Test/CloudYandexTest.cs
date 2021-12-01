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
        /// ����� ��� ����������� � ������
        /// </summary>
        private readonly string Login = "afc.studio@yandex.ru";

        /// <summary>
        /// ������ ��� ����������� � ������ != ��������� ������ � �������� (������������� ���: https://yandex.ru/support/id/authorization/app-passwords.html)
        /// </summary>
        private readonly string Password = "ayamnlgjgoqvlpit";

        /// <summary>
        /// ���� ���������� �����
        /// </summary>
        private readonly string Path = @"C:\Users\kosty\Desktop\PatternRepository.txt";

        /// <summary>
        /// ������� ����� API
        /// </summary>
        private readonly string BaseUrl = "https://webdav.yandex.ru";

        /// <summary>
        /// ���� ����� � ������ (��� ������ � ������)
        /// </summary>
        private readonly string PathOnCloud = "PatternRepository.txt";

        /// <summary>
        /// ��������� ������� ��� �������� ������� �����
        /// </summary>
        private readonly string TransferEncoding = "chunked";

        /// <summary>
        /// ��������� ���� �������� � ���� �������
        /// </summary>
        private readonly string ContentType = "application/binary";

        /// <summary>
        /// �������� ������ ��� �����������
        /// </summary>
        private string BasicAuth
        {
            get
            {
                return Base64Encode($"{Login}:{Password}");
            }
        }

        /// <summary>
        /// ���� �� �������� ����� � ������
        /// </summary>
        [Fact]
        public async void UploadToCloudYandex()
        {
            // ������� �������
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            // ����� ���� � �����
            StreamContent sr = new StreamContent(new FileStream(Path, FileMode.Open));

            // ������� ������
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, PathOnCloud);
            // �������� ���� � ������
            request.Content = sr;
            // ��������� ��������� �������
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
            request.Headers.Add("Authorization", $"Basic {BasicAuth}");
            request.Headers.Add("Transfer-Encoding", TransferEncoding);

            // ����������
            var response = await client.SendAsync(request);

            // ��������� ���������
            // ��� �� 200 ��� �������� � 201 ��� ������� �� �������� �������� ����� � ������ 
            Assert.True((int)response.StatusCode == 201);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);
        }

        /// <summary>
        /// ������� ��� ������� ������
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
