using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddAgentController : ControllerBase
    {
        private readonly ILogger<HddAgentController> _logger;
        private IHddMetricsRepository _repository;

        public HddAgentController(IHddMetricsRepository repository, ILogger<HddAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddAgentController");
            _repository = repository;
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _repository.Create(new HddMetric
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

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            if (metrics == null)
            {
                return Ok(response);
            }

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto { Time = DateTimeOffset.FromUnixTimeSeconds(metric.Time), Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetByTymePeriod([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"HddAgentController: fromTime = {fromTime}; toTime = {toTime};");

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            if (metrics == null)
            {
                return Ok(response);
            }

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto { Time = DateTimeOffset.FromUnixTimeSeconds(metric.Time), Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }

    }
}
