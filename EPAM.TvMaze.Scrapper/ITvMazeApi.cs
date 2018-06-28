using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPAM.TvMaze.Scrapper
{
    public interface ITvMazeApi
    {
        Task<IEnumerable<Show>> GetShowsAsync(int page);
        Task<IEnumerable<Cast>> GetCastAsync(int showId);
    }
}