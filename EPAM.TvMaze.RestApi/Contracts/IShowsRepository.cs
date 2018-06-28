using System.Collections.Generic;
using System.Threading.Tasks;
using EPAM.TvMaze.Contracts;

namespace EPAM.TvMaze.RestApi.Contracts
{
    public interface IShowsRepository
    {
        Task<List<ShowEntity>> GetShowsAsync(int skipCount, int takeCount);
    }
}
