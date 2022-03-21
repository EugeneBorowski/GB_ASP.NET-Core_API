using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AutoMapper;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class AgentRamMetricsController : ControllerBase
    {
        private IRamMetricsRepository _repository;
        private readonly ILogger<AgentRamMetricsController> _logger;
        private readonly IMapper _mapper;

        public AgentRamMetricsController(IRamMetricsRepository repository, ILogger<AgentRamMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricsCreateRequest request)
        {
            _logger.LogInformation("AgentRamMetricsControllerCreate call: " + request);
            _repository.Create(new RamMetrics
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("AgentRamMetricsControllerGetAll call");
            IList<RamMetrics> metrics = _repository.GetAll();
            var response = new AllRamMetricsResponse
            {
                Metrics = new List<RamMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricsDto>(metric));
            }

            _logger.LogInformation("AgentRamMetricsControllerGetAll response:\n");
            return Ok(response);
        }

        [HttpGet("available")]
        public IActionResult GetRamMetrics()
        {
            _logger.LogInformation("AgentAvailableRam call");
            return Ok();
        }
    }
}
