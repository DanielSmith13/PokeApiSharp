using PokeApiSharp.Monads;

namespace Unit.Monads
{
    public class EitherTests
    {
        [Fact]
        public void Success_ConstructsAndMatches()
        {
            var either = new Either<int, string>(42);

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
            var either = new Either<int, string>("oops");

            Assert.False(either.IsSuccess);
            Assert.True(either.IsFailure);

            var result = either.Match(
                _ => 0,
                failure => failure.Length
            );

            Assert.Equal(4, result);
        }

        [Fact]
        public void Match_Throws_WhenSuccessIsNull()
        {
            var either = new Either<string?, int>(null);

            Assert.True(either.IsSuccess);

            Assert.Throws<InvalidOperationException>(() => either.Match(
                success => success?.Length,
                _ => 0
            ));
        }

        [Fact]
        public void Match_Throws_WhenFailureIsNull()
        {
            var either = new Either<int, string?>(null);

            Assert.False(either.IsSuccess);

            Assert.Throws<InvalidOperationException>(() => either.Match(
                _ => 0,
                failure => failure?.Length
            ));
        }

        [Fact]
        public void ValueTypes_AreHandledCorrectly()
        {
            var successEither = new Either<int, string>(0);
            var successResult = successEither.Match(s => s, _ => -1);
            Assert.Equal(0, successResult);

            var failureEither = new Either<int, long>(-5L);
            var failureResult = failureEither.Match(_ => 0L, f => f + 1);
            Assert.Equal(-4L, failureResult);
        }
    }
}

