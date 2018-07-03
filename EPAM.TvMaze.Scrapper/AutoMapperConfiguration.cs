using AutoMapper;
using EPAM.TvMaze.Contracts;
using EPAM.TvMaze.Scrapper.Contracts.Models;

namespace EPAM.TvMaze.Scrapper
{
    public class AutoMapperConfiguration
    {
        public static void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Show, ShowEntity>();
                cfg.CreateMap<Cast, PersonEntity>();
            });
        }
    }
}