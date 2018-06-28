using System.Collections.Specialized;
using System.Configuration;
using System.Threading;
using Autofac;
using Autofac.Extras.Quartz;
using EPAM.TvMaze.Contracts;
using EPAM.TvMaze.Scrapper.Contracts.Services;
using EPAM.TvMaze.Scrapper.Services;
using MongoDB.Driver;

namespace EPAM.TvMaze.Scrapper
{
    public class ContainerConfiguration
    {
        internal static ContainerBuilder Configure(ContainerBuilder cb)
        {
            // configure and register Quartz
            var schedulerConfig = new NameValueCollection {
                {"quartz.threadPool.threadCount", "3"},
                {"quartz.scheduler.threadName", "Scheduler"}
            };

            cb.RegisterModule(new QuartzAutofacFactoryModule
            {
                ConfigurationProvider = c => schedulerConfig
            });
            cb.RegisterModule(new QuartzAutofacJobsModule(typeof(ContainerConfiguration).Assembly));

            var host = ConfigurationManager.AppSettings["TvMazeApiEndpoint"];
            cb.Register(c => new TvMazeApi(host)).As<ITvMazeApi>().SingleInstance();


            cb.RegisterType<ShowsSynchronizationService>().As<IShowsSynchronizationService>();

            var connectionString = ConfigurationManager.AppSettings["MongoDbConnectionString"];
            var databaseName = ConfigurationManager.AppSettings["TvMazeDbName"];
            var showsCollectionName = ConfigurationManager.AppSettings["ShowsCollectionNameName"];
            cb.Register(c => new MongoClient(connectionString)
                    .GetDatabase(databaseName)
                    .GetCollection<ShowEntity>(showsCollectionName))
                .As<IMongoCollection<ShowEntity>>();
            cb.RegisterType<ShowsRepository>().As<IShowsRepository>();
            cb.RegisterType<ScrapperScheduler>().As<IScrapperScheduler>();
            cb.RegisterType<CancellationTokenSource>().AsSelf().SingleInstance();

            return cb;
        }

    }
}
