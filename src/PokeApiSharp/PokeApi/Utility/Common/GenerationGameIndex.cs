using PokeApiSharp.PokeApi.Games;

namespace PokeApiSharp.PokeApi.Utility.Common;

public record GenerationGameIndex(
    int GameIndex,
    NamedApiResource<Generation> Generation
);