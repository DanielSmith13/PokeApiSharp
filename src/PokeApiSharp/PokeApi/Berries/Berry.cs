using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Items;
using PokeApiSharp.PokeApi.Pokemon;

namespace PokeApiSharp.PokeApi.Berries;

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
    IEnumerable<BerryFlavourMap> Flavours,
    NamedApiResource<Item> Item,
    NamedApiResource<Type> NaturalGiftType);
    
public record BerryFlavourMap(
    int Potency,
    NamedApiResource<BerryFlavour> Flavour
);
