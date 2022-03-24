using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class AgentNetworkMetricsController : ControllerBase
    {
        private INetworkMetricsRepository _repository;
        private readonly ILogger<AgentNetworkMetricsController> _logger;

        public AgentNetworkMetricsController(INetworkMetricsRepository repository, ILogger<AgentNetworkMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricsCreateRequest request)
        {
            _logger.LogInformation("AgentNetworkMetricsControllerCreate call: " + request);
            _repository.Create(new NetworkMetrics
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("AgentNetworkMetricsControllerGetAll call");
            var metrics = _repository.GetAll();

            var response = new AllNetworkMetricsResponse
            {
                Metrics = new List<NetworkMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new NetworkMetricsDto
                {
                    Time = metric.Time,
                    Value = metric.Value,
                    Id = metric.Id
                });
            }

            _logger.LogInformation("AgentNetworkMetricsControllerGetAll response:\n");
            return Ok(response);
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetNetworkMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("GetNetworkMetrics call: " + fromTime + " " + toTime);
            return Ok();
        }
    }
}
