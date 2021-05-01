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
        public NetworkAgentController(ILogger<NetworkAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkAgentController");
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"GetMetricsFromAgent: fromTime = {fromTime}; toTime = {toTime};");
            return Ok();
        }
    }
}
