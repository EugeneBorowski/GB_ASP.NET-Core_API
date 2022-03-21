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
    [Route("api/metrics/hdd")]
    [ApiController]
    public class AgentHddMetricsController : ControllerBase
    {
        private IHddMetricsRepository _repository;
        private readonly ILogger<AgentHddMetricsController> _logger;
        private readonly IMapper _mapper;

        public AgentHddMetricsController(IHddMetricsRepository repository, ILogger<AgentHddMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
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
            IList<HddMetrics> metrics = _repository.GetAll();
            var response = new AllHddMetricsResponse
            {
                Metrics = new List<HddMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricsDto>(metric));
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
