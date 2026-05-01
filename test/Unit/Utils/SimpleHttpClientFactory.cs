namespace Unit.Utils;

public sealed class SimpleHttpClientFactory(HttpClient client) : IHttpClientFactory
{
    public HttpClient CreateClient(string? name) => client;
}