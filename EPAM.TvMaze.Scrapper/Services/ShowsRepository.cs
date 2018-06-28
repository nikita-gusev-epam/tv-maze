using System;
using System.Threading.Tasks;
using EPAM.TvMaze.Contracts;
using EPAM.TvMaze.Scrapper.Contracts.Services;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EPAM.TvMaze.Scrapper.Services
{
    public class ShowsRepository: IShowsRepository
    {
        private readonly IMongoCollection<ShowEntity> _showsCollection;

        public ShowsRepository(IMongoCollection<ShowEntity> showsCollection)
        {
            _showsCollection = showsCollection ?? throw new ArgumentNullException(nameof(showsCollection));
        }

        public async Task<int> GetMaxShowIdAsync()
        {
            var maxShowId = await _showsCollection
                .AsQueryable()
                .OrderByDescending(p => p.Id)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();
            return maxShowId;
        }

        public async Task AddNewShowAsync(ShowEntity show)
        {
            await _showsCollection.InsertOneAsync(show);
        }
    }
}
