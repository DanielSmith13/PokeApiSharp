namespace PokeApiSharp.Monads;

public class Either<TSuccess, TFailure>
{
    private readonly TSuccess? _success;
    private readonly TFailure? _failure;
    
    public bool IsSuccess { get; private set; }
    public bool IsFailure => !IsSuccess;
    
    public Either(TSuccess success)
    {
        _success = success;
        _failure = default;
        IsSuccess = true;
    }
    
    public Either(TFailure failure)
    {
        _failure = failure;
        _success = default;
        IsSuccess = false;
    }
    
    public TReturn Match<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure)
    {
        if (IsSuccess && _success != null)
            return onSuccess(_success);
        if (IsFailure && _failure != null)
            return onFailure(_failure);
        throw new InvalidOperationException("Either is in an invalid state.");
    }
}