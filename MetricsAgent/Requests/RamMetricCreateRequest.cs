﻿using System;

namespace MetricsAgent
{
    public class RamMetricCreateRequest
    {
        public DateTime Time { get; set; }
        public int Value { get; set; }
    }
}
