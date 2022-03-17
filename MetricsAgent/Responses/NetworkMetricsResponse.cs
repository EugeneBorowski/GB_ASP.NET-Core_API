using System;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class AllNetworkMetricsResponse
    {
        public List<NetworkMetricsDto> Metrics { get; set; }
    }
    public class NetworkMetricsDto
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
