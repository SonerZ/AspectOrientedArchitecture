using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Core.Utilities.Result
{
    /// <summary>
    /// Error result for unsuccessful method.
    /// </summary>
    public class ErrorResult : Result
    {
        public ErrorResult() : base(false)
        {

        }

        public ErrorResult(string key, string message) : base(false, key, message)
        {
            
        }

        public ErrorResult(string message) : base(false, message)
        {

        }

        public ErrorResult(List<KeyValuePair<string,string>> messages) : base(false, messages)
        {
            
        }
    }
}
