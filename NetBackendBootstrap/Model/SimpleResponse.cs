using Newtonsoft.Json;

namespace NetBackendBootstrap.Model
{
    /// <summary>
    /// Simple model representing a string API message response
    /// </summary>
    public class SimpleResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        public SimpleResponse(string message)
        {
            Message = message;
        }
    }
}
