using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public Task<IActionResult> ShowWeatherReport()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44330/");
                var result = await client.GetAsync("WeatherForecast");
                string weatherReport = await result.Content.ReadAsStringAsync();
            }

            return View("ShowWeatherReport");
        }

        //public IActionResult ShowWeatherReport()
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://localhost:44330/");
        //        var result = client.GetAsync("WeatherForecast").Result;
        //        string weatherReport = result.Content.ReadAsStringAsync().Result;
        //    }

        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
