using System.Collections.Generic;
using System.Threading.Tasks;
using EPAM.TvMaze.Scrapper.Contracts.Models;

namespace EPAM.TvMaze.Scrapper.Contracts.Services
{
    public interface ITvMazeApi
    {
        Task<IEnumerable<Show>> GetShowsAsync(int page);
        Task<IEnumerable<Cast>> GetCastAsync(int showId);
    }
}
