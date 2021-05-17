using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {

        private IHddMetricsRepository _repository;
        private PerformanceCounter _hddCounter;

        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter("PhysicalDisk", "% Disk Read Time", "0 C:");
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
            var hddUsageInPercents = Convert.ToInt32(_hddCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            long time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new HddMetric { Time = time, Value = hddUsageInPercents });

            return Task.CompletedTask;
        }

    }
}
