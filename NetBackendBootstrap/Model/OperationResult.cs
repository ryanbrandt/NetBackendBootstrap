using System;

namespace NetBackendBootstrap.Model
{
    /// <summary>
    /// Generic abstract class containing common properties for any operation
    /// which may succeed or fail and detail an error in the failure case
    /// </summary>
    public abstract class OperationResult
    {
        public bool Success { get; set; }

        public Exception Error { get; set; }

        public OperationResult(bool success = false, Exception error = null)
        {
            Success = success;
            Error = error;
        }

        public OperationResult(bool success)
            : this(success, null) { }

        public OperationResult(Exception error)
            : this(false, error) { }
    }
}
