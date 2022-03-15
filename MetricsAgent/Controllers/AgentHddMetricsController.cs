using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class AgentHddMetricsController : ControllerBase
    {
        [HttpGet("left")]
        public IActionResult GetHddLeftMetrics()
        {
            return Ok();
        }
    }
}
