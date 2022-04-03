using System;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class AllDotNetMetricsResponse
    {
        public List<DotNetMetricsDto> Metrics { get; set; }
    }
    public class DotNetMetricsDto
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
