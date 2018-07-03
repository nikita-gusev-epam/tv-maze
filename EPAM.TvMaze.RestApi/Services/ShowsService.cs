using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EPAM.TvMaze.RestApi.Contracts;
using EPAM.TvMaze.RestApi.ViewModels;

namespace EPAM.TvMaze.RestApi.Services
{
    public class ShowsService : IShowsService
    {
        private readonly IShowsRepository _showsRepository;

        public ShowsService(IShowsRepository showsRepository)
        {
            _showsRepository = showsRepository ?? throw new ArgumentNullException(nameof(showsRepository));
        }

        public async Task<List<ShowViewModel>> GetShowsAsync(int pageNumber, int pageSize)
        {
            var skipCount = pageNumber * pageSize;
            var showEntities = await _showsRepository.GetShowsAsync(skipCount, pageSize);
            var shows = Mapper.Map<List<ShowViewModel>>(showEntities);
            shows.ForEach(show => { show.Casts = show.Casts.OrderByDescending(p => p.BirthDay).ToList(); });
            return shows;
        }
    }
}
