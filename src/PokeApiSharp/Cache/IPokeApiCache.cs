using PokeApiSharp.Monads;

namespace PokeApiSharp.Cache;

public interface IPokeApiCache
{
    Either<TResource, string> GetCachedResource<TResource>(string url);
    void SetCachedResource<TResource>(string url, TResource resource);
    void ClearCache();
    void Dispose();
}