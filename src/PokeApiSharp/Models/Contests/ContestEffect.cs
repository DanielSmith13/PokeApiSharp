using System.Text.Json.Serialization;
using PokeApiSharp.Attributes;

namespace PokeApiSharp;
/// <summary>
/// Contest effects refer to the effects of moves when used in contests.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Appeal">The base number of hearts the user of this move gets.</param>
/// <param name="Jam">The base number of hearts the user's opponent loses.</param>
/// <param name="EffectEntries">The result of this contest effect listed in different languages.</param>
/// <param name="FlavourTextEntries">The flavour text of this contest effect listed in different languages.</param>
[PokeApiResource("contest-effect")]
public record ContestEffect(
    int Id,
    int Appeal,
    int Jam,
    IEnumerable<EffectEntry> EffectEntries,
    [property: JsonPropertyName("flavor_text_entries")]IEnumerable<FlavourTextEntry> FlavourTextEntries);