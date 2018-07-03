using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPAM.TvMaze.Contracts;
using EPAM.TvMaze.DAL.Shared;
using EPAM.TvMaze.RestApi.Contracts;
using MongoDB.Driver;

namespace EPAM.TvMaze.RestApi.Services
{
    public class ShowsRepository : IShowsRepository
    {
        private readonly IDbContext _context;

        public ShowsRepository(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<ShowEntity>> GetShowsAsync(int skipCount, int takeCount)
        {
            var shows = await _context.GetShowsCollection()
                .Find(p => true)
                .Skip(skipCount)
                .Limit(takeCount)
                .ToListAsync();
            return shows;
        }
    }
}
