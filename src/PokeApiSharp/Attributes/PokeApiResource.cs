namespace PokeApiSharp.Attributes;

/// <summary>
/// Attribute to specify the API endpoint path for a given resource class or struct.
/// This allows for easy mapping between the class/struct and its corresponding API
/// endpoint when making requests to the PokeAPI.
/// </summary>
/// <param name="path"></param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
public sealed class PokeApiResource(string path) : Attribute
{
    /// <summary>
    /// Gets the API endpoint path associated with the resource class or struct.
    /// </summary>
    public string Path { get; } = path;
}