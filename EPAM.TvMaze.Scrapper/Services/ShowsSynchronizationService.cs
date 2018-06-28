using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EPAM.TvMaze.Contracts;
using EPAM.TvMaze.Scrapper.Contracts.Models;
using EPAM.TvMaze.Scrapper.Contracts.Services;
using Serilog;

namespace EPAM.TvMaze.Scrapper.Services
{
    public class ShowsSynchronizationService : IShowsSynchronizationService
    {
        private static readonly ILogger Logger = Log.ForContext<ShowsSynchronizationService>();

        private readonly IShowsRepository _showsRepository;
        private readonly ITvMazeApi _tvMazeApi;
        private readonly CancellationTokenSource _cts;

        public ShowsSynchronizationService(IShowsRepository showsRepository, ITvMazeApi tvMazeApi, CancellationTokenSource cts)
        {
            _showsRepository = showsRepository ?? throw new ArgumentNullException(nameof(showsRepository));
            _tvMazeApi = tvMazeApi;
            _cts = cts;
        }

        public async Task SynchronizeAsync()
        {
            Logger.Information("Starting shows synchronization");
            const int pageSize = 250;

            var latestShowId = await _showsRepository.GetMaxShowIdAsync();
            var currentPage = latestShowId / pageSize;

            List<Show> newShows;
            do
            {
                newShows = (await _tvMazeApi.GetShowsAsync(currentPage)).ToList();

                var newShowsToAdd = newShows.Where(p => p.Id > latestShowId);
                foreach (var newShow in newShowsToAdd)
                {
                    var showCasts = await _tvMazeApi.GetCastAsync(newShow.Id);

                    var showEntity = MapShowEntity(newShow, showCasts);

                    await _showsRepository.AddNewShowAsync(showEntity);

                    if (_cts.IsCancellationRequested)
                    {
                        break;
                    }
                }

                if (_cts.IsCancellationRequested)
                {
                    break;
                }

                currentPage++;
            } while (newShows.Any());

            Logger.Information("Finished shows synchronization");
        }

        private static ShowEntity MapShowEntity(Show newShow, IEnumerable<Cast> showCasts)
        {
            var showEntity = new ShowEntity
            {
                Id = newShow.Id,
                Name = newShow.Name,
                Casts = showCasts.Select(cast => new PersonEntity
                {
                    Id = cast.Person.Id,
                    Name = cast.Person.Name,
                    BirthDay = cast.Person.BirthDay
                }).ToList()
            };
            return showEntity;
        }
    }
}
