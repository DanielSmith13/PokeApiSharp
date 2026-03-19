namespace PokeApiSharp;

/// <summary>
/// Represents a game index entry for an API resource in a specific version.
/// </summary>
/// <param name="GameIndex">The internal id of an API resource within game data.</param>
/// <param name="Version">The version relevent to this game index.</param>
public record VersionGameIndex(
    int GameIndex,
    NamedApiResource<Version> Version
);