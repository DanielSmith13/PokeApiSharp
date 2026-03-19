namespace PokeApiSharp;

/// <summary>
/// Represents a name entry for an API resource in a specific language.
/// </summary>
/// <param name="Name">The localised name for an API resource in a specific language.</param>
/// <param name="Language">The language this name is in.</param>
public record NameEntry(
    string Name,
    NamedApiResource<Language> Language
);