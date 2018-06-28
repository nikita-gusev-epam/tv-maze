using System;
using Newtonsoft.Json;

namespace EPAM.TvMaze.Scrapper
{
    public class Person
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("birthday")]
        public DateTime? BirthDay { get; set; }
    }
}