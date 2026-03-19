using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Methods by which the player might encounter Pokémon in the wild,
/// e.g. walking in tall grass. Check out
/// <see href="https://bulbapedia.bulbagarden.net/wiki/Wild_Pok%C3%A9mon">Bulbapedia</see>
/// for greater detail.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Order">A good value for sorting.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
[PokeApiResource("encounter-method")]
public record EncounterMethod(
    int Id,
    string Name,
    int Order,
    IEnumerable<NameEntry> Names
);