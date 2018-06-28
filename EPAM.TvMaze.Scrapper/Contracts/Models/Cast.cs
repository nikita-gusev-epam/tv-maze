using Newtonsoft.Json;

namespace EPAM.TvMaze.Scrapper.Contracts.Models
{
    public class Cast
    {
        [JsonProperty("person")]
        public Person Person { get; set; }
    }
}
