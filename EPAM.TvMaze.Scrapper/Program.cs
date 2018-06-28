using System;
using System.Diagnostics;
using System.Threading;
using Autofac;
using EPAM.TvMaze.Infrastructure;
using EPAM.TvMaze.Scrapper.Contracts.Services;

namespace EPAM.TvMaze.Scrapper
{
    public static class Program
    {
        static void Main()
        {
            Trace.CorrelationManager.ActivityId = Guid.NewGuid();
            LoggerConfiguration.InitializeLogger();

            using (var container = ContainerConfiguration.Configure(new ContainerBuilder()).Build())
            using (var scheduler = container.Resolve<IScrapperScheduler>())
            using (var cts = container.Resolve<CancellationTokenSource>())
            {
                var cronExpression = System.Configuration.ConfigurationManager.AppSettings["ShowsSyncCronExpression"];
                scheduler.Schedule<ShowsSynchronizationJob>(cronExpression);

                scheduler.Start();
                Console.ReadKey();
                cts.Cancel();
            }
        }
    }
}
