using System;
using EPAM.TvMaze.Contracts;
using MongoDB.Driver;

namespace EPAM.TvMaze.DAL.Shared
{
    public class DbContext : IDbContext
    {
        private readonly MongoClient _mongoClient;
        private readonly string _databaseName;

        public DbContext(MongoClient mongoClient, string databaseName)
        {
            _mongoClient = mongoClient;
            _databaseName = databaseName;
        }

        public IMongoCollection<ShowEntity> GetShowsCollection()
        {
            return _mongoClient.GetDatabase(_databaseName).GetCollection<ShowEntity>("Shows");
        }
    }
}
