﻿using MetricsManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuAgentController : ControllerBase
    {
        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAgent([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] Percentile percentil)
        {
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent( [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}