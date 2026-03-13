using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Evolutions;
using PokeApiSharp.PokeApi.Utility.Common;
using Version = PokeApiSharp.PokeApi.Games.Version;

namespace PokeApiSharp.PokeApi.Items;

[PokeApiResource("item")]
public record Item(
    int Id,
    string Name,
    int Cost,
    int FlingPower,
    NamedApiResource<ItemFlingEffect> FlingEffect,
    IEnumerable<NamedApiResource<ItemAttribute>> Attributes,
    NamedApiResource<ItemCategory> Category,
    IEnumerable<VerboseEffect> EffectEntries,
    IEnumerable<VersionGroupFlavourText> FlavourTextEntries,
    IEnumerable<GenerationGameIndex> GameIndices,
    IEnumerable<NameEntry> Names,
    ItemSprites Sprites,
    IEnumerable<ItemHolderPokemon> HeldByPokemon,
    ApiResource<EvolutionChain> BabyTriggerFor,
    IEnumerable<MachineVersionDetail> Machines);

public record ItemSprites(
    string Default);

public record ItemHolderPokemon(
    NamedApiResource<Pokemon.Pokemon> Pokemon,
    IEnumerable<ItemHolderPokemonVersionDetail> VersionDetails);

public record ItemHolderPokemonVersionDetail(
    int Rarity,
    NamedApiResource<Version> Version);