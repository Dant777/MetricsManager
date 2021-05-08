using System;

namespace MetricsAgent
{
    public class HddMetricCreateRequest
    {
        public DateTime Time { get; set; }
        public int Value { get; set; }
    }
}
