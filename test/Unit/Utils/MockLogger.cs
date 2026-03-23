using Microsoft.Extensions.Logging;

namespace Unit.Utils;

public class MockLogger<T> : ILogger<T>
{
    public List<(LogLevel Level, string Message)> LoggedMessages { get; } = new();

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        LoggedMessages.Add((logLevel, formatter(state, exception)));
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
}
