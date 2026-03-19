using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Characteristics indicate which stat contains a Pokémon's highest IV.
/// A Pokémon's Characteristic is determined by the remainder of its highest
/// IV divided by 5 (gene_modulo). Check out
/// <see href="https://bulbapedia.bulbagarden.net/wiki/Characteristic">Bulbapedia</see> for greater detail.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="GeneModulo">The remainder of the highest stat/IV divided by 5.</param>
/// <param name="PossibleValues">
/// The possible values of the highest stat that would result in a Pokémon
/// receiving this characteristic when divided by 5.
/// </param>
/// <param name="HighestStat">The stat which results in this characteristic.</param>
/// <param name="Descriptions">The descriptions of this characteristic listed in different languages.</param>
[PokeApiResource("characteristic")]
public record Characteristic(
    int Id,
    int GeneModulo,
    IEnumerable<int> PossibleValues,
    NamedApiResource<Stat> HighestStat,
    IEnumerable<DescriptionEntry> Descriptions);