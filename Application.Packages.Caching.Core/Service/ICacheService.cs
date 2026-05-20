namespace Application.Packages.Caching.Core.Service
{
    /// <summary>
    /// Cache service interface provides some operations for caching.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Insert data to cache.
        /// </summary>
        /// <param name="key">Unique key name</param>
        /// <param name="data">Data</param>
        void Add(string key, object data, int timeOut = 100);
        /// <summary>
        /// Get data from cache.
        /// </summary>
        /// <typeparam name="T">Return element type</typeparam>
        /// <param name="key">Unique key name</param>
        /// <returns></returns>
        T Get<T>(string key);
        /// <summary>
        /// Remove element from cache.
        /// </summary>
        /// <param name="key">Unique key name</param>
        void Remove(string key);

        void RemoveByPattern(string pattern);
        /// <summary>
        /// Check data in cache.
        /// </summary>
        /// <returns></returns>
        bool Any(string key);
    }
}
