using System.Threading.Tasks;

namespace EPAM.TvMaze.Scrapper.Contracts.Services
{
    public interface IShowsSynchronizationService
    {
        Task SynchronizeAsync();
    }
}
