using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {

        private IRamMetricsRepository _repository;
        private PerformanceCounter _ramCounter;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
            var ramUsageInPercents = Convert.ToInt32(_ramCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            long time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new RamMetric { Time = time, Value = ramUsageInPercents });

            return Task.CompletedTask;
        }

    }
}
