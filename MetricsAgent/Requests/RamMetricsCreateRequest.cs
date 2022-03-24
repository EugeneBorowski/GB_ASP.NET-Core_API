using System;

namespace MetricsAgent.Requests
{
    public class RamMetricsCreateRequest
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
    }
}
