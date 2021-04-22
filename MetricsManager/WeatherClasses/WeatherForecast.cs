using System;

namespace MetricsManager
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; } = DateTime.Now;

        public int TemperatureC { get; set; } = 0;

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
