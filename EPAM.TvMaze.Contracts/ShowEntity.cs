using System.Collections.Generic;

namespace EPAM.TvMaze.Contracts
{
    public class ShowEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<PersonEntity> Casts { get; set; }
    }
}
