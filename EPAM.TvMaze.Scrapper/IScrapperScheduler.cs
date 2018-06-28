using System;
using Quartz;

namespace EPAM.TvMaze.Scrapper
{
    public interface IScrapperScheduler:IDisposable
    {
        void Schedule<T>(string cronExpression) where T : IJob;
        void Start();
    }
}
