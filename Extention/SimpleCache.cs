using Microsoft.Extensions.Caching.Memory;

namespace Extention
{
    public class SimpleCache
    {
        private readonly MemoryCache cache;

        public SimpleCache() : this(1024) { }

        public SimpleCache(int sizeLimit)
        {
            cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = sizeLimit,
            });
        }


        protected virtual bool IsItemEmpty(string val)
        {
            return val == null;
        }

        public async Task<string> Get(string key)
        {
            var item = cache.Get<string>(key);
            if (!IsItemEmpty(item))
            {
                return item;
            }
            return null;
        }

        public async Task<string> Add(string key, string value,int Expireinminits)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                cache.Set
                    (
                        key, value, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(Expireinminits))
                    );
                return value;
            }
            return null;
        }
    }
}