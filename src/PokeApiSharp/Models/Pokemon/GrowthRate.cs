using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Growth rates are the speed with which Pokémon gain levels through experience. Check out
/// <see href="https://bulbapedia.bulbagarden.net/wiki/Experience">Bulbapedia</see>
/// for greater detail.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Formula">The formula used to calculate the rate at which the Pokémon species gains level.</param>
/// <param name="Descriptions">The descriptions of this characteristic listed in different languages.</param>
/// <param name="Levels">A list of levels and the amount of experience needed to reach them based on this growth rate.</param>
/// <param name="PokemonSpecies">A list of Pokémon species that gain levels at this growth rate.</param>
[PokeApiResource("growth-rate")]
public record GrowthRate(
    int Id,
    string Name,
    string Formula,
    IEnumerable<DescriptionEntry> Descriptions,
    IEnumerable<GrowthRateExperienceLevel> Levels,
    IEnumerable<NamedApiResource<PokemonSpecies>> PokemonSpecies
);

/// <summary>
/// A level and the amount of experience required to reach it based on a growth rate.
/// </summary>
/// <param name="Level">The level gained.</param>
/// <param name="Experience">The amount of experience required to reach the referenced level.</param>
public record  GrowthRateExperienceLevel(
    int Level,
    int Experience
);