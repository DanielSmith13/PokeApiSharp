namespace PokeApiSharp;

/// <summary>
/// Represents a flavour text entry for an API resource in a specific version group.
/// </summary>
/// <param name="Text">The localised name for an API resource in a specific language.</param>
/// <param name="Language">The language this name is in.</param>
/// <param name="VersionGroup">The version group which uses this flavour text.</param>
public record VersionGroupFlavourText(
    string Text,
    NamedApiResource<Language> Language,
    NamedApiResource<VersionGroup> VersionGroup);