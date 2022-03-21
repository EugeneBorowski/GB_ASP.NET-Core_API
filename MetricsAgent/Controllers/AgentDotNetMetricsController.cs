using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using AutoMapper;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet/errors-count")]
    [ApiController]
    public class AgentDotNetMetricsController : ControllerBase
    {
        private IDotNetMetricsRepository _repository;
        private readonly ILogger<AgentDotNetMetricsController> _logger;
        private readonly IMapper _mapper;

        public AgentDotNetMetricsController(IDotNetMetricsRepository repository, ILogger<AgentDotNetMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
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
            IList<DotNetMetrics> metrics = _repository.GetAll();
            var response = new AllDotNetMetricsResponse
            {
                Metrics = new List<DotNetMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricsDto>(metric));
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