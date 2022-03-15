using GB_ASP.NET_Core_API.Class;
using Microsoft.AspNetCore.Mvc;

namespace GB_ASP.NET_Core_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly AgentsHolder _holder;

        public AgentsController(AgentsHolder holder)
        {
            _holder = holder;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpGet("showregagents")]
        public IActionResult ShowRegisteredAgents()
        {
            var result = "Active agents:"+"\n";
            foreach (var e in _holder.Values)
            {
                result += e+"\n";
            }
            return Ok(result);
        }
    }
}
