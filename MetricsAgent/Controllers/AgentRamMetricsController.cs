using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class AgentRamMetricsController : ControllerBase
    {
        [HttpGet("available")]
        public IActionResult GetRamMetrics()
        {
            return Ok();
        }
    }
}
