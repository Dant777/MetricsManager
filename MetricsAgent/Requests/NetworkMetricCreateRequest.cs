using System;

namespace MetricsAgent
{
    public class NetworkMetricCreateRequest
    {
        public DateTime Time { get; set; }
        public int Value { get; set; }
    }
}
