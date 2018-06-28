using System;
using System.Threading;
using EPAM.TvMaze.Scrapper.Contracts.Services;
using Quartz;

namespace EPAM.TvMaze.Scrapper.Services
{
    public class ScrapperScheduler : IScrapperScheduler
    {
        private readonly CancellationTokenSource _cts;
        private readonly IScheduler _scheduler;

        public ScrapperScheduler(IScheduler scheduler, CancellationTokenSource cts)
        {
            _cts = cts ?? throw new ArgumentNullException(nameof(cts));
            _scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
        }

        public void Schedule<T>(string cronExpression) where T : IJob
        {
            var jobBuilder = JobBuilder.Create<T>();

            var job = jobBuilder.Build();
            var trigger = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule(cronExpression).Build();

            _scheduler.ScheduleJob(job, trigger, _cts.Token);
        }

        public void Start()
        {
            _scheduler.Start().Wait();
        }

        public void Dispose()
        {
            _scheduler.Shutdown().Wait();
        }
    }
}
