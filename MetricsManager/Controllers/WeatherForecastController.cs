using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
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
        private WeatherForecastCollection _weatherForecasts;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecastCollection weatherForecasts)
        {
            _logger = logger;
            _weatherForecasts = weatherForecasts;
          
        }


        [HttpPost("SaveTemp")]
        public IActionResult SaveTemp([FromQuery] DateTime date, [FromQuery] int temperature)
        {
           

            _weatherForecasts.GetCollection.Add(new WeatherForecast
            {

                Date = date,
                TemperatureC = temperature

            });
            return Ok();
        }
        [HttpPost("SaveTempInTime")]
        public IActionResult SaveTempInTime([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            bool isTime = true;

            while (isTime)
            {
                int n = DateTime.Compare(DateTime.Now, date);
                if (n >= 0)
                {
                    isTime = false;
                }
            }
            _weatherForecasts.GetCollection.Add( new WeatherForecast
            {
                //0001-01-01T00:00:00
                Date = date,
                TemperatureC = temperature

            });
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok(_weatherForecasts.GetCollection);
        }

    }
}
