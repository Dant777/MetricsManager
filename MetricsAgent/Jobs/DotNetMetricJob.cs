using System.Threading.Tasks;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {

        private IDotNetMetricsRepository _repository;

        public DotNetMetricJob(IDotNetMetricsRepository repository)
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
