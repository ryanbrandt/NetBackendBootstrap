using System.ComponentModel;
using System.Net;

namespace NetBackendBootstrap.Enum
{
    /// <summary>
    /// enum containing standard response messages associated with different ResponseStatus values
    /// </summary>
    public enum ResponseStatus
    {
        [Description("Request success")]
        OK = HttpStatusCode.OK,

        [Description("Unauthorized")]
        Unauthorized = HttpStatusCode.Unauthorized,

        [Description("Bad request")]
        BadRequest = HttpStatusCode.BadRequest,

        [Description("Not found")]
        NotFound = HttpStatusCode.NotFound,

        [Description("Successfully created")]
        Created = HttpStatusCode.Created,

        [Description("")]
        NoContent = HttpStatusCode.NoContent,

        [Description("An unexpected error has occurred")]
        InternalServerError = HttpStatusCode.InternalServerError,
    }
}
