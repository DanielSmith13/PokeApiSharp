namespace PokeApiSharp.Monads;

/// <summary>
/// Represents a monadic container that can hold either a success value or a failure value.
/// </summary>
/// <typeparam name="TSuccess">The type of the success value.</typeparam>
/// <typeparam name="TFailure">The type of the failure value.</typeparam>
public class Either<TSuccess, TFailure>
{
    private readonly TSuccess? _success;
    private readonly TFailure? _failure;

    /// <summary>
    /// Indicates whether the instance represents a successful outcome.
    /// </summary>
    /// <remarks>
    /// This property is set to true when the instance contains a success value,
    /// and false when it contains a failure value. It is primarily used to
    /// determine the state of the Either monadic container.
    /// </remarks>
    public bool IsSuccess { get; private set; }

    /// <summary>
    /// Indicates whether the instance represents a failure outcome.
    /// </summary>
    /// <remarks>
    /// This property is set to true when the instance contains a failure value,
    /// and false when it contains a success value. It provides a straightforward
    /// way to determine if the Either monadic container represents a failure state.
    /// </remarks>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Creates an <see cref="Either{TSuccess, TFailure}"/> representing a successful result.
    /// </summary>
    /// <param name="success">The success value to store in the container.</param>
    public Either(TSuccess success)
    {
        _success = success;
        _failure = default;
        IsSuccess = true;
    }

    /// <summary>
    /// Creates an <see cref="Either{TSuccess, TFailure}"/> representing a failure result.
    /// </summary>
    /// <param name="failure">The failure value to store in the container.</param>
    public Either(TFailure failure)
    {
        _failure = failure;
        _success = default;
        IsSuccess = false;
    }

    /// <summary>
    /// Applies one of two provided functions based on the state of the <see cref="Either{TSuccess, TFailure}"/> instance,
    /// returning a value of the specified type.
    /// </summary>
    /// <typeparam name="TReturn">The type of the result produced by the provided functions.</typeparam>
    /// <param name="onSuccess">The function to invoke if the <see cref="Either{TSuccess, TFailure}"/> instance represents a success.</param>
    /// <param name="onFailure">The function to invoke if the <see cref="Either{TSuccess, TFailure}"/> instance represents a failure.</param>
    /// <returns>The result of invoking the appropriate function based on the state of the <see cref="Either{TSuccess, TFailure}"/> instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the <see cref="Either{TSuccess, TFailure}"/> instance is in an invalid state.</exception>
    public TReturn Match<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure)
    {
        if (IsSuccess && _success != null)
            return onSuccess(_success);
        if (IsFailure && _failure != null)
            return onFailure(_failure);
        throw new InvalidOperationException("Either is in an invalid state.");
    }
}