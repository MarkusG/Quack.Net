using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json.Linq;

namespace DuckDuckGo
{
    public class DuckDuckGoClient
    {
        private static HttpClient _client;
        private string _appName;

        /// <summary>
        /// Gets an instant answer for the given parameters
        /// </summary>
        /// <param name="query">Search query</param>
        /// <param name="noHtml">Remove HTML from text</param>
        /// <param name="skipDisambiguation">Skip Disambiguations</param>
        public async Task<InstantAnswer> GetInstantAnswerAsync(string query, bool noHtml = false, bool skipDisambiguation = false)
        {
            var queryBuilder = new StringBuilder("?q=" + query);
            if (_appName != null)
                queryBuilder.Append($"&t={_appName}");
            if (noHtml)
                queryBuilder.Append("&no_html=1");
            if (skipDisambiguation)
                queryBuilder.Append("&skip_disambig=1");
            queryBuilder.Append("&format=json");
            queryBuilder.Append("&no_redirect=1");
            var response = await _client.GetAsync(queryBuilder.ToString()).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var jObj = JObject.Parse(json);
            if ((string)jObj["Type"] == "D") return new DisambiguationAnswer(jObj);
            else return new InstantAnswer(jObj);
        }

        /// <summary>
        /// Gets the !bang redirect URL for the given query
        /// </summary>
        /// <param name="query">Search query</param>
        /// <returns>!bang redirect URL</returns>
        public async Task<string> GetBangRedirectUrlAsync(string query)
        {
            var queryBuilder = new StringBuilder("?q=" + query);
            if (_appName != null)
                queryBuilder.Append($"&t={_appName}");
            queryBuilder.Append("&format=json");
            queryBuilder.Append("&no_redirect=1");
            var response = await _client.GetAsync(queryBuilder.ToString()).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var jObj = JObject.Parse(json);
            var answer = new InstantAnswer(jObj);
            return answer.RedirectUrl;
        }

        public DuckDuckGoClient(string appName)
        {
            if (_client == null)
            {
                _client = new HttpClient()
                {
                    BaseAddress = new Uri("http://api.duckduckgo.com/")
                };
            }
            _appName = appName;
        }
    }
}
