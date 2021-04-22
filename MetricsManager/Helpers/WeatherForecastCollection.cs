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
            _weatherForecasts = CreateData().ToList();
        }

        public List<WeatherForecast> GetCollection { get => _weatherForecasts; }

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
