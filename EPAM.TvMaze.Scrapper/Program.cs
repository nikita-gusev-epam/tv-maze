using System;
using System.Diagnostics;
using System.Threading;
using System.Xml.Linq;
using Autofac;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace EPAM.TvMaze.Scrapper
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Trace.CorrelationManager.ActivityId = Guid.NewGuid();
            InitializeLogger();

            using (var container = ContainerConfiguration.Configure(new ContainerBuilder()).Build())
            using (var scheduler = container.Resolve<IScrapperScheduler>())
            using (var cts = container.Resolve<CancellationTokenSource>())
            {
                scheduler.Schedule<ShowsSynchronizationJob>("*/5 * * * * ?");

                scheduler.Start();
                Console.ReadKey();
                cts.Cancel();
            }
        }

        private static void InitializeLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.With<CorrelationIdEnricher>()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code,
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u}] {CorrelationId} {SourceContext} {Message}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}
