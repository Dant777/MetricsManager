using System.Collections.Generic;

namespace MetricsAgent
{
    public class AllDotNetMetricsResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }
}
