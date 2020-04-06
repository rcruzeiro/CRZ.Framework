using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;

namespace CRZ.Framework.CronJobs
{
    public class JobHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<JobSchedule> _jobSchedules;

        public IScheduler Scheduler { get; private set; }

        public JobHostedService(ISchedulerFactory schedulerFactory,
                                IJobFactory jobFactory,
                                IEnumerable<JobSchedule> jobSchedules)
        {
            _schedulerFactory = schedulerFactory ?? throw new ArgumentNullException(nameof(schedulerFactory));
            _jobFactory = jobFactory ?? throw new ArgumentNullException(nameof(jobFactory));
            _jobSchedules = jobSchedules ?? throw new ArgumentNullException(nameof(jobSchedules));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;

            foreach (var schedule in _jobSchedules)
            {
                var job = CreateJob(schedule);
                var trigger = CreateTrigger(schedule);

                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }

            await Scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        private static IJobDetail CreateJob(JobSchedule job)
        {
            var jobType = job.JobType;

            return JobBuilder.Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
                .Build();
        }

        private static ITrigger CreateTrigger(JobSchedule job)
        {
            return TriggerBuilder.Create()
                .WithIdentity($"{job.JobType.FullName}.trigger")
                .WithCronSchedule(job.CronExpression)
                .WithDescription(job.CronExpression)
                .Build();
        }
    }
}
