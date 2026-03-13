using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PokeApiSharp.PokeApi;

namespace PokeApiSharp;

public interface IPokeApiClient
{
    Task<T?> GetAsync<T>(string name, CancellationToken cancellationToken = default);

    Task<T?> GetAsync<T>(int id, CancellationToken cancellationToken = default);

    Task<T?> GetAsync<T>(ApiResource<T> resource, CancellationToken cancellationToken = default);

    Task<NamedApiResourceList<T>> ListAsync<T>(
        int? limit = 20,
        int? offset = 0,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<T?>> GetAsync<T>(IEnumerable<ApiResource<T>> resources,
        CancellationToken cancellationToken = default);

    Task<T?> GetAsync<T>(NamedApiResource<T> resource, CancellationToken cancellationToken = default);

    Task<IEnumerable<T?>> GetAsync<T>(IEnumerable<NamedApiResource<T>> resources,
        CancellationToken cancellationToken = default);
    
    Task<IEnumerable<T?>> GetAsync<T>(CancellationToken cancellationToken = default);
}