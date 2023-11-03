using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Services.Common.Interface;

namespace Extention
{
    public class CacheService : ICacheService
    {
        
        private readonly IMemoryCache _memoryCache;

        public CacheService( IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public T GetData<T>(string key)
        {
            try
            {
                T item = (T)_memoryCache.Get(key);
                return item;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool SetData<T>(string key, T value, int expirationTimeinMinutes)
        {
            bool res = true;
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(expirationTimeinMinutes))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(expirationTimeinMinutes+1));
                    _memoryCache.Set(key, value, cacheEntryOptions);
                }
                return true;
            }
            catch (Exception e)
            {
                throw;
                res = false;
            }
            return res;
        }
        //public bool SetData(string key, string value, int expirationTimeinMinutes)
        //{
        //    bool res = true;
        //    try
        //    {
        //        var cacheEntryOptions = new MemoryCacheEntryOptions()
        //           .SetSlidingExpiration(TimeSpan.FromMinutes(expirationTimeinMinutes - 1))
        //           .SetAbsoluteExpiration(TimeSpan.FromMinutes(expirationTimeinMinutes));
        //        _memoryCache.Set(key, value, cacheEntryOptions);
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //    return res;
        //}
        public bool RemoveData(string key)
        {
            try
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                   .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                   .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
                _memoryCache.Set(key, "", cacheEntryOptions);
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
            return false;
        }
    }
}
