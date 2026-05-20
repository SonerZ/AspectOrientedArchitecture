namespace Application.Core.Utilities.Result;

    /// <summary>
    /// IDataResult provides use data with IResult.
    /// </summary>
    /// <typeparam name="T">Return data type.</typeparam>
    public interface IDataResult<T> : IResult
    {
        /// <summary>
        /// Return data.
        /// </summary>
        T Data { get; set; }
        
        /// <summary>
        /// is cache data ?
        /// </summary>
        bool IsCache { get; set; }
    }

