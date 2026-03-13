using System.Text.Json;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi;

namespace PokeApiSharp;

public class PokeApiClient : IPokeApiClient
{
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = true,
    };

    private const int MaxConcurrency = 8;

    public PokeApiClient(HttpClient httpClient)
    {
        if(httpClient.BaseAddress == null)
            httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        _httpClient = httpClient;
    }

    public async Task<T?> GetAsync<T>(string name, CancellationToken cancellationToken = default)
        => await FetchResourceAsync<T>($"{GetResourcePath<T>()}/{name.Trim().ToLowerInvariant()}", cancellationToken);

    
    public async Task<T?> GetAsync<T>(int id, CancellationToken cancellationToken = default)
        => await GetAsync<T>(id.ToString(), cancellationToken);

    public async Task<T?> GetAsync<T>(ApiResource<T> resource, CancellationToken cancellationToken = default)
        => await FetchResourceAsync<T>(resource.Url, cancellationToken);
    
    public async Task<NamedApiResourceList<T>> ListAsync<T>(int? limit = 20, int? offset = 0, CancellationToken cancellationToken = default)
        => await FetchResourceListAsync<T>($"{GetResourcePath<T>()}?limit={limit}&offset={offset}", cancellationToken);

    public async Task<IEnumerable<T?>> GetAsync<T>(IEnumerable<ApiResource<T>> resources,
        CancellationToken cancellationToken = default)
        => await FetchResourcesAsync<T?>(resources.Select(r => r.Url), cancellationToken);

    public async Task<T?> GetAsync<T>(NamedApiResource<T> resource, CancellationToken cancellationToken = default)
        => await FetchResourceAsync<T>(resource.Url, cancellationToken);

    public async Task<IEnumerable<T?>> GetAsync<T>(IEnumerable<NamedApiResource<T>> resources,
        CancellationToken cancellationToken = default)
        => await FetchResourcesAsync<T?>(resources.Select(r => r.Url), cancellationToken);

    public async Task<IEnumerable<T?>> GetAsync<T>(CancellationToken cancellationToken = default)
    {
        var resources = new List<T?>();
        await foreach (var resource in FetchAllResourcesAsync<T>(GetResourcePath<T>(), cancellationToken))
            resources.Add(resource);
        return resources;
    }

    private async Task<T?> FetchResourceAsync<T>(string url, CancellationToken cancellationToken)
    {
        using var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        await using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        return await JsonSerializer.DeserializeAsync<T>(contentStream, JsonOptions, cancellationToken);
    }
    
    private async Task<IEnumerable<T?>> FetchResourcesAsync<T>(IEnumerable<string> urls, CancellationToken cancellationToken)
    {
        var tasks = urls.Select(url => FetchResourceAsync<T>(url, cancellationToken));
        return await Task.WhenAll(tasks);
    }
    
    private async Task<NamedApiResourceList<T>> FetchResourceListAsync<T>(string url, CancellationToken cancellationToken)
    {
        using var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        await using var stream =
            await response.Content.ReadAsStreamAsync(cancellationToken);
        return await JsonSerializer.DeserializeAsync<NamedApiResourceList<T>>(
            stream,
            JsonOptions,
            cancellationToken) ?? new NamedApiResourceList<T>(0, null, null, []);
    }

    private async IAsyncEnumerable<T?> FetchAllResourcesAsync<T>(
        string url,
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var nextUrl = url;
        var semaphore = new SemaphoreSlim(MaxConcurrency);
        while (nextUrl is not null)
        {
            var resourceList = await FetchResourceListAsync<T>(nextUrl, cancellationToken);
            nextUrl = resourceList.Next;

            var urls = resourceList.Results.Select(r => r.Url).ToList();

            var tasks = urls.Select(async u =>
            {
                await semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
                try
                {
                    return await FetchResourceAsync<T>(u, cancellationToken).ConfigureAwait(false);
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

    private static string GetResourcePath<T>()
    {
        var attribute = typeof(T).GetCustomAttributes(typeof(PokeApiResource), false);
        return attribute.Length > 0 
            ? ((PokeApiResource) attribute[0]).Path 
            : throw new InvalidOperationException("Resource is not an API resource");
    }
}