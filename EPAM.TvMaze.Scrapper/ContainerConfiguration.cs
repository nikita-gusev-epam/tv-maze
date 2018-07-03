using System.Configuration;
using System.Threading;
using Autofac;
using EPAM.TvMaze.DAL.Shared;
using EPAM.TvMaze.Scrapper.Contracts.Services;
using EPAM.TvMaze.Scrapper.Services;
using MongoDB.Driver;

namespace EPAM.TvMaze.Scrapper
{
    public class ContainerConfiguration
    {
        internal static ContainerBuilder Configure(ContainerBuilder cb)
        {
            var host = ConfigurationManager.AppSettings["TvMazeApiEndpoint"];
            cb.Register(c => new TvMazeApi(host)).As<ITvMazeApi>().SingleInstance();

            cb.RegisterType<ShowsSynchronizationService>().As<IShowsSynchronizationService>();

            var connectionString = ConfigurationManager.AppSettings["MongoDbConnectionString"];
            var databaseName = ConfigurationManager.AppSettings["TvMazeDbName"];
            cb.Register(c => new MongoClient(connectionString)).As<MongoClient>();
            cb.Register(c => new DbContext(c.Resolve<MongoClient>(), databaseName)).As<IDbContext>();
            cb.RegisterType<ShowsRepository>().As<IShowsRepository>();
            cb.RegisterType<CancellationTokenSource>().AsSelf().SingleInstance();

            return cb;
        }

    }
}
