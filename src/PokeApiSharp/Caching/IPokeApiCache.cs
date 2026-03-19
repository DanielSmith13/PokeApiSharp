using PokeApiSharp.Monads;

namespace PokeApiSharp.Caching;

/// <summary>
/// Interface for caching PokeApi resources. Implementations of this interface can be
/// used to cache API responses and reduce the number of API calls made by the library.
/// </summary>
public interface IPokeApiCache
{
    /// <summary>
    /// Gets a cached resource from the cache. If the resource is not found in the cache, returns an error message.
    /// </summary>
    /// <param name="url">The URL of the resource to retrieve from the cache.</param>
    /// <typeparam name="TResource">The type of the resource to retrieve.</typeparam>
    /// <returns>An Either type containing either the cached resource or an error message.</returns>
    Either<TResource, string> GetCachedResource<TResource>(string url);
    /// <summary>
    /// Sets a resource in the cache with the specified URL as the key.
    /// This method can be used to store API responses in the cache for future retrieval.
    /// </summary>
    /// <param name="url">The URL of the resource to store in the cache.</param>
    /// <param name="resource">The resource to store in the cache.</param>
    /// <typeparam name="TResource">The type of the resource to store.</typeparam>
    void SetCachedResource<TResource>(string url, TResource resource);
    /// <summary>
    /// Clears all cached resources from the cache.
    /// This method can be used to reset the cache and remove all stored resources.
    /// </summary>
    void ClearCache();
    /// <summary>
    /// Disposes of any resources used by the cache.
    /// This method should be called when the cache is no longer needed to free up any resources it may be using.
    /// </summary>
    void Dispose();
}