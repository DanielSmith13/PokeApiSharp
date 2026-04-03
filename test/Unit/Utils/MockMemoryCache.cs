using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace Unit.Utils;

public class MockMemoryCache : IMemoryCache
    {
        private readonly Dictionary<object, object?> _store = new();
        public TimeSpan LastAbsoluteExpirationRelativeToNow { get; private set; }

        public bool TryGetValue(object key, out object? value)
        {
            if (_store.TryGetValue(key, out var v))
            {
                value = v;
                return true;
            }
            value = null;
            return false;
        }

        public ICacheEntry CreateEntry(object key)
        {
            return new MockCacheEntry(this, key);
        }

        public void Remove(object key)
        {
            _store.Remove(key);
        }

        public void Dispose()
        {
            _store.Clear();
        }

        private class MockCacheEntry(MockMemoryCache owner, object key) : ICacheEntry
        {
            public object Key { get; } = key;
            public object? Value { get; set; }
            public DateTimeOffset? AbsoluteExpiration { get; set; }

            public TimeSpan? AbsoluteExpirationRelativeToNow
            {
                get;
                set
                {
                    field = value;
                    if (value.HasValue)
                    {
                        owner.LastAbsoluteExpirationRelativeToNow = value.Value;
                    }
                }
            }
            public TimeSpan? SlidingExpiration { get; set; }
            public IList<IChangeToken> ExpirationTokens { get; } = new List<IChangeToken>();
            public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks { get; } 
                = new List<PostEvictionCallbackRegistration>();
            public CacheItemPriority Priority { get; set; }
            public long? Size { get; set; }
            public void Dispose()
            {
                owner._store[Key] = Value;
            }
        }
    }