using EPAM.TvMaze.Contracts;
using MongoDB.Driver;

namespace EPAM.TvMaze.DAL.Shared
{
    public interface IDbContext
    {
        IMongoCollection<ShowEntity> GetShowsCollection();
    }
}