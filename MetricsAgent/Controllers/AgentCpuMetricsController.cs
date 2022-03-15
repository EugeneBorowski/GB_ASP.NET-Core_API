using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class AgentCpuMetricsController : ControllerBase
    {
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetCpuMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetCpuMetricsPercentile([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] int percentile)
        {
            return Ok();
        }
    }
}
