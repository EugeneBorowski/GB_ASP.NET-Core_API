using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
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
            return Task.CompletedTask;
        }
    }
}
