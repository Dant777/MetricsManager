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
        public HddAgentController(ILogger<HddAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddAgentController");
        }
        
        public IActionResult GetDiskSpace()
        [HttpGet("left/{fromTime}/to/{toTime}")]
        public IActionResult GetDiskSpace([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"GetDiskSpace;");
            return Ok();
        }

    }
}
