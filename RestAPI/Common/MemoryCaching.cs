using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace RestAPI.Common
{
    public class MemoryCaching
    {
        // We hold an instance of IMemoryCache to perform actual caching operations.
        private readonly IMemoryCache _cache;

        // We use a thread-safe ConcurrentDictionary to track cache keys.
        private readonly ConcurrentDictionary<string, bool> _cacheKeys;

        // The constructor receives an IMemoryCache from DI.
        public MemoryCaching(IMemoryCache cache)
        {
            _cache = cache;
            _cacheKeys = new ConcurrentDictionary<string, bool>();
        }

        // Adds a cache entry and tracks its key in our ConcurrentDictionary.
        // options can define expiration strategies, priority, etc.
        public void Set<T>(string key, T value, MemoryCacheEntryOptions options)
        {
            // Store the item in the IMemoryCache with the specified options
            _cache.Set(key, value, options);

            // Track the key in our dictionary (the bool value here is unused, but needed by ConcurrentDictionary)
            _cacheKeys.TryAdd(key, true);
        }

        // Attempts to retrieve a cache entry.
        // If the key exists in the IMemoryCache, returns true along with the value.
        // Otherwise, removes it from our dictionary.
        public bool IsExist<T>(string key, out T? value)
        {
            if (_cache.TryGetValue(key, out value))
            {
                return true;
            }

            // If not found in the cache, remove from the dictionary
            _cacheKeys.TryRemove(key, out _);
            value = default;
            return false;
        }

        // New: Retrieves a cache entry value or default if not present.
        // This is a simple getter variant that returns the value (or null/default)
        // and ensures our tracking dictionary stays in sync.
        public T? Get<T>(string key)
        {
            if (_cache.TryGetValue(key, out T? value))
            {
                return value;
            }

            // If not found, remove from tracking and return default
            _cacheKeys.TryRemove(key, out _);
            return default;
        }

        // Removes a cache entry from both IMemoryCache and our dictionary.
        public void Remove(string key)
        {
            _cache.Remove(key);
            _cacheKeys.TryRemove(key, out _);
        }

        // Returns all currently known (tracked) cache keys.
        // Note: This might include keys that recently expired, so you may want to
        // re-check each key in IMemoryCache if you want only actively stored ones.
        public List<string> GetAllKeys()
        {
            return _cacheKeys.Keys.ToList();
        }

        // Clears all cache entries from IMemoryCache and resets our dictionary.
        public void Clear()
        {
            foreach (var key in _cacheKeys.Keys)
            {
                _cache.Remove(key);
            }

            _cacheKeys.Clear();
        }
        public void Clear(string key)
        {
            
            _cache.Remove(key);
            _cacheKeys.Clear();
        }

    }
}

