using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPAM.TvMaze.RestApi.Contracts;
using EPAM.TvMaze.RestApi.ViewModels;

namespace EPAM.TvMaze.RestApi.Services
{
    public class ShowsService:IShowsService
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
            var shows = showEntities.Select(show => new ShowViewModel
            {
                Id = show.Id,
                Name = show.Name,
                Casts = show.Casts.Select(cast => new PersonViewModel
                    {
                        Id = cast.Id,
                        Name = cast.Name,
                        BirthDay = cast.BirthDay
                    }).OrderByDescending(p => p.BirthDay)
                    .ToList()
            }).ToList();

            return shows;
        }
    }
}
