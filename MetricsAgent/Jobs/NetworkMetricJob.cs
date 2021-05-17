using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {

        private INetworkMetricsRepository _repository;
        private PerformanceCounter _networkCounter;

        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            PerformanceCounterCategory pcg = new PerformanceCounterCategory("Network Interface"); 
            string instance = pcg.GetInstanceNames()[0];
            _networkCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", instance);
            
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
            var networkUsageInPercents = Convert.ToInt32(_networkCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            long time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new NetworkMetric { Time = time, Value = networkUsageInPercents });

            return Task.CompletedTask;
        }

    }
}
