using System;

namespace MetricsAgent.Requests
{
    public class DotNetMetricsCreateRequest
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
    }
}
