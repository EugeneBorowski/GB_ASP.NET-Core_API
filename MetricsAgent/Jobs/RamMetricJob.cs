using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob :IJob
    {
        private IRamMetricsRepository _repository;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
