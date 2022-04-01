using System;

namespace MetricsAgent
{
    public class JobSchedule
    {
        public JobSchedule(Type jobtype, string cronException)
        {
            JobType = jobtype;
            CronException = cronException;
        }

        public Type JobType { get;  }
        public string CronException { get; }
    }
}
