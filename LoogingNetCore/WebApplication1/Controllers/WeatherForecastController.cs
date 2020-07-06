using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel;
using System.Threading.Tasks;

using CompanyWcfServiceReference;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebServiceContracts;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //public async Task<IEnumerable<Company>> Get()
        //{
        //    CompanyServiceClient wcfClient = new CompanyServiceClient(new BasicHttpBinding
        //        {
        //            MaxReceivedMessageSize = 200000000
        //        }, 
        //        new EndpointAddress("http://localhost/WcfServices/CompanyService.svc/basic"));

        //    var result = await wcfClient.GetAllCompaniesAsync();
        //    return result;

        //    //var rng = new Random();
        //    //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    //{
        //    //    Date = DateTime.Now.AddDays(index),
        //    //    TemperatureC = rng.Next(-20, 55),
        //    //    Summary = Summaries[rng.Next(Summaries.Length)]
        //    //})
        //    //.ToArray();
        //}

        [HttpGet]
        public async Task<string> Get()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost/WcfServices/");
                var response = await client.GetAsync("CompanyService.svc/web/GetAllCompanies");
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }


            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
        }

        //[HttpGet]
        //public async Task<IEnumerable<WeatherForecast>> GetAsync()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //        {
        //            Date = DateTime.Now.AddDays(index),
        //            TemperatureC = rng.Next(-20, 55),
        //            Summary = Summaries[rng.Next(Summaries.Length)]
        //        })
        //        .ToArray();
        //}
    }
}
