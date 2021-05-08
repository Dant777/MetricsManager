using System;

namespace MetricsAgent
{
    public class DotNetMetricCreateRequest
    {
        public DateTime Time { get; set; }
        public int Value { get; set; }
    }
}
