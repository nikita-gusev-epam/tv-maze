using System.Threading.Tasks;
using EPAM.TvMaze.Contracts;

namespace EPAM.TvMaze.Scrapper.Contracts.Services
{
    public interface IShowsRepository
    {
        Task<int> GetMaxShowIdAsync();

        Task AddNewShowAsync(ShowEntity show);
    }
}
