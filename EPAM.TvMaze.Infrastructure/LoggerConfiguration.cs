using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace EPAM.TvMaze.Infrastructure
{
    public class LoggerConfiguration
    {
        public static void InitializeLogger()
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
