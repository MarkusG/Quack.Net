using Newtonsoft.Json;

namespace DuckDuckGo
{
    /// <summary>
    /// A topic related to an abstract
    /// </summary>
    public class Topic
    {
        /// <summary>
        /// HTML link to related topic
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// First URL in Result
        /// </summary>
        [JsonProperty("FirstURL")]
        public string FirstUrl { get; set; }

        /// <summary>
        /// Icon associated with related topic
        /// </summary>
        public Icon Icon { get; set; }

        /// <summary>
        /// Text from the first URL
        /// </summary>
        public string Text { get; set; }
    }
}
