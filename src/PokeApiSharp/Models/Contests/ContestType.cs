using System.Text.Json.Serialization;
using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Contest types are categories judges used to weigh a Pokémon's condition in Pokémon contests.
/// Check out <see href="https://bulbapedia.bulbagarden.net/wiki/Contest_condition">Bulbapedia</see> for greater detail.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="BerryFlavour">The berry flavour that correlates with this contest type.</param>
/// <param name="Names">The name of this contest type listed in different languages.</param>
[PokeApiResource("contest-type")]
public record Contest(
    int Id,
    string Name,
    [property: JsonPropertyName("berry_flavor")]NamedApiResource<BerryFlavour> BerryFlavour,
    IEnumerable<ContestName> Names);

/// <summary>
/// The name of this contest type listed in different languages.
/// </summary>
/// <param name="Name">The name for this contest.</param>
/// <param name="Colour">The colour associated with this contest type.</param>
/// <param name="Language">The language this name is in.</param>
public record ContestName(
    string Name,
    [property: JsonPropertyName("color")]string Colour,
    NamedApiResource<Language> Language
);