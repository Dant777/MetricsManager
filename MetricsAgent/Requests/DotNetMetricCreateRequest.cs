using System;

namespace MetricsAgent
{
    public class DotNetMetricCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
