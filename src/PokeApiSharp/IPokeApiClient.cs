using PokeApiSharp.PokeApi;

namespace PokeApiSharp;

public interface IPokeApiClient
{
    /// <summary>
    /// Fetches a single API resource by name. The provided <paramref name="name"/> is
    /// trimmed of leading/trailing whitespace and compared case-insensitively.
    /// The method constructs a resource path using <see cref="GetResourcePath{T}"/> and
    /// performs an HTTP GET to the PokeAPI.
    /// </summary>
    /// <typeparam name="TResource">
    /// The CLR type that represents the API resource to fetch. This type must be
    /// decorated with the <see cref="PokeApiResource"/> attribute to identify the
    /// corresponding API path (for example, <c>pokemon</c>, <c>item</c>, etc.).
    /// </typeparam>
    /// <param name="name">The name (string identifier) of the resource to fetch. Leading/trailing whitespace is ignored; case is not significant.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
    /// <returns>
    /// A task that resolves to an instance of <typeparamref name="TResource"/> if the resource is found and deserialized successfully; otherwise null if deserialization returns null.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown if <typeparamref name="TResource"/> is not declared as an API resource (missing <see cref="PokeApiResource"/> attribute).</exception>
    /// <exception cref="System.Net.Http.HttpRequestException">Thrown when the HTTP request does not return a successful status code.</exception>
    /// <example>
    /// Basic usage:
    /// <code>
    /// var zigzagoon = await client.GetAsync&lt;Pokemon&gt;("zigzagoon");
    /// </code>
    /// Example showing trimming and case-insensitivity:
    /// <code>
    /// // Leading/trailing whitespace and casing are normalized
    /// var masterBall = await client.GetAsync&lt;Item&gt;("  Master Ball  ");
    /// </code>
    /// Example with a CancellationToken:
    /// <code>
    /// using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
    /// var pikachu = await client.GetAsync&lt;Pokemon&gt;("Pikachu", cts.Token);
    /// </code>
    /// </example>
    Task<TResource?> GetAsync<TResource>(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches a single API resource by ID.
    /// The method constructs a resource path using
    /// <see cref="GetResourcePath{T}"/> and performs an HTTP GET to the PokeAPI.
    /// </summary>
    /// <typeparam name="TResource">
    /// The CLR type that represents the API resource to fetch. This type must be
    /// decorated with the <see cref="PokeApiResource"/> attribute to identify the
    /// corresponding API path (for example, <c>pokemon</c>, <c>item</c>, etc.).
    /// </typeparam>
    /// <param name="id">The ID of the resource to fetch.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
    /// <returns>A task that resolves to an instance of <typeparamref name="TResource"/> if the resource is found and deserialized successfully; otherwise null if deserialization returns null.</returns>
    /// /// <exception cref="InvalidOperationException">Thrown if <typeparamref name="TResource"/> is not declared as an API resource (missing <see cref="PokeApiResource"/> attribute).</exception>
    /// <exception cref="System.Net.Http.HttpRequestException">Thrown when the HTTP request does not return a successful status code.</exception>
    /// <example>
    /// Basic usage:
    /// <code>
    /// var zigzagoon = await client.GetAsync&lt;Pokemon&gt;(263);
    /// </code>
    /// Example with a CancellationToken:
    /// <code>
    /// using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
    /// var pikachu = await client.GetAsync&lt;Pokemon&gt;(25, cts.Token);
    /// </code>
    /// </example>
    Task<TResource?> GetAsync<TResource>(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches a single API resource by ApiResource.
    /// The method performs an HTTP GET to the PokeAPI using the URL provided in the resource.
    /// </summary>
    /// <typeparam name="TResource">
    /// The CLR type that represents the API resource to fetch. This type must be
    /// decorated with the <see cref="PokeApiResource"/> attribute to identify the
    /// corresponding API path (for example, <c>pokemon</c>, <c>item</c>, etc.).
    /// </typeparam>
    /// <param name="resource">The ApiResource to fetch.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
    /// <returns>A task that resolves to an instance of <typeparamref name="TResource"/> if the resource is found and deserialized successfully; otherwise null if deserialization returns null.</returns>
    /// /// <exception cref="InvalidOperationException">Thrown if <typeparamref name="TResource"/> is not declared as an API resource (missing <see cref="PokeApiResource"/> attribute).</exception>
    /// <exception cref="System.Net.Http.HttpRequestException">Thrown when the HTTP request does not return a successful status code.</exception>
    /// <example>
    /// Basic usage:
    /// <code>
    /// var zigzagoon = await client.GetAsync&lt;PokemonSpecies&gt;("zigzagoon");
    /// var evolutionChain = await client.GetAsync&lt;EvolutionChain&gt;(zigzagoon.EvolutionChain);
    /// </code>
    /// Example with a CancellationToken:
    /// <code>
    /// using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
    /// var zigzagoon = await client.GetAsync&lt;PokemonSpecies&gt;("zigzagoon", cts.Token);
    /// var evolutionChain = await client.GetAsync&lt;EvolutionChain&gt;(zigzagoon.EvolutionChain, cts.Token);
    /// </code>
    /// </example>
    Task<TResource?> GetAsync<TResource>(ApiResource<TResource> resource, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches a list of API resources. The method constructs a resource path using <see cref="GetResourcePath{T}"/> and performs an HTTP GET to the PokeAPI with pagination parameters.
    /// </summary>
    /// <param name="limit">The maximum number of resources to return per page. Defaults to 20.</param>
    /// <param name="offset">The starting index for pagination. Defaults to 0.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
    /// <typeparam name="TResource">The type of API resource to fetch.</typeparam>
    /// <returns>A task that resolves to a <see cref="NamedApiResourceList{T}"/> containing the list of resources.</returns>
    /// <exception cref="InvalidOperationException">Thrown if <typeparamref name="TResource"/> is not declared as an API resource (missing <see cref="PokeApiResource"/> attribute).</exception>
    /// <exception cref="System.Net.Http.HttpRequestException">Thrown when the HTTP request does not return a successful status code.</exception>
    /// <example>
    /// Basic usage:
    /// <code>
    /// var pokemonList = await client.ListAsync&lt;Pokemon&gt;();
    /// </code>
    /// Pagination example:
    /// <code>
    /// var page1 = await client.ListAsync&lt;Pokemon&gt;(limit: 10, offset: 0);
    /// var page2 = await client.ListAsync&lt;Pokemon&gt;(limit: 10, offset: 10);
    /// </code>
    /// </example>
    Task<NamedApiResourceList<TResource>> ListAsync<TResource>(
        int? limit = 20,
        int? offset = 0,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches multiple API resources by ApiResource.
    /// The method performs HTTP GET requests to the PokeAPI using the URL provided in the resources.
    /// </summary>
    /// <typeparam name="TResource">
    /// The CLR type that represents the API resource to fetch. This type must be
    /// decorated with the <see cref="PokeApiResource"/> attribute to identify the
    /// corresponding API path (for example, <c>pokemon</c>, <c>item</c>, etc.).
    /// </typeparam>
    /// <param name="resources">A collection of ApiResources to fetch.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
    /// <returns>A task that resolves to an instance of <typeparamref name="TResource"/> if the resource is found and deserialized successfully; otherwise null if deserialization returns null.</returns>
    /// /// <exception cref="InvalidOperationException">Thrown if <typeparamref name="TResource"/> is not declared as an API resource (missing <see cref="PokeApiResource"/> attribute).</exception>
    /// <exception cref="System.Net.Http.HttpRequestException">Thrown when the HTTP request does not return a successful status code.</exception>
    /// <example>
    /// Basic usage:
    /// <code>
    /// var pokemonList = new List&lt;PokemonSpecies&gt;();
    /// pokemonList.Add(new ApiResource&lt;PokemonSpecies&gt;("zigzagoon"));
    /// pokemonList.Add(new ApiResource&lt;PokemonSpecies&gt;("charizard"));
    /// var evolutionChain = await client.GetAsync&lt;EvolutionChain&gt;(pokemonList.Select(p => p.EvolutionChain));
    /// </code>
    /// Example with a CancellationToken:
    /// <code>
    /// using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
    /// var pokemonList = new List&lt;PokemonSpecies&gt;();
    /// pokemonList.Add(await client.GetAsync&lt;PokemonSpecies&gt;("zigzagoon", cts.Token));
    /// pokemonList.Add(await client.GetAsync&lt;PokemonSpecies&gt;("charizard", cts.Token));
    /// var evolutionChain = await client.GetAsync&lt;EvolutionChain&gt;(pokemonList.Select(p => p.EvolutionChain), cts.Token);
    /// </code>
    /// </example>
    Task<IEnumerable<TResource?>> GetAsync<TResource>(IEnumerable<ApiResource<TResource>> resources,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches a single Named API resource by NamedApiResource.
    /// The method performs an HTTP GET to the PokeAPI using the URL provided in the resource.
    /// </summary>
    /// <typeparam name="TResource">
    /// The CLR type that represents the API resource to fetch. This type must be
    /// decorated with the <see cref="PokeApiResource"/> attribute to identify the
    /// corresponding API path (for example, <c>pokemon</c>, <c>item</c>, etc.).
    /// </typeparam>
    /// <param name="resource">The NamedApiResource to fetch.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
    /// <returns>A task that resolves to an instance of <typeparamref name="TResource"/> if the resource is found and deserialized successfully; otherwise null if deserialization returns null.</returns>
    /// /// <exception cref="InvalidOperationException">Thrown if <typeparamref name="TResource"/> is not declared as an API resource (missing <see cref="PokeApiResource"/> attribute).</exception>
    /// <exception cref="System.Net.Http.HttpRequestException">Thrown when the HTTP request does not return a successful status code.</exception>
    /// <example>
    /// Basic usage:
    /// <code>
    /// var zigzagoon = await client.GetAsync&lt;PokemonSpecies&gt;("zigzagoon");
    /// var generation = await client.GetAsync&lt;Generation&gt;(zigzagoon.Generation);
    /// </code>
    /// Example with a CancellationToken:
    /// <code>
    /// using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
    /// var zigzagoon = await client.GetAsync&lt;PokemonSpecies&gt;("zigzagoon", cts.Token);
    /// var generation = await client.GetAsync&lt;Generation&gt;(zigzagoon.Generation, cts.Token);
    /// </code>
    /// </example>
    Task<TResource?> GetAsync<TResource>(NamedApiResource<TResource> resource, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches multiple Named API resources by ApiResource.
    /// The method performs HTTP GET requests to the PokeAPI using the URL provided in the resources.
    /// </summary>
    /// <typeparam name="TResource">
    /// The CLR type that represents the API resource to fetch. This type must be
    /// decorated with the <see cref="PokeApiResource"/> attribute to identify the
    /// corresponding API path (for example, <c>pokemon</c>, <c>item</c>, etc.).
    /// </typeparam>
    /// <param name="resources">A collection of NamedApiResources to fetch.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
    /// <returns>A task that resolves to an instance of <typeparamref name="TResource"/> if the resource is found and deserialized successfully; otherwise null if deserialization returns null.</returns>
    /// /// <exception cref="InvalidOperationException">Thrown if <typeparamref name="TResource"/> is not declared as an API resource (missing <see cref="PokeApiResource"/> attribute).</exception>
    /// <exception cref="System.Net.Http.HttpRequestException">Thrown when the HTTP request does not return a successful status code.</exception>
    /// <example>
    /// Basic usage:
    /// <code>
    /// var zigzagoon = await client.GetAsync&lt;PokemonSpecies&gt;("zigzagoon");
    /// var eggGroups = await client.GetAsync&lt;EggGroup&gt;(zigzagoon.EggGroups);
    /// </code>
    /// Example with a CancellationToken:
    /// <code>
    /// using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
    /// var zigzagoon = await client.GetAsync&lt;PokemonSpecies&gt;("zigzagoon", cts.Token);
    /// var eggGroups = await client.GetAsync&lt;EggGroup&gt;(zigzagoon.EggGroups, cts.Token);
    /// </code>
    /// </example>
    Task<IEnumerable<TResource?>> GetAsync<TResource>(IEnumerable<NamedApiResource<TResource>> resources,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Fetches all API resources of a given type. The method constructs a resource path using <see cref="GetResourcePath{T}"/> and performs HTTP GET to the PokeAPI with pagination parameters.
    /// The method uses asynchronous iteration to fetch resources in batches, allowing for efficient handling of large result sets.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TResource"></typeparam>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of fetched resources.</returns>
    /// <exception cref="InvalidOperationException">Thrown if <typeparamref name="TResource"/> is not declared as an API resource (missing <see cref="PokeApiResource"/> attribute).</exception>
    /// <exception cref="System.Net.Http.HttpRequestException">Thrown when the HTTP request does not return a successful status code.</exception>
    /// <example>
    /// Basic usage:
    /// <code>
    /// var pokemonList = await client.GetAsync&lt;Pokemon&gt;();
    /// </code>
    /// Example with a CancellationToken:
    /// <code>
    /// using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
    /// var pokemonList = await client.GetAsync&lt;Pokemon&gt;(cts.Token);
    /// </code>
    /// </example>
    Task<IEnumerable<TResource?>> GetAsync<TResource>(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Clears the internal cache.
    /// </summary>
    void ClearCache();
    
    /// <summary>
    /// Disposes the client and releases any resources associated with it.
    /// </summary>
    void Dispose();
}