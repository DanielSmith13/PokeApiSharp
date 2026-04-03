using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using PokeApiSharp;
using Unit.Utils;
using DelegatingHandler = Unit.Utils.DelegatingHandler;

namespace Unit.Clients;

public class PokeApiClientTests
{
    private const string BaseAddress = "https://pokeapi.co/api/v2/";
    private const string ResourcePath = "pokemon";
    private static Uri GetExpectedUrl(int i) => new($"{BaseAddress}{ResourcePath}/{i}");
    private static Uri GetExpectedUrl(string name) => new($"{BaseAddress}{ResourcePath}/{name}");

    private static Uri GetExpectedListUrl(int? limit = 20, int? offset = 0) =>
        new($"{BaseAddress}{ResourcePath}?limit={limit}&offset={offset}");

    private const string ResourceJson = "{\"id\":263,\"name\":\"zigzagoon\"}";

    private const string ListJson =
        "{\"count\":1,\"next\":null,\"previous\":null,\"results\":[{\"name\":\"zigzagoon\",\"url\":\"https://pokeapi.co/api/v2/pokemon/263/\"}]}";

    [Fact]
    public void Constructor_SetsBaseAddress_WhenNull()
    {
        var httpClient = new HttpClient();
        var pokeApiClient = new PokeApiClient(httpClient);

        Assert.NotNull(pokeApiClient);
        Assert.NotNull(httpClient.BaseAddress);
        Assert.Equal(new Uri("https://pokeapi.co/api/v2/"), httpClient.BaseAddress);
    }

    [Fact]
    public void Constructor_DoesNotSetBaseAddress_WhenNotNull()
    {
        var baseAddress = new Uri("https://pokeapi.co/api/v2/test");
        var httpClient = new HttpClient { BaseAddress = baseAddress };
        var pokeApiClient = new PokeApiClient(httpClient);

        Assert.NotNull(pokeApiClient);
        Assert.NotNull(httpClient.BaseAddress);
        Assert.Equal(baseAddress, httpClient.BaseAddress);
    }

