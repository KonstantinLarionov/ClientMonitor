using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Objects;
using ClientMonitor.Infrastructure.CloudManager;
using ClientMonitor.Infrastructure.CloudManager.Adaptors;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ClientMonitor.Test.ModulesTests
{
    public class CloudTests : IClassFixture<ApplicationStanding>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        private readonly CloudsFactory service;
        public CloudTests(ApplicationStanding factory)
        {
           // var a = new YandexAdaptor(,);
            _factory = factory;
            //_factory.Services.GetService(YandexAdaptor.ReferenceEquals);
            //Пример получения сервиса для теста (нужно сделать глобальным и писать тесты с ним)
            service = _factory.Services.GetRequiredService<ICloudFactory>() as CloudsFactory;
        }

        [Fact]
        public void NameTest_Success()
        {
            var Cloud = service.GetCloud(Application.Domanes.Enums.CloudTypes.YandexCloud);
            string[] getFilesFromHall = Directory.GetFiles(@"C:\Users\Павел\Desktop\Тесты", "*.mp4");
            
            string[] files = GetWitoutLastElement(getFilesFromHall, getFilesFromHall.Length);

            Assert.NotNull(files); // проверка файла
            //не понимаю как проверить выполнение функции загрузки
            //foreach (var file in files)
            //{
            //    FileInfo fileInf = new FileInfo(file);
            //    var uploadFile = GetUploadFile(fileInf, "Тест");
            //    Cloud.UploadFiles(uploadFile);
            //    fileInf.Delete();
            //}

            // var res = GetFilesAndFoldersAsync();
        }
        [Fact]
        public void NameTest_Error()
        {

        }


        private string[] GetWitoutLastElement(string[] mas, int leght)
        {
            string[] files = new string[leght - 1];
            for (int i = 0; i < leght - 1; i++)
                files[i] = mas[i];
            return files;
        }

        private UploadedFilesInfo GetUploadFile(FileInfo fileInf, string pathToLoad)
        {
            UploadedFilesInfo uploadedFiles = new UploadedFilesInfo();

            uploadedFiles.Name = fileInf.Name;
            uploadedFiles.Extension = fileInf.Extension;
            uploadedFiles.Create = fileInf.CreationTime;
            uploadedFiles.Path = fileInf.DirectoryName;
            uploadedFiles.FolderName = pathToLoad;
            return uploadedFiles;
        }
    }
}
