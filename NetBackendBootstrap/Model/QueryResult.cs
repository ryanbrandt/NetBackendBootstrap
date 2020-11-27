using System;
using System.Collections.Generic;

namespace NetBackendBootstrap.Model
{
    /// <summary>
    /// Model used to represent the result of IDatabaseHandler.Query
    /// Extends OperationResult with an IList of type T for query results 
    /// in the case of a successful Query operation
    /// </summary>
    public class QueryResult<T> : OperationResult
    {
        public IList<T> Data { get; set; }

        public QueryResult()
            : base() { }

        public QueryResult(IList<T> data) : base(true)
        {
            data = Data;
        }

        public QueryResult(Exception error) 
            : base(error) { }
    }
}
