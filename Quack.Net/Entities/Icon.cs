using Newtonsoft.Json;

namespace DuckDuckGo
{
    public class Icon
    {
        [JsonProperty("URL")]
        public string Url { get; set; }

        public int? Height { get; set; }

        public int? Width { get; set; }
    }
}
