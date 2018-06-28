using System.Threading.Tasks;

namespace EPAM.TvMaze.Scrapper
{
    public interface IShowsSynchronizationService
    {
        Task SynchronizeAsync();
    }
}