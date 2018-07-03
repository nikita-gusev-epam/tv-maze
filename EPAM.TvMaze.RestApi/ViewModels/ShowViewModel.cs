using System.Collections.Generic;

namespace EPAM.TvMaze.RestApi.ViewModels
{
    public class ShowViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<PersonViewModel> Casts { get; set; }
    }
}
