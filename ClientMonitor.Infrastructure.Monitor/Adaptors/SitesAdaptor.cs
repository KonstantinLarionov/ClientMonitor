using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes.Enums;
using ClientMonitor.Application.Domanes.Objects;
using RestSharp;
using System;
using System.Collections.Generic;


namespace ClientMonitor.Infrastructure.Monitor.Adaptors
{
    public class SitesAdaptor : IMonitor
    {
        #region [ClientData]
        public readonly List<Client> Сlients = new List<Client>()
        {
            new Client
            {
                ClientName = "Давыдов",
                DateCreate = DateTime.Now,
                ClientResources = new List<ClientResource>()
                {
                     new ClientResource
                     {
                         Name = "Личный сайт",
                         Path = "https://companyvd.ru/",
                         Type = ClientResourseType.Site,
                     },
                }
            },
            new Client
            {
                ClientName = "Ормек",
                DateCreate = DateTime.Now,
                ClientResources = new List<ClientResource>()
                {
                    new ClientResource
                    {
                        Name = "Личный сайт",
                        Path = "https://ormek.ru/",
                        Type = ClientResourseType.Site,
                    },
                }
            },
            new Client
            {
                ClientName = "Памятники",
                DateCreate = DateTime.Now,
                ClientResources = new List<ClientResource>()
                {
                    new ClientResource
                    {
                        Name = "Личный сайт",
                        Path = "https://pamiatnikigm.ru/",
                        Type = ClientResourseType.Site,
                    },
                }
            },
            new Client
            {
                ClientName = "Афк",
                DateCreate = DateTime.Now,
                ClientResources = new List<ClientResource>()
                {
                    new ClientResource
                    {
                        Name = "Личный сайт",
                        Path = "https://afcstudio.ru/",
                        Type = ClientResourseType.Site,
                    },
                    new ClientResource
                    {
                        Name = "Личный сайт",
                        Path = "https://cleancash.net/",
                        Type = ClientResourseType.Site,
                    }
                }
            },
            new Client
            {
                ClientName = "Золотой кот",
                DateCreate = DateTime.Now,
                ClientResources = new List<ClientResource>()
                {
                    new ClientResource
                    {
                        Name = "Личный сайт",
                        Path = "http://goldencat.su/",
                        Type = ClientResourseType.Site,
                    },
                    new ClientResource
                    {
                        Name = "Личный сайт",
                        Path = "http://badik56.ru/",
                        Type = ClientResourseType.Site,
                    }
                }
            },
            new Client
            {
                ClientName = "Руслан",
                DateCreate = DateTime.Now,
                ClientResources = new List<ClientResource>()
                {
                    new ClientResource
                    {
                        Name = "Личный сайт",
                        Path = "https://korall56.ru/",
                        Type = ClientResourseType.Site,
                    },
                }
            },
        };
        #endregion

        public object ReceiveInfoMonitor()
        {
            List<ResultMonitoring> resultMonitoring = new List<ResultMonitoring>();
            foreach (var client in Сlients)
            {
                var resources = client.ClientResources;
                foreach (var resource in resources)
                {
                    if (resource.Type == ClientResourseType.Site)
                    {
                        var result = CheckClientResources(resource);
                        if (result.Success)
                        { resultMonitoring.Add(new ResultMonitoring(true, $"Client: {client.ClientName}, Resource: {resource.Name}, Path: {resource.Path} Успешно")); }
                        else
                        { resultMonitoring.Add(new ResultMonitoring(false, $"Client: {client.ClientName}, Resource: {resource.Name}, Path: {resource.Path} НЕ РАБОТАЕТ")); }

                    }
                    else { continue; }
                }
            }
            return resultMonitoring;
        }

        private ResultCheckStatus CheckClientResources(ClientResource resource)
        {
            if (resource.Type != ClientResourseType.Site)
            { return new ResultCheckStatus("Unrecognized resource!", DateTime.Now, false); }

            var request = new RestRequest("", Method.GET);

            var res = SendRequest(resource.Path, request);

            var resultStatusCode = res.StatusCode;
            var content = res.Content;

            if (resultStatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrEmpty(content))
            {
                return new ResultCheckStatus("Ok check status", DateTime.Now, true);
            }
            else
            {
                return new ResultCheckStatus("Fail", DateTime.Now, false);
            }
        }

        private IRestResponse SendRequest(string baseUrl, RestRequest request)
        {
            RestClient restClient = new RestClient(new Uri(baseUrl));
            var res = restClient.Execute(request);
            return res;
        }
    }
}
