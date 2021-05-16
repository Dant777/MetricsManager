using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkAgentController : ControllerBase
    {
        private readonly ILogger<NetworkAgentController> _logger;
        private INetworkMetricsRepository _repository;
        public NetworkAgentController(INetworkMetricsRepository repository, ILogger<NetworkAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkAgentController");
            _repository = repository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _repository.Create(new NetworkMetric
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

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            if (metrics == null)
            {
                return Ok(response);
            }

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new NetworkMetricDto { Time = DateTimeOffset.FromUnixTimeSeconds(metric.Time), Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetByTymePeriod([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            _logger.LogInformation($"NetworkAgentController: fromTime = {fromTime}; toTime = {toTime};");

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            if (metrics == null)
            {
                return Ok(response);
            }

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new NetworkMetricDto { Time = DateTimeOffset.FromUnixTimeSeconds(metric.Time), Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
    }
}
