namespace PokeApiSharp;

/// <summary>
/// Represents a reference to another API resource, containing only the URL to that resource.
/// This is used in various places in the PokeAPI to link related resources without including their full data.
/// </summary>
/// <param name="Url">The URL of the referenced resource.</param>
/// <typeparam name="T">The type of the referenced resource.</typeparam>
public record ApiResource<T>(string Url);