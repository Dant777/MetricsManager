using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {

        private IDotNetMetricsRepository _repository;
        private Random _rnd;

        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _rnd = new Random();
        }

        public Task Execute(IJobExecutionContext context)
        {
            
            int randomMetric = _rnd.Next(1, 100);

            long time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            _repository.Create(new DotNetMetric { Time = time, Value = randomMetric });

            return Task.CompletedTask;
        }

    }
}
