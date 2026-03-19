using System.Text.Json.Serialization;
using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Berries are small fruits that can provide HP and status condition restoration,
/// stat enhancement, and even damage negation when eaten by Pokémon.
/// Check out <see href="https://bulbapedia.bulbagarden.net/wiki/Berry">Bulbapedia</see> for greater detail.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="GrowthTime">The time it takes the tree to grow one stage, in hours. Berry trees go through four of these growth stages before they can be picked.</param>
/// <param name="MaxHarvest">The maximum number of these berries that can grow on one tree in Generation IV.</param>
/// <param name="NaturalGiftPower">The power of the move "Natural Gift" when used with this Berry.</param>
/// <param name="Size">The size of this Berry, in millimeters.</param>
/// <param name="Smoothness">How much the Berry's firmness affects the severity of poisoning effects.</param>
/// <param name="SoilDryness">The soil dryness of this Berry, as a percentage, affecting the speed of the tree's fully grown cycle.</param>
/// <param name="Firmness">The firmness of this berry, used in making Pokéblocks or Poffins.</param>
/// <param name="Flavours">A list of references to each flavour a berry can have and the potency of each of those flavours in regard to this berry.</param>
/// <param name="Item">Berries are actually items. This is a reference to the item-specific data for this berry.</param>
/// <param name="NaturalGiftType">The type inherited by "Natural Gift" when used with this Berry.</param>
[PokeApiResource("berry")]
public record Berry(
    int Id,
    string Name,
    int GrowthTime,
    int MaxHarvest,
    int NaturalGiftPower,
    int Size,
    int Smoothness,
    int SoilDryness,
    NamedApiResource<BerryFirmness> Firmness,
    [property: JsonPropertyName("flavors")]IEnumerable<BerryFlavourMap> Flavours,
    NamedApiResource<Item> Item,
    NamedApiResource<Type> NaturalGiftType);
    
/// <summary>
/// A list of references to each flavour a berry can have and the potency of each of those flavours in regard to this berry.
/// </summary>
/// <param name="Potency">How powerful the referenced flavour is for this berry.</param>
/// <param name="Flavour">The referenced berry flavour.</param>
public record BerryFlavourMap(
    int Potency,
    [property: JsonPropertyName("flavor")]NamedApiResource<BerryFlavour> Flavour
);
