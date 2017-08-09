using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DuckDuckGo
{
    /// <summary>
    /// Represents an instant answer
    /// </summary>
    public class InstantAnswer
    {
        private List<Topic> _topics;
        internal readonly JObject JObject;

        /// <summary>
        /// Topic summary (can contain HTML)
        /// </summary>
        public string Abstract { get; set; }

        /// <summary>
        /// Topic summary (no HTML)
        /// </summary>
        public string AbstractText { get; set; }

        /// <summary>
        /// Name of Abstract source
        /// </summary>
        public string AbstractSource { get; set; }

        /// <summary>
        /// Deep link to expanded topic page in AbstractSource
        /// </summary>
        [JsonProperty("AbstractURL")]
        public string AbstractUrl { get; set; }

        /// <summary>
        /// URL of the image associated with Abstract
        /// </summary>
        [JsonProperty("Image")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Name of topic associated with Abstract
        /// </summary>
        public string Heading { get; set; }

        /// <summary>
        /// Instant answer
        /// </summary>
        public string Answer { get; set; } // TODO find out what this is

        /// <summary>
        /// Answer type
        /// </summary>
        public string AnswerType { get; set; } // TODO find a list of these and make an enum

        /// <summary>
        /// Dictionary definition
        /// </summary>
        public string Definition { get; set; }

        /// <summary>
        /// Name of Definition source
        /// </summary>
        public string DefinitionSource { get; set; }

        /// <summary>
        /// Deep link to expanded definition page in DefinitionSource
        /// </summary>
        [JsonProperty("DefinitionURL")]
        public string DefinitionUrl { get; set; }

        public string Entity { get; set; } // TODO find out what this is and add a docstring for it

        /// <summary>
        /// Array of external links associated with Abstract
        /// </summary>
        public Result[] Results { get; set; }

        /// <summary>
        /// Response category
        /// </summary>
        public InstantAnswerType Type { get; set; }

        /// <summary>
        /// A collection of topics related to the query
        /// </summary>
        public IEnumerable<Topic> Topics => _topics;

        /// <summary>
        /// !bang redirect URL
        /// </summary>
        /// <seealso cref="DuckDuckGoClient.BangAsync(string)">
        /// Use DuckDuckGoClient.BangAsync(string) for !bang queries
        /// </seealso>
        [JsonProperty("Redirect")]
        public string RedirectUrl { get; set; }

        internal InstantAnswer(JObject jObj)
        {
            _topics = new List<Topic>();
            JObject = jObj;
            var topics = JObject["RelatedTopics"];
            for (int i = 0; i < topics.Count(); i++)
            {
                if (topics[i]["Topics"] == null) // Element is not a Disambiguation group
                    _topics.Add(topics[i].ToObject<Topic>());
            }
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new InstantAnswerTypeConverter());
            JsonConvert.PopulateObject(jObj.ToString(), this, settings);
            if (Topics != null)
            {
                foreach (var t in _topics)
                {
                    if (string.IsNullOrEmpty(t.Icon.Url))
                        t.Icon = null;
                }
            }
            if (Results != null)
            {
                for (int i = 0; i < Results.Length; i++)
                {
                    if (string.IsNullOrEmpty(Results[i].Icon.Url))
                        Results[i].Icon = null;
                }
            }
        }
    }
}
