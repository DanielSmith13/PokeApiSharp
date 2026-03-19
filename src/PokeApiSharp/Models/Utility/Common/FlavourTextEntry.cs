using System.Text.Json.Serialization;

namespace PokeApiSharp;

/// <summary>
/// Represents a flavour text entry for an API resource in a specific language and game version.
/// </summary>
/// <param name="FlavourText">The localised flavour text for an API resource in a specific language.</param>
/// <param name="Language">The language this flavour text is in.</param>
/// <param name="Version">The game version this flavour text is extracted from.</param>
public record FlavourTextEntry(
    [property: JsonPropertyName("flavor_text")]string FlavourText,
    NamedApiResource<Language> Language,
    NamedApiResource<Version> Version
);