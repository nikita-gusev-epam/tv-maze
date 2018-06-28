using Newtonsoft.Json;

namespace EPAM.TvMaze.Scrapper
{
    public class Cast
    {
        [JsonProperty("person")]
        public Person Person { get; set; }
    }
}