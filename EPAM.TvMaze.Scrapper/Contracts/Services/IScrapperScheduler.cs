using System;
using Quartz;

namespace EPAM.TvMaze.Scrapper.Contracts.Services
{
    public interface IScrapperScheduler:IDisposable
    {
        void Schedule<T>(string cronExpression) where T : IJob;
        void Start();
    }
}
