using Newtonsoft.Json;

namespace EPAM.TvMaze.Scrapper
{
    public class Show
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}