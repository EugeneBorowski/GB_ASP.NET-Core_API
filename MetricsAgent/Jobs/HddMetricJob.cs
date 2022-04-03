using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob :IJob
    {
        private IHddMetricsRepository _repository;

        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
