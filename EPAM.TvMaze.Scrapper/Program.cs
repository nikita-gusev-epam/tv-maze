using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using EPAM.TvMaze.Infrastructure;
using EPAM.TvMaze.Scrapper.Contracts.Services;
using EPAM.TvMaze.Scrapper.Services;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace EPAM.TvMaze.Scrapper
{
    public static class Program
    {
        public static async Task Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            Trace.CorrelationManager.ActivityId = Guid.NewGuid();

            InitializeLogger();
            AutoMapperConfiguration.InitializeMapper();

            using (var container = ContainerConfiguration.Configure(new ContainerBuilder()).Build())
            {
                var showsSynchronizationService = container.Resolve<IShowsSynchronizationService>();
                await showsSynchronizationService.SynchronizeAsync();
            }
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            Log.Logger.Fatal((Exception)unhandledExceptionEventArgs.ExceptionObject, "Application crashed");
        }

        private static void InitializeLogger()
        {
            Log.Logger = new Serilog.LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.With<CorrelationIdEnricher>()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code,
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u}] {CorrelationId} {SourceContext} {Message}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}
