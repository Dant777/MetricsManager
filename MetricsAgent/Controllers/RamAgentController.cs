using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamAgentController : ControllerBase
    {
        private readonly ILogger<RamAgentController> _logger;
        private IRamMetricsRepository _repository;

        public RamAgentController(IRamMetricsRepository repository, ILogger<RamAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamAgentController");
            _repository = repository;
        }


        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _repository.Create(new RamMetric
            {
                Time = request.Time.ToUnixTimeSeconds(),
                Value = request.Value
            });

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            if (metrics == null)
            {
                return Ok(response);
            }

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricDto { Time = DateTimeOffset.FromUnixTimeSeconds(metric.Time), Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetByTymePeriod([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            _logger.LogInformation($"RamAgentController: fromTime = {fromTime}; toTime = {toTime};");
            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            if (metrics == null)
            {
                return Ok(response);
            }

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricDto { Time = DateTimeOffset.FromUnixTimeSeconds(metric.Time), Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);

        }

    }
}
