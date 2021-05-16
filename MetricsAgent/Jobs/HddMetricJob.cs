using System.Threading.Tasks;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {

        private IHddMetricsRepository _repository;

        public HddMetricJob(IHddMetricsRepository repository)
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
