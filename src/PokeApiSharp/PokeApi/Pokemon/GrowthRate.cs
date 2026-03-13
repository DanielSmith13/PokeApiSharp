using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Pokemon;

[PokeApiResource("growth-rate")]
public record GrowthRate(
    int Id,
    string Name,
    string Formula,
    IEnumerable<DescriptionEntry> Descriptions,
    IEnumerable<GrowthRateExperienceLevel> Levels,
    IEnumerable<NamedApiResource<PokemonSpecies>> PokemonSpecies
);

public record  GrowthRateExperienceLevel(
    int Level,
    int Experience
);