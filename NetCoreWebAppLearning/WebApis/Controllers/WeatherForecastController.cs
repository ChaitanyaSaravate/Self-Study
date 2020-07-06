using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private WeatherForecastDataReader _dataReader;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecastDataReader dataReader)
        {
            _dataReader = dataReader;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await _dataReader.GetWeather();
        }
    }
}
