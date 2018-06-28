using System.Collections.Specialized;
using System.Threading;
using Autofac;
using Autofac.Extras.Quartz;
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

            var host = "http://api.tvmaze.com";
            cb.Register(c => new TvMazeApi(host)).As<ITvMazeApi>().SingleInstance();


            cb.RegisterType<ShowsSynchronizationService>().As<IShowsSynchronizationService>();

            var connectionString = "mongodb://localhost:32768";
            var databaseName = "TvMaze";
            var showsCollectionName = "Shows";
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