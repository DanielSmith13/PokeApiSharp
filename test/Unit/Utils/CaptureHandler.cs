using System.Text;

namespace Unit.Utils;

public class CaptureHandler(HttpResponseMessage response) : HttpMessageHandler
{
    public HttpRequestMessage? LastRequest { get; private set; }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        try
        {
            LastRequest = request;
            var clone = new HttpResponseMessage(response.StatusCode)
            {
                Content = new StringContent(response.Content
                    .ReadAsStringAsync(cancellationToken)
                    .GetAwaiter()
                    .GetResult(),
                    Encoding.UTF8,
                    "application/json"),
                ReasonPhrase = response.ReasonPhrase,
                Version = response.Version
            };
            return Task.FromResult(clone);
        }
        catch (Exception exception)
        {
            return Task.FromException<HttpResponseMessage>(exception);
        }
    }
}