using Newtonsoft.Json;

namespace EPAM.TvMaze.Scrapper.Contracts.Models
{
    public class Person
    {
        [JsonProperty("birthday")]
        public string BirthDay { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
