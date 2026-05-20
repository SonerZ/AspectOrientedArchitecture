using System.Collections.Generic;

namespace Application.Core.Utilities.Result
{
    public class Result : IResult
    {
        public Result()
        {

        }

        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
            Messages = new List<KeyValuePair<string, string>>();
        }

        public Result(bool isSuccess, string key, string message) : this(isSuccess)
        {
            Messages.Add(new KeyValuePair<string, string>(key, message));
        }  
        
        public Result(bool isSuccess, string message) : this(isSuccess)
        {
            Messages.Add(new KeyValuePair<string, string>(string.Empty, message));
        }
        
        public Result(bool isSuccess, List<KeyValuePair<string, string>> messages) : this(isSuccess)
        {
            Messages.AddRange(messages);
        }

       
        public void AddMessage(string key, string message)
        {
            Messages.Add(new KeyValuePair<string, string>(key, message));
        }
        public void AddMessages(List<KeyValuePair<string, string>> messages)
        {
            Messages.AddRange(messages);
        }
        public bool IsSuccess { get; set; }
        public List<KeyValuePair<string, string>> Messages { get; set; }
    }
}
