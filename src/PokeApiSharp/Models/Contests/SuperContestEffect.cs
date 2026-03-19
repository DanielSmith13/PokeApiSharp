using System.Text.Json.Serialization;
using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Super contest effects refer to the effects of moves when used in super contests.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Appeal">The level of appeal this super contest effect has.</param>
/// <param name="FlavourTextEntries">The flavour text of this super contest effect listed in different languages.</param>
/// <param name="Moves">A list of moves that have the effect when used in super contests.</param>
[PokeApiResource("super-contest-effect")]
public record SuperContestEffect(
    int Id,
    int Appeal,
    [property: JsonPropertyName("flavor_text_entries")]IEnumerable<FlavourTextEntry> FlavourTextEntries,
    IEnumerable<NamedApiResource<Move>> Moves);