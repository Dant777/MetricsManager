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
     

        private readonly ILogger<WeatherForecastController> _logger;
        private WeatherForecastCollection _weatherForecasts;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecastCollection weatherForecasts)
        {
            _logger = logger;
            _weatherForecasts = weatherForecasts;
          
        }

        /// <summary>
        /// Добавить температуру и дату в базу данных
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="temperature">Температура</param>
        /// <returns></returns>
        [HttpPost("SaveTemp")]
        public IActionResult SaveTemp([FromQuery] DateTime date, [FromQuery] int temperature)
        {
           

            _weatherForecasts.Collection.Add(new WeatherForecast
            {

                Date = date,
                TemperatureC = temperature

            });
            return Ok();
        }

        /// <summary>
        /// Сохранить температуру в указанное время
        /// </summary>
        /// <param name="date">дата и время редактирование</param>
        /// <param name="temperature">температура</param>
        /// <returns></returns>
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
            _weatherForecasts.Collection.Add( new WeatherForecast
            {
               
                Date = date,
                TemperatureC = temperature

            });
            return Ok();
        }

        /// <summary>
        /// Вывести все данные
        /// </summary>
        /// <returns></returns>
        [HttpGet("ReadAll")]
        public IActionResult ReadAll()
        {
            return Ok(_weatherForecasts.Collection);
        }

        /// <summary>
        /// Редактирует показатель температуры в указанное время
        /// </summary>
        /// <param name="date">Дата и вермя для редактирование</param>
        /// <param name="temperatureToUpdate">Температуру которую надо отредактироватьт</param>
        /// <param name="newTemperature">Новый показатель температуры</param>
        /// <returns></returns>
        [HttpPut("UpdateInTime")]
        public IActionResult UpdateInTime([FromQuery] DateTime date, [FromQuery] int temperatureToUpdate, [FromQuery] int newTemperature)
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

            foreach (var item in _weatherForecasts.Collection)
            {
                if (item.TemperatureC == temperatureToUpdate)
                {
                    item.TemperatureC = newTemperature;
                }
            }

            return Ok();
        }

        /// <summary>
        /// Удаляет показатель температуры в указанный промежуток времени
        /// </summary>
        /// <param name="startDate">Начальная дата</param>
        /// <param name="endDate">Конечная дата интервала</param>
        /// <param name="temperature">Температура для удаления</param>
        /// <returns></returns>
        [HttpDelete("DeleteInTimeInterval")]
        public IActionResult DeleteInTimeInterval([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int temperature)
        {

            foreach (var item in _weatherForecasts.Collection)
            {
                int t_1 = DateTime.Compare(startDate, item.Date);
                int t_2 = DateTime.Compare(endDate, item.Date);

                bool isInTimeInterval = (t_1 <= 0) && (t_2 >= 0);
                if (isInTimeInterval)
                {
                    if (item.TemperatureC == temperature)
                    {
                        _weatherForecasts.Collection.Remove(item);
                        break;
                    }
                }
            }
            return Ok();
        }
    }
}
