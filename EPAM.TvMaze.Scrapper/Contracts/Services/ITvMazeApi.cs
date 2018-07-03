using System.Collections.Generic;
using System.Threading.Tasks;
using EPAM.TvMaze.Scrapper.Contracts.Models;

namespace EPAM.TvMaze.Scrapper.Contracts.Services
{
    public interface ITvMazeApi
    {
        Task<List<Show>> GetShowsAsync(int page);
        Task<List<Cast>> GetCastAsync(int showId);
    }
}
