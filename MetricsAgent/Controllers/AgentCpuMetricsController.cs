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
    [Route("api/metrics/cpu")]
    [ApiController]
    public class AgentCpuMetricsController : ControllerBase
    {
        private ICpuMetricsRepository _repository;
        private readonly ILogger<AgentCpuMetricsController> _logger;

        public AgentCpuMetricsController(ICpuMetricsRepository repository, ILogger<AgentCpuMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricsCreateRequest request)
        {
            _logger.LogInformation("AgentCpuMetricsControllerCreate call: " + request);
            _repository.Create(new CpuMetrics
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("AgentCpuMetricsControllerGetAll call");
            var metrics = _repository.GetAll();

            var response = new AllCpuMetricsResponse
            {
                Metrics = new List<CpuMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricsDto
                {
                    Time = metric.Time,
                    Value = metric.Value,
                    Id = metric.Id
                });
            }

            _logger.LogInformation("AgentCpuMetricsControllerGetAll response:\n");
            return Ok(response);
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetCpuMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("CpuGetMetrics call: " + fromTime + " " + toTime);
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetCpuMetricsPercentile([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] int percentile)
        {
            _logger.LogInformation("GetCpuGetMetricsPercentile: " + fromTime + " " + toTime + " " + percentile);
            return Ok();
        }
    }
}
