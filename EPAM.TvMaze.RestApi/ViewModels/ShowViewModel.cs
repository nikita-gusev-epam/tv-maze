using System.Collections.Generic;

namespace EPAM.TvMaze.RestApi.ViewModels
{
    public class ShowViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<PersonViewModel> Casts { get; set; }
    }
}
