namespace Unit.Utils;

public class DelayingHandler(TimeSpan delay, HttpResponseMessage response) : HttpMessageHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        await Task.Delay(delay, cancellationToken);
        return response;
    }
}