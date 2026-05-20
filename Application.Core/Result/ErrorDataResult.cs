using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Core.Utilities.Result
{
    /// <summary>
    /// Error result for unsuccessful method.
    /// </summary>
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data) : base(data, false)
        {

        }
        
        public ErrorDataResult(string key, string message) : base(false, key ,message)
        {
            
        }

        public ErrorDataResult(string message) : base(false, message)
        {

        }

        public ErrorDataResult(T data, string key, string message) : base(data, false, key ,message)
        {

        }

        public ErrorDataResult(T data, List<KeyValuePair<string, string>> messages) : base(data,false, messages)
        {

        } 
        
        public ErrorDataResult(List<KeyValuePair<string, string>> messages) : base(false, messages)
        {

        }
    }
}
