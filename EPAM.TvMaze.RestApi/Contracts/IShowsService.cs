using System.Collections.Generic;
using System.Threading.Tasks;
using EPAM.TvMaze.RestApi.ViewModels;

namespace EPAM.TvMaze.RestApi.Contracts
{
    public interface IShowsService
    {
        Task<List<ShowViewModel>> GetShowsAsync(int pageNumber, int pageSize);
    }
}
