using System;

namespace MetricsAgent
{
    public class RamMetricCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
