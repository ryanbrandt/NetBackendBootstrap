using System.Collections.Generic;
using NetBackendBootstrap.Enum;
using NetBackendBootstrap.Interface;
using NetBackendBootstrap.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetBackendBootstrap.Model
{
    /// <summary>
    /// Simple implementation of IResponse
    /// Serializes both ResponseStatus values and JArray values
    /// </summary>
    public class Response : IResponse
    {
        public ResponseStatus Status { get; set; }

        public string Body { get; set; }

        public static Response FromList<T>(IList<T> results, ResponseStatus status = ResponseStatus.OK) where T : new()
        {
            var serializedResults = JsonExtensions.FromList(results);
            return new Response(serializedResults, status);
        }

        public Response(JArray results, ResponseStatus status = ResponseStatus.OK)
        {
            Status = status;
            Body = JsonConvert.SerializeObject(new BodyResponse(results));
        }

        public Response(ResponseStatus status, string message = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = EnumExtensions.ExtractDescription(status);
            }
            Status = status;
            Body = JsonConvert.SerializeObject(new SimpleResponse(message));
        }

        public override string ToString()
        {
            return $"Status: {Status}, Body: {Body}";
        }
    }
}
