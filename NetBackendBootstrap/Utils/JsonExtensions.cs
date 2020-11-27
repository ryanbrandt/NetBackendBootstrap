using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace NetBackendBootstrap.Utils
{
    /// <summary>
    /// Utility class for JSON/Newtonsoft.Json extension methods
    /// </summary>
    public static class JsonExtensions
    {
        public static JObject FromObject<T>(object o) where T : new()
        {
            JObject jObj = (JObject)JToken.FromObject(o);
            return jObj;
        }

        public static JArray FromList<T>(IList<T> l) where T : new()
        {
            JArray jArr = (JArray)JToken.FromObject(l);
            return jArr;
        }
    }
}
