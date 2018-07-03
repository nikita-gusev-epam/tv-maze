using System;
using System.Threading.Tasks;
using EPAM.TvMaze.Contracts;
using EPAM.TvMaze.DAL.Shared;
using EPAM.TvMaze.Scrapper.Contracts.Services;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EPAM.TvMaze.Scrapper.Services
{
    public class ShowsRepository : IShowsRepository
    {
        private readonly IDbContext _context;

        public ShowsRepository(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddNewShowAsync(ShowEntity show)
        {
            await _context.GetShowsCollection().InsertOneAsync(show);
        }

        public async Task<int> GetMaxShowIdAsync()
        {
            var maxShowId = await _context.GetShowsCollection()
                .AsQueryable()
                .OrderByDescending(p => p.Id)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();
            return maxShowId;
        }
    }
}
