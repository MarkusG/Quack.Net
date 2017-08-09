using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DuckDuckGo
{
    /// <summary>
    /// Represents an instant answer with a collection of Disambiguation groups
    /// </summary>
    public class DisambiguationAnswer : InstantAnswer
    {
        private List<DisambiguationGroup> _DisambiguationGroups;

        public IEnumerable<DisambiguationGroup> DisambiguationGroups => _DisambiguationGroups;

        internal DisambiguationAnswer(JObject jObj) : base(jObj)
        {
            var topics = JObject["RelatedTopics"];
            var count = topics.Count();
            if (count == 0) return;
            _DisambiguationGroups = new List<DisambiguationGroup>();
            for (int i = 0; i < count; i++)
            {
                if (topics[i]["Topics"] != null) // Element is a Disambiguation group
                    _DisambiguationGroups.Add(topics[i].ToObject<DisambiguationGroup>());
            }
        }
    }
}
