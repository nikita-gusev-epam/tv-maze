using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EPAM.TvMaze.Scrapper.Contracts.Services;
using Quartz;
using Serilog;

namespace EPAM.TvMaze.Scrapper
{
    [DisallowConcurrentExecution]
    public class ShowsSynchronizationJob : IJob
    {
        private static readonly ILogger Logger = Log.ForContext<ShowsSynchronizationJob>();

        private readonly IShowsSynchronizationService _showsSynchronizationService;

        public ShowsSynchronizationJob(IShowsSynchronizationService showsSynchronizationService)
        {
            _showsSynchronizationService = showsSynchronizationService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Trace.CorrelationManager.ActivityId = Guid.NewGuid();
                await _showsSynchronizationService.SynchronizeAsync();
            }
            catch (Exception e)
            {
                Logger.Error(e, "Exception processing synchronization job");
            }
        }
    }
}
