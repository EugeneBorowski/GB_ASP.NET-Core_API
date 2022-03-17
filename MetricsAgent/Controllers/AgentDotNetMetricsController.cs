using MetricsAgent.DAL;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet/errors-count")]
    [ApiController]
    public class AgentDotNetMetricsController : ControllerBase
    {
        private IDotNetMetricsRepository _repository;
        private readonly ILogger<AgentDotNetMetricsController> _logger;

        public AgentDotNetMetricsController(IDotNetMetricsRepository repository, ILogger<AgentDotNetMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricsCreateRequest request)
        {
            _logger.LogInformation("AgentDotNetMetricsControllerCreate call: " + request);
            _repository.Create(new DotNetMetrics
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("AgentDotNetMetricsControllerGetAll call");
            var metrics = _repository.GetAll();

            var response = new AllDotNetMetricsResponse
            {
                Metrics = new List<DotNetMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new DotNetMetricsDto
                {
                    Time = metric.Time,
                    Value = metric.Value,
                    Id = metric.Id
                });
            }

            _logger.LogInformation("AgentDotNetMetricsControllerGetAll response:\n");
            return Ok(response);
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetDotNetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("GetDotNetMetricsFromAllCluster call: " + fromTime + " " + toTime);
            return Ok();
        }
    }
}