using System;

namespace MetricsAgent.Requests
{
    public class NetworkMetricsCreateRequest
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
    }
}
