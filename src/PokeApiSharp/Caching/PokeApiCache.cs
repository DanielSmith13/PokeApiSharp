using Microsoft.Extensions.Caching.Memory;
using PokeApiSharp.Monads;

namespace PokeApiSharp.Caching;

/// <summary>
/// Implements a caching mechanism for PokeApi resources using IMemoryCache.
/// </summary>
/// <param name="cache">An instance of IMemoryCache used for caching PokeApi resources.</param>
/// <param name="cacheDuration">
/// How long each cached entry is kept before expiring. Defaults to one hour if not specified.
/// </param>
public class PokeApiCache(IMemoryCache cache, TimeSpan? cacheDuration = null) : IPokeApiCache
{
    private readonly TimeSpan _cacheDuration = ValidateCacheDuration(cacheDuration);

    private static TimeSpan ValidateCacheDuration(TimeSpan? cacheDuration)
    {
        var validatedDuration = cacheDuration ?? TimeSpan.FromHours(1);

        return validatedDuration <= TimeSpan.Zero 
            ? throw new ArgumentOutOfRangeException(nameof(cacheDuration), "Cache duration must be greater than TimeSpan.Zero.")
            : validatedDuration;
    }
    /// <inheritdoc/>
    public Either<TResource, string> GetCachedResource<TResource>(string url)
    {
        if (!cache.TryGetValue(url, out TResource? resource))
        {
            return "Resource not found in cache.";
        }
        return resource != null
            ? resource
            : "Cached resource is null.";
    }
    /// <inheritdoc/>
    public void SetCachedResource<TResource>(string url, TResource resource)
        => cache.Set(url, resource, _cacheDuration);

    /// <inheritdoc/>
    public void ClearCache()
        => (cache as MemoryCache)?.Clear();

    /// <inheritdoc/>
    public void Dispose()
    {
        cache.Dispose();
    }
}