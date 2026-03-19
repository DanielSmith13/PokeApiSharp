using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Flavours determine whether a Pokémon will benefit or suffer from eating a berry based on their nature.
/// Check out <see href="https://bulbapedia.bulbagarden.net/wiki/Flavor">Bulbapedia</see> for greater detail.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Berries">A list of references to each berry that has this flavour and the potency of that flavour.</param>
/// <param name="ContestType">The contest type associated with this berry flavour.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
[PokeApiResource("berry-flavor")]
public record BerryFlavour(
    int Id,
    string Name,
    IEnumerable<FlavourBerryMap> Berries,
    NamedApiResource<ContestType> ContestType,
    IEnumerable<NameEntry> Names);
    
/// <summary>
/// A list of references to each berry that has this flavour and the potency of that flavour.
/// </summary>
/// <param name="Potency">How powerful the referenced flavour is for this berry.</param>
/// <param name="Berry">The berry with the referenced flavour.</param>
public record FlavourBerryMap(
    int Potency,
    NamedApiResource<Berry> Berry
);