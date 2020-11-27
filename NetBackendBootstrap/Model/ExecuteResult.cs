using System;

namespace NetBackendBootstrap.Model
{
    /// <summary>
    /// Model used to represent the result of IDatabaseHandler.Execute
    /// Extends OperationResult with an integer property RowsAffected
    /// representing the number of rows affected by a successful Execute operation
    /// </summary>
    public class ExecuteResult: OperationResult
    {
        public int RowsAffected { get; set; }

        public ExecuteResult()
            : base() { }

        public ExecuteResult(int rowsAffected) : base(true)
        {
            RowsAffected = rowsAffected;
        }

        public ExecuteResult(Exception error)
            : base(error) { }
    }
}
