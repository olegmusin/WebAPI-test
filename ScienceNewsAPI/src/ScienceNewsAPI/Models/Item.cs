using Newtonsoft.Json;
using System;

namespace ScienceNewsAPI.Models
{
    public class Item
    {
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore, Required = Required.Default)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public DateTime PubDate { get; set; }
        public string Thumbnail { get; set; }
    }

}
