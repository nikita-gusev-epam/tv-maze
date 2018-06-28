using System;

namespace EPAM.TvMaze.Contracts
{
    public class PersonEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? BirthDay { get; set; }
    }
}
