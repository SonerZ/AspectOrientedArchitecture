using System.Collections.Generic;

namespace Application.Core.Utilities.Result;

    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, bool isSuccess) : base(isSuccess)
        {
            Data = data;
        }

        public DataResult(T data, bool isSuccess, string key, string message) : base(isSuccess, key, message)
        {
            Data = data;
        }
        
        public DataResult(bool isSuccess, string key, string message) : base(isSuccess, key, message)
        {
          
        }

        public DataResult(bool isSuccess, string message) : base(isSuccess,  message)
        {

        }

        public DataResult(T data , bool isSuccess, List<KeyValuePair<string, string>> messages) : base( isSuccess, messages)
        {
            Messages.AddRange(messages);
        }
        
        public DataResult(bool isSuccess, List<KeyValuePair<string, string>> messages) : base( isSuccess, messages)
        {
            Messages.AddRange(messages);
        }
        
        public DataResult(T data)
        {

        }

        public DataResult()
        {
            
        }

        public T Data { get; set; }
        public bool IsCache { get; set; }
    }

