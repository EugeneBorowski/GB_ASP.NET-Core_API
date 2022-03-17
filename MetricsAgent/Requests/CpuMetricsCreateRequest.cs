using System;

namespace MetricsAgent.Requests
{
    public class CpuMetricsCreateRequest
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
    }
}
