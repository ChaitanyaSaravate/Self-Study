using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Abstractions.Internal.Framework.Entities;
using Abstractions.Internal.Framework.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace Business.Framework
{
    public class ExternalDataReader : IDataReader
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ExternalDataReader(IHttpClientFactory httpClientFactory, IHostingEnvironment hostingEnvironment)
        {
            _httpClientFactory = httpClientFactory;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<bool> ReadData<T>(T request, EduEntity entityToArchive)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.BaseAddress = new Uri("http://localhost:62774/");
                    var requestMessage = new HttpRequestMessage();
                    requestMessage.Method = HttpMethod.Post;

                    var requestString = JsonSerializer.Serialize<T>(request);

                    var content = new StringContent(requestString, Encoding.Default, "application/json");
                    var response = await httpClient.PostAsync(entityToArchive.ArchiveEndpoints.First(), content);
                    var result = await response.Content.ReadAsStringAsync();
                    var fileStreamWriter = File.CreateText(_hostingEnvironment.ContentRootPath + $"{entityToArchive.Name}.json");
                    fileStreamWriter.WriteLine(result);
                    fileStreamWriter.Dispose();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
    }
}
