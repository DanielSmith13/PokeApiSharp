using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Berries can be soft or hard. Check out <see href="https://bulbapedia.bulbagarden.net/wiki/Category:Berries_by_firmness">Bulbapedia</see> for greater detail.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Berries">A list of the berries with this firmness.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
[PokeApiResource("berry-firmness")]
public record BerryFirmness(
    int Id,
    string Name,
    IEnumerable<NamedApiResource<Berry>> Berries,
    IEnumerable<NameEntry> Names
);