    [Theory]
    [InlineData(263)]
    public async Task GetAsync_ById_UsesExpectedUrl(int id)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(ResourceJson, Encoding.UTF8, "application/json")
        };

        var handler = new CaptureHandler(response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");

        var sut = new PokeApiClient(httpClient);

        var result = await sut.GetAsync<Pokemon>(id);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal("zigzagoon", result.Name);

        Assert.NotNull(handler.LastRequest);
        Assert.Equal(GetExpectedUrl(id), handler.LastRequest.RequestUri);
        Assert.Equal(HttpMethod.Get, handler.LastRequest.Method);
    }


    [Theory]
    [InlineData("zigzagoon")]
    [InlineData("  ZiGzAgOoN  ")]
    public async Task GetAsync_ByName_UsesExpectedUrl(string name)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(ResourceJson, Encoding.UTF8, "application/json")
        };

        var handler = new CaptureHandler(response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");

        var sut = new PokeApiClient(httpClient);

        var result = await sut.GetAsync<Pokemon>(name);

        Assert.NotNull(result);
        Assert.Equal(263, result.Id);
        Assert.Equal("zigzagoon", result.Name);

        Assert.NotNull(handler.LastRequest);
        Assert.Equal(GetExpectedUrl("zigzagoon"), handler.LastRequest.RequestUri);
        Assert.Equal(HttpMethod.Get, handler.LastRequest.Method);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(100, null)]
    [InlineData(null, 100)]
    [InlineData(100, 100)]
    public async Task ListAsync_UsesProvidedParams_AndDefaultsTo20LimitAnd0Offset(int? limit, int? offset)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(ListJson, Encoding.UTF8, "application/json")
        };

        var handler = new CaptureHandler(response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");

        var sut = new PokeApiClient(httpClient);

        var result = await sut.ListAsync<Pokemon>(limit, offset);

        Assert.NotNull(result);
        Assert.Equal(1, result.Count);
        Assert.Null(result.Next);
        Assert.Null(result.Previous);
        Assert.NotNull(result.Results);
        Assert.Equal("https://pokeapi.co/api/v2/pokemon/263/", result.Results[0].Url);
        Assert.Equal("zigzagoon", result.Results[0].Name);


        Assert.NotNull(handler.LastRequest);
        Assert.Equal(GetExpectedListUrl(limit, offset), handler.LastRequest.RequestUri);
        Assert.Equal(HttpMethod.Get, handler.LastRequest.Method);
    }

    [Fact]
    public async Task GetAsync_GetsValueFromCache_WhenAlreadyFetched()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(ResourceJson, Encoding.UTF8, "application/json")
        };

        var handler = new CaptureHandler(response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");

        var sut = new PokeApiClient(httpClient);

        var first = await sut.GetAsync<Pokemon>(263);

        Assert.Equal(1, handler.RequestCount);
        Assert.NotNull(handler.LastRequest);
        Assert.Equal(GetExpectedUrl(263), handler.LastRequest.RequestUri);
        Assert.Equal(HttpMethod.Get, handler.LastRequest.Method);
        Assert.NotNull(first);
        Assert.Equal("zigzagoon", first.Name);
        Assert.Equal(263, first.Id);

        var second = await sut.GetAsync<Pokemon>(263);

        Assert.NotNull(second);
        Assert.Equal("zigzagoon", second.Name);
        Assert.Equal(263, second.Id);
        Assert.Equal(1, handler.RequestCount);
    }

    [Fact]
    public async Task ClearCache_RemovesCachedItem_AndNextCallHitsHttp()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(ResourceJson, Encoding.UTF8, "application/json")
        };

        var handler = new CaptureHandler(response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri(BaseAddress);

        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var sut = new PokeApiClient(httpClient, memoryCache);

        var first = await sut.GetAsync<Pokemon>(263);
        Assert.NotNull(first);
        Assert.Equal(1, handler.RequestCount);

        sut.ClearCache();

        var second = await sut.GetAsync<Pokemon>(263);

        Assert.NotNull(second);
        Assert.Equal(2, handler.RequestCount);
    }

    [Fact]
    public void Dispose_DoesNotDispose_ExternalHttpClient()
    {
        var handler = new TrackingHandler();
        var httpClient = new HttpClient(handler) { BaseAddress = new Uri(BaseAddress) };

        var sut = new PokeApiClient(httpClient, new MemoryCache(new MemoryCacheOptions()));

        sut.Dispose();

        Assert.False(handler.WasDisposed);

        httpClient.Dispose();
        Assert.True(handler.WasDisposed);
    }

    [Fact]
    public void Dispose_DoesNotDispose_ExternalMemoryCache()
    {
        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var httpClient = new HttpClient();
        var sut = new PokeApiClient(httpClient, memoryCache);

        sut.Dispose();

        memoryCache.Set("key", "value");
        Assert.True(memoryCache.TryGetValue("key", out string? value));
        Assert.Equal("value", value);

        httpClient.Dispose();
        memoryCache.Dispose();
    }

    [Fact]
    public async Task GetAsync_Throws_OnNonSuccessStatus()
    {
        var response = new HttpResponseMessage(HttpStatusCode.NotFound);

        var handler = new CaptureHandler(response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri(BaseAddress);

        var sut = new PokeApiClient(httpClient);

        await Assert.ThrowsAsync<HttpRequestException>(async () => { await sut.GetAsync<Pokemon>(9999); });

        Assert.Equal(1, handler.RequestCount);
    }

    [Fact]
    public async Task GetAsync_MultipleResources_ThrowsIfAnyFail()
    {
        var okResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(ResourceJson, Encoding.UTF8, "application/json")
        };
        var notFoundResponse = new HttpResponseMessage(HttpStatusCode.NotFound);

        var handler = new DelegatingHandler(request => request.RequestUri!.AbsoluteUri.EndsWith("/pokemon/1")
            ? okResponse
            : notFoundResponse);

        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri(BaseAddress);

        var sut = new PokeApiClient(httpClient);

        var resources = new[]
        {
            new ApiResource<Pokemon>("pokemon/1"),
            new ApiResource<Pokemon>("pokemon/9999")
        };

        await Assert.ThrowsAsync<HttpRequestException>(async () => { await sut.GetAsync(resources); });
    }

    [Fact]
    public async Task GetAsync_ThrowsOnInvalidJson()
    {
        const string badJson = "{ not valid json ";
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(badJson, Encoding.UTF8, "application/json")
        };

        var handler = new CaptureHandler(response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri(BaseAddress);

        var sut = new PokeApiClient(httpClient);

        await Assert.ThrowsAsync<JsonException>(async () => { await sut.GetAsync<Pokemon>(1); });
    }

    [Fact]
    public async Task GetAsync_HonorsCancellationToken()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(ResourceJson, Encoding.UTF8, "application/json")
        };
        var handler = new DelayingHandler(TimeSpan.FromSeconds(5), response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri(BaseAddress);

        var sut = new PokeApiClient(httpClient);

        using var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromMilliseconds(50));

        await Assert.ThrowsAsync<TaskCanceledException>(async () => { await sut.GetAsync<Pokemon>(1, cts.Token); });
    }

    [Fact]
    public async Task GetAsync_Overloads_ApiResourceAndNamedApiResource()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(ResourceJson, Encoding.UTF8, "application/json")
        };
        var handler = new CaptureHandler(response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri(BaseAddress);
        var sut = new PokeApiClient(httpClient);

        var apiResource = new ApiResource<Pokemon>("pokemon/263");
        var byApiResource = await sut.GetAsync(apiResource);

        Assert.NotNull(byApiResource);
        Assert.Equal(263, byApiResource.Id);

        var named = new NamedApiResource<Pokemon>("zigzagoon", "pokemon/263");
        var byNamed = await sut.GetAsync(named);

        Assert.NotNull(byNamed);
        Assert.Equal(263, byNamed.Id);
    }

    [Fact]
    public async Task GetAsync_MultipleNamedResources_FetchesEachResource()
    {
        const string pokemon1Json = "{\"id\":1,\"name\":\"a\"}";
        const string pokemon2Json = "{\"id\":2,\"name\":\"b\"}";

        var handler = new DelegatingHandler(req =>
        {
            var uri = req.RequestUri!.ToString();
            if (uri.EndsWith("/pokemon/1/") || uri.EndsWith("/pokemon/1"))
                return new HttpResponseMessage(HttpStatusCode.OK)
                    { Content = new StringContent(pokemon1Json, Encoding.UTF8, "application/json") };
            if (uri.EndsWith("/pokemon/2/") || uri.EndsWith("/pokemon/2"))
                return new HttpResponseMessage(HttpStatusCode.OK)
                    { Content = new StringContent(pokemon2Json, Encoding.UTF8, "application/json") };
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        });

        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri(BaseAddress);

        var sut = new PokeApiClient(httpClient);

        var results = (await sut.GetAsync([
            new NamedApiResource<Pokemon>("a", "pokemon/1"),
            new NamedApiResource<Pokemon>("b", "pokemon/2")
        ])).ToList();

        Assert.Equal(2, results.Count);
        Assert.NotNull(results[0]);
        Assert.NotNull(results[1]);
        Assert.Equal(1, results[0]?.Id);
        Assert.Equal(2, results[1]?.Id);
        Assert.Equal("a", results[0]?.Name);
        Assert.Equal("b", results[1]?.Name);
    }
    
    [Fact]
    public async Task FetchAllResourcesAsync_FollowsPages_AndFetchesEachResource()
    {
        var firstListJson = JsonSerializer.Serialize(
            new NamedApiResourceList<Pokemon>(
                2,
                "https://pokeapi.co/api/v2/pokemon?limit=100&offset=100",
                null,
                [
                    new NamedApiResource<Pokemon>("a", "https://pokeapi.co/api/v2/pokemon/1/"),
                    new NamedApiResource<Pokemon>("b", "https://pokeapi.co/api/v2/pokemon/2/")
                ]));
        var secondListJson = JsonSerializer.Serialize(
            new NamedApiResourceList<Pokemon>(
                2,
                null,
                "https://pokeapi.co/api/v2/pokemon?limit=100&offset=0"
                , [
                    new NamedApiResource<Pokemon>("c", "https://pokeapi.co/api/v2/pokemon/3/"),
                    new NamedApiResource<Pokemon>("d", "https://pokeapi.co/api/v2/pokemon/4/")
                ]));
        
        const string pokemon1Json = "{\"id\":1,\"name\":\"a\"}";
        const string pokemon2Json = "{\"id\":2,\"name\":\"b\"}";
        const string pokemon3Json = "{\"id\":3,\"name\":\"c\"}";
        const string pokemon4Json = "{\"id\":4,\"name\":\"d\"}";

        var handler = new DelegatingHandler(req =>
        {
            var uri = req.RequestUri!.ToString();
            if (uri.EndsWith("pokemon") || uri.Contains("offset=0"))
                return new HttpResponseMessage(HttpStatusCode.OK)
                    { Content = new StringContent(firstListJson, Encoding.UTF8, "application/json") };
            if (uri.Contains("offset=100"))
                return new HttpResponseMessage(HttpStatusCode.OK)
                    { Content = new StringContent(secondListJson, Encoding.UTF8, "application/json") };
            if (uri.EndsWith("/pokemon/1/") || uri.EndsWith("/pokemon/1"))
                return new HttpResponseMessage(HttpStatusCode.OK)
                    { Content = new StringContent(pokemon1Json, Encoding.UTF8, "application/json") };
            if (uri.EndsWith("/pokemon/2/") || uri.EndsWith("/pokemon/2"))
                return new HttpResponseMessage(HttpStatusCode.OK)
                    { Content = new StringContent(pokemon2Json, Encoding.UTF8, "application/json") };
            if (uri.EndsWith("/pokemon/3/") || uri.EndsWith("/pokemon/3"))
                return new HttpResponseMessage(HttpStatusCode.OK)
                    { Content = new StringContent(pokemon3Json, Encoding.UTF8, "application/json") };
            if (uri.EndsWith("/pokemon/4/") || uri.EndsWith("/pokemon/4"))
                return new HttpResponseMessage(HttpStatusCode.OK)
                    { Content = new StringContent(pokemon4Json, Encoding.UTF8, "application/json") };
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        });

        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri(BaseAddress);
        var sut = new PokeApiClient(httpClient);

        var all = await sut.GetAsync<Pokemon>();

        var list = all.ToList();
        Assert.Equal(4, list.Count);
        Assert.Equal(1, list[0]?.Id);
        Assert.Equal(2, list[1]?.Id);
        Assert.Equal(3, list[2]?.Id);
        Assert.Equal(4, list[3]?.Id);
    }

    [Fact]
    public async Task FetchAllResourcesAsync_ReturnsEmpty_WhenNoResults()
    {
        var list = new NamedApiResourceList<Pokemon>(0, null, null, new List<NamedApiResource<Pokemon>>());
        var listJson = JsonSerializer.Serialize(list);

        var handler = new DelegatingHandler(req =>
        {
            var uri = req.RequestUri!.ToString();
            if (uri.Contains("offset=0"))
                return new HttpResponseMessage(HttpStatusCode.OK)
                    { Content = new StringContent(listJson, Encoding.UTF8, "application/json") };
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        });

        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri(BaseAddress);

        var sut = new PokeApiClient(httpClient);

        var all = (await sut.GetAsync<Pokemon>()).ToList();

        Assert.Empty(all);
    }

    [Fact]
    public async Task FetchAllResourcesAsync_Stops_WhenPageDeserializesToNull()
    {
        var handler = new DelegatingHandler(req =>
        {
            var uri = req.RequestUri!.ToString();
            if (uri.Contains("offset=0"))
                return new HttpResponseMessage(HttpStatusCode.OK)
                    { Content = new StringContent("null", Encoding.UTF8, "application/json") };
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        });

        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri(BaseAddress);

        var sut = new PokeApiClient(httpClient);

        var all = (await sut.GetAsync<Pokemon>()).ToList();

        Assert.Empty(all);
    }

    [Fact]
    public async Task FetchAllResourcesAsync_SwallowsIndividualResourceFailures()
    {
        var firstList = new NamedApiResourceList<Pokemon>(
            2,
            null,
            null,
            new List<NamedApiResource<Pokemon>>
            {
                new("a", "https://pokeapi.co/api/v2/pokemon/1/"),
                new("b", "https://pokeapi.co/api/v2/pokemon/2/")
            });

        var firstListJson = JsonSerializer.Serialize(firstList);
        const string pokemon1Json = "{\"id\":1,\"name\":\"a\"}";

        var handler = new DelegatingHandler(req =>
        {
            var uri = req.RequestUri!.ToString();
            if (uri.Contains("offset=0"))
                return new HttpResponseMessage(HttpStatusCode.OK)
                    { Content = new StringContent(firstListJson, Encoding.UTF8, "application/json") };
            if (uri.EndsWith("/pokemon/1/") || uri.EndsWith("/pokemon/1"))
                return new HttpResponseMessage(HttpStatusCode.OK)
                    { Content = new StringContent(pokemon1Json, Encoding.UTF8, "application/json") };
            if (uri.EndsWith("/pokemon/2/") || uri.EndsWith("/pokemon/2"))
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        });

        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri(BaseAddress);

        var sut = new PokeApiClient(httpClient);

        var all = (await sut.GetAsync<Pokemon>()).ToList();

        Assert.Equal(2, all.Count);
        Assert.NotNull(all[0]);
        Assert.Equal(1, all[0]?.Id);
        Assert.Null(all[1]);
    }

    [Fact]
    public async Task FetchAllResourcesAsync_HonorsCancellationToken()
    {
        var listJson = JsonSerializer.Serialize(new NamedApiResourceList<Pokemon>(0, null, null, new List<NamedApiResource<Pokemon>>()));
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(listJson, Encoding.UTF8, "application/json")
        };

        var handler = new DelayingHandler(TimeSpan.FromSeconds(5), response);
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri(BaseAddress);

        var sut = new PokeApiClient(httpClient);

        using var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromMilliseconds(50));

        await Assert.ThrowsAsync<TaskCanceledException>(async () => { await sut.GetAsync<Pokemon>(cts.Token); });
    }
}