using EPAM.TvMaze.Contracts;
using EPAM.TvMaze.RestApi.Contracts;
using EPAM.TvMaze.RestApi.Infrastructure.Correlation;
using EPAM.TvMaze.RestApi.Infrastructure.RequestLogging;
using EPAM.TvMaze.RestApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace EPAM.TvMaze.RestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IShowsService, ShowsService>();
            services.AddScoped<IShowsRepository, ShowsRepository>();
            services.AddScoped<IShowsRepository, ShowsRepository>();
            services.AddTransient<CorrelationMiddleware>();
            services.AddTransient<RequestLoggingMiddleware>();

            var connectionString = Configuration.GetValue<string>("MongoDbConnectionString");
            var databaseName = Configuration.GetValue<string>("TvMazeDbName");
            var showsCollectionName = Configuration.GetValue<string>("ShowsCollectionNameName");
            services.AddScoped<IMongoCollection<ShowEntity>>(sb => new MongoClient(connectionString)
                .GetDatabase(databaseName)
                .GetCollection<ShowEntity>(showsCollectionName));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCorrelationMiddleware();
            app.UseRequestLoggingMiddleware();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
