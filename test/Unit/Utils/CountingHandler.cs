namespace Unit.Utils;

/// <summary>
/// An async-capable HTTP handler that tracks the peak number of concurrent in-flight requests.
/// </summary>
public class CountingHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> responder)
    : HttpMessageHandler
{
    private int _concurrent;
    private int _maxConcurrent;

    public int MaxConcurrent => _maxConcurrent;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var current = Interlocked.Increment(ref _concurrent);

        int prev;
        do
        {
            prev = _maxConcurrent;
        } while (current > prev && Interlocked.CompareExchange(ref _maxConcurrent, current, prev) != prev);

        try
        {
            return await responder(request, cancellationToken);
        }
        finally
        {
            Interlocked.Decrement(ref _concurrent);
        }
    }
}
