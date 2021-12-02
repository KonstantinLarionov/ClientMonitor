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
        private readonly string Path = @"C:\Test\Test1\����-��-�����_2021_11_29_13_7_24 � ����� (3).mp4";

        /// <summary>
        /// ������� ����� API
        /// </summary>
        private readonly string BaseUrl = "https://webdav.yandex.ru";

        /// <summary>
        /// ���� ����� � ������ (��� ������ � ������)
        /// </summary>
        private readonly string PathOnCloud = "����-��-�����_2021_11_29_13_7_24 � ����� (3).mp4";

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
            #region [������ ������]

            // ������� �������
            HttpClient client = new HttpClient();

            //������ Expect: 100-continue
            client.DefaultRequestHeaders.ExpectContinue = false;
            client.BaseAddress = new Uri(BaseUrl);
            // ����� ���� � �����
            StreamContent sr = new StreamContent(new FileStream(Path, FileMode.Open));
            // ������� ������
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, PathOnCloud);
            request.Headers.Add("Authorization", $"Basic {BasicAuth}");
            request.Headers.Add("Transfer-Encoding", TransferEncoding);

            // �������� ���� � ������
            request.Content = sr;
            // ��������� ��������� �������
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);


            //����������
            var response = await client.SendAsync(request);





            //Task<HttpResponseMessage> response = client.SendAsync(request);
            Assert.True((int)response.StatusCode == 201);

            //var response = await client.SendAsync(request);
            // ��������� ���������
            // ��� �� 200 ��� �������� � 201 ��� ������� �� �������� �������� ����� � ������ 
            //Assert.True((int)response.Result.StatusCode == 201);

            //Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created);

            #endregion

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
