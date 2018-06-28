using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace EPAM.TvMaze.RestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EPAM.TvMaze.Infrastructure.LoggerConfiguration.InitializeLogger();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog();
    }
}
