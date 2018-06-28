using System;

namespace EPAM.TvMaze.RestApi.ViewModels
{
    public class PersonViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? BirthDay { get; set; }
    }
}
