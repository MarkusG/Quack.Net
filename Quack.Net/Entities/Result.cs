using Newtonsoft.Json;

namespace DuckDuckGo
{
    /// <summary>
    /// External link associated with an abstract
    /// </summary>
    public class Result
    {
        /// <summary>
        /// HTML link to external site
        /// </summary>
        [JsonProperty("Result")]
        public string ResultText { get; set; }

        /// <summary>
        /// First URL in ResultText
        /// </summary>
        [JsonProperty("FirstURL")]
        public string FirstUrl { get; set; }

        /// <summary>
        /// Icon associated with FirstUrl
        /// </summary>
        public Icon Icon { get; set; }

        /// <summary>
        /// Text from FirstUrl
        /// </summary>
        public string Text { get; set; }
    }
}
