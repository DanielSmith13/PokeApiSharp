using Microsoft.Extensions.Caching.Memory;
using PokeApiSharp.Monads;

namespace PokeApiSharp.Cache;

public class PokeApiCache(IMemoryCache cache) : IPokeApiCache
{
    public Either<TResource, string> GetCachedResource<TResource>(string url)
    {
        Console.WriteLine($"Attempting to retrieve resource from cache: {url}");
        if (!cache.TryGetValue(url, out TResource? resource))
        {
            Console.WriteLine($"Resource not found in cache: {url}");
            return new Either<TResource, string>("Resource not found in cache.");
        }
        Console.WriteLine($"Resource retrieved from cache: {url}");
        return resource != null
            ? new Either<TResource, string>(resource)
            : new Either<TResource, string>("Cached resource is null.");
    }

    public void SetCachedResource<TResource>(string url, TResource resource)
        => cache.Set(url, resource, TimeSpan.FromHours(1));

    public void ClearCache()
        => (cache as MemoryCache)?.Clear();

    public void Dispose()
    {
        cache.Dispose();
    }
}