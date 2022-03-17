using GB_ASP.NET_Core_API.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GB_ASP.NET_Core_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly AgentsHolder _holder;
        private readonly ILogger<AgentsController> _logger;

        public AgentsController(AgentsHolder holder)
        {
            _holder = holder;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation("RegisterAgent call:" + agentInfo);
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation("EnableAgentById call" + agentId);
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation("DisableAgentById call" + agentId);
            return Ok();
        }

        [HttpGet("showregagents")]
        public IActionResult ShowRegisteredAgents()
        {
            _logger.LogInformation("ShowRegisteredAgents call");
            var result = "Active agents:" + "\n";
            foreach (var e in _holder.Values)
            {
                result += e + "\n";
            }
            _logger.LogInformation(result);
            return Ok(result);
        }
    }
}
