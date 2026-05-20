using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Core.Utilities.Result
{
    /// <summary>
    /// Success result for successfully done method.
    /// </summary>
    public class SuccessResult : Result
    {
        public SuccessResult() : base(true)
        {

        }
        public SuccessResult(string key, string message) : base(true,  key, message)
        {
            
        }
    }
}
