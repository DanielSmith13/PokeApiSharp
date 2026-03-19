namespace PokeApiSharp;

/// <summary>
/// Represents a reference to a named API resource, containing both the
/// resource's name and a URL providing access to further details about the resource.
/// </summary>
/// <typeparam name="T">
/// The type of the resource that this reference points to, for example, a specific Pokémon species or move.
/// </typeparam>
/// <param name="Name">The name of the resource.</param>
/// <param name="Url">The URL to access further details about the resource.</param>
public record NamedApiResource<T>(string Name, string Url) : INamedApiResource;