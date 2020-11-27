using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetBackendBootstrap.Model
{
    /// <summary>
    /// Simple model representing an API JSON body
    /// </summary>
    public class BodyResponse
    {
        [JsonProperty("results")]
        public JArray Results { get; set; }

        public BodyResponse(JArray results)
        {
            Results = results;
        }
    }
}
