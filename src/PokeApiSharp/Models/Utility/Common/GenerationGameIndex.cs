namespace PokeApiSharp;

/// <summary>
/// Represents a game index for an API resource within a specific generation.
/// </summary>
/// <param name="GameIndex">The internal id of an API resource within game data.</param>
/// <param name="Generation">The generation relevent to this game index.</param>
public record GenerationGameIndex(
    int GameIndex,
    NamedApiResource<Generation> Generation
);