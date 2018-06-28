using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPAM.TvMaze.Contracts;
using EPAM.TvMaze.RestApi.Contracts;
using MongoDB.Driver;

namespace EPAM.TvMaze.RestApi.Services
{
    public class ShowsRepository : IShowsRepository
    {
        private readonly IMongoCollection<ShowEntity> _showsCollection;

        public ShowsRepository(IMongoCollection<ShowEntity> showsCollection)
        {
            _showsCollection = showsCollection ?? throw new ArgumentNullException(nameof(showsCollection));
        }

        public Task<List<ShowEntity>> GetShowsAsync(int skipCount, int takeCount)
        {
            var shows = _showsCollection
                .AsQueryable()
                .Skip(skipCount)
                .Take(takeCount)
                .ToList();
            return Task.FromResult(shows);
        }
    }
}
