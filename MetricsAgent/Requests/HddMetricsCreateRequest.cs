using System;

namespace MetricsAgent.Requests
{
    public class HddMetricsCreateRequest
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
    }
}
