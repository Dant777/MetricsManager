using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
{
    public class WeatherForecastCollection
    {
        private List<WeatherForecast> _weatherForecasts;

        public WeatherForecastCollection()
        {
            _weatherForecasts = new List<WeatherForecast>(){
                new WeatherForecast() { Date = new DateTime(2021, 04, 20, 7, 0, 0 ), TemperatureC = 10 },
                new WeatherForecast() { Date = new DateTime(2021, 04, 20, 8, 0, 0 ), TemperatureC = 12 },
                new WeatherForecast() { Date = new DateTime(2021, 04, 21, 8, 0, 0 ), TemperatureC = 14 },
                new WeatherForecast() { Date = new DateTime(2021, 04, 22, 9, 0, 0 ), TemperatureC = 16 },
                new WeatherForecast() { Date = new DateTime(2021, 04, 23, 7, 0, 0 ), TemperatureC = 18 },
                new WeatherForecast() { Date = new DateTime(2021, 04, 24, 7, 0, 0 ), TemperatureC = 20 },
            };
        }

        public List<WeatherForecast> Collection { get => _weatherForecasts; set 
            {
                _weatherForecasts = value;
            }
        }

        private IEnumerable<WeatherForecast> CreateData()
        {
            var rng = new Random();
            return Enumerable.Range(1, 6).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),

            });
            
        }
    }
}
