namespace PokeApiSharp;

/// <summary>
/// Represents a reference to a resource within the API.
/// This interface provides access to the URL of the resource,
/// allowing retrieval of related data or additional information.
/// </summary>
public interface IApiResource
{
    /// <summary>
    /// The URL of the referenced resource.
    /// </summary>
    public string Url { get; }
}