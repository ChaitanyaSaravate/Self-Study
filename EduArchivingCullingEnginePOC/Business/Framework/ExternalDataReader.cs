using System;
using System.Collections.Generic;
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
        private readonly InputOutputFilesManager _inputOutputFilesManager;

        public ExternalDataReader(IHttpClientFactory httpClientFactory, InputOutputFilesManager inputOutputFilesManager)
        {
            _httpClientFactory = httpClientFactory;
            _inputOutputFilesManager = inputOutputFilesManager;
        }

        public async Task<List<string>> ReadData<T>(T request, EduEntity entityToArchive)
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

                    //TODO: It's calling one endpoint at present. You may have to call multiple endpoints.
                    var response = await httpClient.PostAsync(entityToArchive.ArchiveEndpoints.First(), content);
                    var result = await response.Content.ReadAsStringAsync();

                    var filePath = _inputOutputFilesManager.CreateInputDataFile(entityToArchive.Name, result);
                   
                    //TODO: Return list of files containing data only when the operation is successful.
                    return new List<string>
                    {
                        filePath
                    } ;
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
