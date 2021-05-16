﻿using System.Threading.Tasks;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {

        private IRamMetricsRepository _repository;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            // теперь можно записать что-то при помощи репозитория

            return Task.CompletedTask;
        }

    }
}
