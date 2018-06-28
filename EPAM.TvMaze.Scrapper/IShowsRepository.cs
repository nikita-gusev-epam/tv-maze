using System.Threading.Tasks;

namespace EPAM.TvMaze.Scrapper
{
    public interface IShowsRepository
    {
        Task<int> GetMaxShowIdAsync();

        Task AddNewShowAsync(ShowEntity show);
    }
}