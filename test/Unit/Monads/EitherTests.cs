using PokeApiSharp.Monads;

namespace Unit.Monads;

public class EitherTests
{
    [Fact]
    public void Success_ConstructsAndMatches()
    {
        Either<int, string> either = 42;

        Assert.True(either.IsSuccess);
        Assert.False(either.IsFailure);

        var result = either.Match(
            success => success + 1,
            _ => -1
        );

        Assert.Equal(43, result);
    }

    [Fact]
    public void Failure_ConstructsAndMatches()
    {
        Either<int, string> either = "oops";

        Assert.False(either.IsSuccess);
        Assert.True(either.IsFailure);

        var result = either.Match(
            _ => 0,
            failure => failure.Length
        );

        Assert.Equal(4, result);
    }

    [Fact]
    public void ValueTypes_AreHandledCorrectly()
    {
        Either<int, string> successEither = 0;
        var successResult = successEither.Match(s => s, _ => -1);
        Assert.Equal(0, successResult);

        Either<int, long> failureEither = -5L;
        var failureResult = failureEither.Match(_ => 0L, f => f + 1);
        Assert.Equal(-4L, failureResult);
    }
}