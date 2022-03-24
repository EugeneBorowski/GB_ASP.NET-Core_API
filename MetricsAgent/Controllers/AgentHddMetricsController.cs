using MetricsAgent.DAL;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class AgentHddMetricsController : ControllerBase
    {
        private IHddMetricsRepository _repository;
        private readonly ILogger<AgentHddMetricsController> _logger;

        public AgentHddMetricsController(IHddMetricsRepository repository, ILogger<AgentHddMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricsCreateRequest request)
        {
            _logger.LogInformation("AgentHddMetricsControllerCreate call: " + request);
            _repository.Create(new HddMetrics
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("AgentHddMetricsControllerGetAll call");
            var metrics = _repository.GetAll();

            var response = new AllHddMetricsResponse
            {
                Metrics = new List<HddMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricsDto
                {
                    Time = metric.Time,
                    Value = metric.Value,
                    Id = metric.Id
                });
            }

            _logger.LogInformation("AgentHddMetricsControllerGetAll response:\n");
            return Ok(response);
        }

        [HttpGet("left")]
        public IActionResult GetHddLeftMetrics()
        {
            _logger.LogInformation("GetHddLeftMetrics call: ");
            return Ok();
        }
    }
}
