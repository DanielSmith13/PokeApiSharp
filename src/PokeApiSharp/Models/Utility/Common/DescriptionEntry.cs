namespace PokeApiSharp;

/// <summary>
/// Represents a description of an API resource in a specific language.
/// </summary>
/// <param name="Description">The localised description for an API resource in a specific language.</param>
/// <param name="Language">The language this name is in.</param>
public record DescriptionEntry(
    string Description,
    NamedApiResource<Language> Language
);