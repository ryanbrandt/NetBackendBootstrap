using NetBackendBootstrap.Enum;

namespace NetBackendBootstrap.Interface
{
    /// <summary>
    /// Interface representing a generic API response
    /// </summary>
    public interface IResponse
    {
        ResponseStatus Status { get; set; }

        string Body { get; set; }
    }
}
