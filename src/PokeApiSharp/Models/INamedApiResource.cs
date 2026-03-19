namespace PokeApiSharp;

/// <summary>
/// Represents an entity with a name and a URL, which provides access to
/// additional details about the entity in an API.
/// </summary>
public interface INamedApiResource
{
    /// <summary>
    /// Gets the name of the API resource. This name is a unique identifier
    /// for the resource and is commonly used in API endpoints or as reference
    /// within the context of the application.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the URL of the API resource. This URL serves as a link to access
    /// further details or actions related to the resource within the API's context.
    /// </summary>
    string Url { get; }
}