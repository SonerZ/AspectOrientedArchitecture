using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using Application.Packages.Caching.Core.Service;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Packages.Caching.InMemory.Service;

    public class InMemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Add(string key, object data, int timeOut)
        {
            _memoryCache.Set(key, data, TimeSpan.FromMinutes(timeOut));
        }

        public T Get<T>(string key)
        {
            _memoryCache.TryGetValue(key, out T returnData);
            return returnData;
        }

        public void Remove(string key)
        {
            if (Any(key))
            {
                _memoryCache.Remove(key);
            }
        }
        
        public void RemoveByPattern(string pattern)
        {
            var fieldInfo = typeof(MemoryCache).GetField("_coherentState", BindingFlags.Instance | BindingFlags.NonPublic);
           
            var propertyInfo = fieldInfo.FieldType.GetProperty("EntriesCollection", BindingFlags.Instance | BindingFlags.NonPublic);

            var value = fieldInfo.GetValue(_memoryCache);
            var dict = propertyInfo.GetValue(value);
            var cacheEntries = dict as IDictionary;

            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            foreach (var cacheEntry in cacheEntries)
            {
                var key = cacheEntry.GetType().GetProperty("Key").GetValue(cacheEntry).ToString();

                if (regex.IsMatch(key))
                {
                    Remove(key);
                }
            }
        }

        public bool Any(string key)
        {
            return _memoryCache.TryGetValue(key, out object data);
        }
    }

