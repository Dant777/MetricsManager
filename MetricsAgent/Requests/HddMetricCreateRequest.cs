using System;

namespace MetricsAgent
{
    public class HddMetricCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
