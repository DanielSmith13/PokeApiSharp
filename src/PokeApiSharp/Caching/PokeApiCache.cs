using Microsoft.Extensions.Caching.Memory;
using PokeApiSharp.Monads;

namespace PokeApiSharp.Caching;

/// <summary>
/// Implements a caching mechanism for PokeApi resources using IMemoryCache.
/// </summary>
/// <param name="cache">An instance of IMemoryCache used for caching PokeApi resources.</param>
public class PokeApiCache(IMemoryCache cache) : IPokeApiCache
{
    /// <inheritdoc/>
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
    /// <inheritdoc/>
    public void SetCachedResource<TResource>(string url, TResource resource)
        => cache.Set(url, resource, TimeSpan.FromHours(1));

    /// <inheritdoc/>
    public void ClearCache()
        => (cache as MemoryCache)?.Clear();

    /// <inheritdoc/>
    public void Dispose()
    {
        cache.Dispose();
    }
}