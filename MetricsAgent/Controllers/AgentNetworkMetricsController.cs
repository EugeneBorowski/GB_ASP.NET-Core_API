using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class AgentNetworkMetricsController : ControllerBase
    {
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetNetworkMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
