using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Entities;
using Abstractions.Internal.Framework.Interfaces;

namespace Business.Framework
{
    public class ExternalDataReader : IExternalDataReader
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly InputOutputFilesManager _inputOutputFilesManager;

        public ExternalDataReader(IHttpClientFactory httpClientFactory, InputOutputFilesManager inputOutputFilesManager)
        {
            _httpClientFactory = httpClientFactory;
            _inputOutputFilesManager = inputOutputFilesManager;
        }

        /// <summary>
        /// Connects you to data provider and downloads the data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="entityToArchive"></param>
        /// <returns></returns>
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

                    //TODO: It's calling only one endpoint at present. Improve the code to call multiple and get data from multiple sources.
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
