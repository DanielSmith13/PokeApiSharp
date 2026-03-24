using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PokeApiSharp.Attributes;
using PokeApiSharp.Caching;
using PokeApiSharp.Utilities;

namespace PokeApiSharp;

/// <summary>
/// Provides functionality to interact with the PokeAPI, a RESTful API for Pokémon data.
/// </summary>
/// <remarks>
/// Supports operations for retrieving specific resources, listing resources,
/// and caching responses to optimize performance. This class serves as the primary client
/// for interacting with the API.
/// </remarks>
public class PokeApiClient : IPokeApiClient
{
    private readonly HttpClient _httpClient;
    private bool _ownsHttpClient;
    private readonly PokeApiCache _cache;
    private bool _ownsCache;
    private readonly ILogger<PokeApiClient>? _logger;
    private const string BaseAddress = "https://pokeapi.co/api/v2/";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = true,
    };

    private const int MaxConcurrency = 8;

    /// <summary>
    /// Provides methods to interact with the PokeAPI, a RESTful API for retrieving Pokémon data.
    /// </summary>
    /// <param name="httpClient">
    /// An optional HttpClient instance to use for API requests.
    /// If not provided, a new instance will be created.
    /// </param>
    /// <param name="cache">
    /// An optional IMemoryCache instance to use for caching API responses.
    /// If not provided, a new instance will be created.
    /// </param>
    /// <param name="logger">
    /// An optional ILogger instance for logging unmapped properties and other diagnostic information.
    /// </param>
    public PokeApiClient(HttpClient? httpClient = null, IMemoryCache? cache = null, ILogger<PokeApiClient>? logger = null)
    {
        httpClient ??= InitialiseHttpClient();
        if (httpClient.BaseAddress == null)
            httpClient.BaseAddress = new Uri(BaseAddress);
        _httpClient = httpClient;
        _cache = InitialiseCache(cache);
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<TResource?> GetAsync<TResource>(string name, CancellationToken cancellationToken = default)
        => await GetResourceAsync<TResource>(
            $"{GetResourcePath<TResource>()}/{name.Trim().ToLowerInvariant()}",
            cancellationToken);

    /// <inheritdoc/>
    public async Task<TResource?> GetAsync<TResource>(int id, CancellationToken cancellationToken = default)
        => await GetAsync<TResource>(id.ToString(), cancellationToken);

    /// <inheritdoc/>
    public async Task<TResource?> GetAsync<TResource>(ApiResource<TResource> resource,
        CancellationToken cancellationToken = default)
        => await GetResourceAsync<TResource>(resource.Url, cancellationToken);

    /// <inheritdoc/>
    public async Task<NamedApiResourceList<TResource>> ListAsync<TResource>(int? limit = 20, int? offset = 0,
        CancellationToken cancellationToken = default)
        => await GetResourceAsync<NamedApiResourceList<TResource>>(
               $"{GetResourcePath<TResource>()}?limit={limit}&offset={offset}",
               cancellationToken) ??
           new NamedApiResourceList<TResource>(0, null, null, new List<NamedApiResource<TResource>>());

    /// <inheritdoc/>
    public async Task<IEnumerable<TResource?>> GetAsync<TResource>(IEnumerable<ApiResource<TResource>> resources,
        CancellationToken cancellationToken = default)
        => await FetchResourcesAsync<TResource?>(resources.Select(r => r.Url), cancellationToken);

    /// <inheritdoc/>
    public async Task<TResource?> GetAsync<TResource>(NamedApiResource<TResource> resource,
        CancellationToken cancellationToken = default)
        => await GetResourceAsync<TResource>(resource.Url, cancellationToken);

    /// <inheritdoc/>
    public async Task<IEnumerable<TResource?>> GetAsync<TResource>(IEnumerable<NamedApiResource<TResource>> resources,
        CancellationToken cancellationToken = default)
        => await FetchResourcesAsync<TResource?>(resources.Select(r => r.Url), cancellationToken);

    /// <inheritdoc/>
    public async Task<IEnumerable<TResource?>> GetAsync<TResource>(CancellationToken cancellationToken = default)
    {
        var resources = new List<TResource?>();
        await foreach (var resource in FetchAllResourcesAsync<TResource>(GetResourcePath<TResource>(),
                           cancellationToken))
            resources.Add(resource);
        return resources;
    }

    /// <inheritdoc/>
    public void ClearCache() => _cache.ClearCache();

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_ownsHttpClient)
            _httpClient.Dispose();
        if (_ownsCache)
            _cache.Dispose();
    }

    private async Task<T?> GetResourceAsync<T>(string url, CancellationToken cancellationToken)
        => await _cache.GetCachedResource<T>(url).Match<Task<T?>>(
            resource => resource is not null
                ? Task.FromResult<T?>(resource)
                : Task.FromResult<T?>(default),
            _ => FetchResourceAsync<T>(url, cancellationToken));

    private async Task<T?> FetchResourceAsync<T>(string url, CancellationToken cancellationToken)
    {
        using var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        T? resource;
        if (_logger is not null)
        {
            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            LogUnmappedJsonProperties<T>(json);
            resource = JsonSerializer.Deserialize<T>(json, JsonOptions);
        }
        else
        {
            await using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
            resource = await JsonSerializer.DeserializeAsync<T>(contentStream, JsonOptions, cancellationToken);
        }

        _cache.SetCachedResource(url, resource);
        return resource;
    }

    private void LogUnmappedJsonProperties<T>(string json)
    {
        if (_logger is null) return;

        var unmapped = JsonDiffHelpers.FindUnmappedJsonProperties<T>(json, JsonOptions);

        if (unmapped.Count != 0)
        {
            _logger.LogWarning("Unmapped properties found for resource {ResourceType}: {UnmappedProperties}",
                typeof(T).Name, string.Join(", ", unmapped));
        }
    }

    private async Task<IEnumerable<T?>> FetchResourcesAsync<T>(IEnumerable<string> urls,
        CancellationToken cancellationToken)
    {
        var tasks = urls.Select(url => GetResourceAsync<T>(url, cancellationToken));
        return await Task.WhenAll(tasks);
    }

    private async IAsyncEnumerable<T?> FetchAllResourcesAsync<T>(
        string url,
        [System.Runtime.CompilerServices.EnumeratorCancellation]
        CancellationToken cancellationToken)
    {
        var nextUrl = url + "?limit=100&offset=0";
        var semaphore = new SemaphoreSlim(MaxConcurrency);
        while (nextUrl is not null)
        {
            var resourceList = await GetResourceAsync<NamedApiResourceList<T>>(nextUrl, cancellationToken);
            if (resourceList is null) yield break;
            nextUrl = resourceList.Next;

            var urls = resourceList.Results.Select(r => r.Url).ToList();

            var tasks = urls.Select(async u =>
            {
                await semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
                try
                {
                    return await GetResourceAsync<T>(u, cancellationToken).ConfigureAwait(false);
                }
                catch
                {
                    return default;
                }
                finally
                {
                    semaphore.Release();
                }
            });

            var results = await Task.WhenAll(tasks).ConfigureAwait(false);
            foreach (var result in results)
                yield return result;
        }
    }

    private static string GetResourcePath<TResource>()
    {
        var attribute = typeof(TResource).GetCustomAttributes(typeof(PokeApiResource), false);
        return attribute.Length > 0
            ? ((PokeApiResource)attribute[0]).Path
            : throw new InvalidOperationException($"Resource {typeof(TResource).Name} is not an API resource");
    }

    private HttpClient InitialiseHttpClient()
    {
        _ownsHttpClient = true;
        return new HttpClient
        {
            BaseAddress = new Uri(BaseAddress),
        };
    }

    private PokeApiCache InitialiseCache(IMemoryCache? cache)
    {
        if (cache is not null) return new PokeApiCache(cache);
        _ownsCache = true;
        return new PokeApiCache(new MemoryCache(new MemoryCacheOptions()));
    }
}