using EPAM.TvMaze.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;

namespace EPAM.TvMaze.RestApi
{
    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog();
        }

        public static void Main(string[] args)
        {
            InitializeLogger();
            CreateWebHostBuilder(args).Build().Run();
        }

        private static void InitializeLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.With<CorrelationIdEnricher>()
                .WriteTo.Trace(outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u}] {CorrelationId} {SourceContext} {Message}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}
