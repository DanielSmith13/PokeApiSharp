using System.Text.Json.Serialization;
using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// An item is an object in the games which the player can pick up,
/// keep in their bag, and use in some manner. They have various uses,
/// including healing, powering up, helping catch Pokémon, or to access a new area.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="Cost">The price of this item in stores.</param>
/// <param name="FlingPower">The power of the move Fling when used with this item.</param>
/// <param name="FlingEffect">The effect of the move Fling when used with this item.</param>
/// <param name="Attributes">A list of attributes this item has.</param>
/// <param name="Category">The category of items this item falls into.</param>
/// <param name="EffectEntries">The effect of this item listed in different languages.</param>
/// <param name="FlavourTextEntries">The flavour text of this ability listed in different languages.</param>
/// <param name="GameIndices">A list of game indices relevant to this item by generation.</param>
/// <param name="Names">The name of this item listed in different languages.</param>
/// <param name="Sprites">A set of sprites used to depict this item in the game.</param>
/// <param name="HeldByPokemon">A list of Pokémon that might be found in the wild holding this item.</param>
/// <param name="BabyTriggerFor">An evolution chain this item requires to produce a bay during mating.</param>
/// <param name="Machines">A list of the machines related to this item.</param>
[PokeApiResource("item")]
public record Item(
    int Id,
    string Name,
    int Cost,
    int? FlingPower,
    NamedApiResource<ItemFlingEffect>? FlingEffect,
    IEnumerable<NamedApiResource<ItemAttribute>> Attributes,
    NamedApiResource<ItemCategory> Category,
    IEnumerable<VerboseEffect> EffectEntries,
    [property: JsonPropertyName("flavor_text_entries")]IEnumerable<VersionGroupFlavourText> FlavourTextEntries,
    IEnumerable<GenerationGameIndex> GameIndices,
    IEnumerable<NameEntry> Names,
    ItemSprites Sprites,
    IEnumerable<ItemHolderPokemon> HeldByPokemon,
    ApiResource<EvolutionChain>? BabyTriggerFor,
    IEnumerable<MachineVersionDetail> Machines);

/// <summary>
/// A set of sprites used to depict this item in the game.
/// </summary>
/// <param name="Default">The default depiction of this item.</param>
public record ItemSprites(
    string? Default);

/// <summary>
/// Details about the Pokémon that holds this item.
/// </summary>
/// <param name="Pokemon">The Pokémon that holds this item.</param>
/// <param name="VersionDetails">The details for the version that this item is held in by the Pokémon.</param>
public record ItemHolderPokemon(
    NamedApiResource<Pokemon> Pokemon,
    IEnumerable<ItemHolderPokemonVersionDetail> VersionDetails);

/// <summary>
/// Details about the rarity of the Pokémon holding this item in a specific version.
/// </summary>
/// <param name="Rarity">How often this Pokémon holds this item in this version.</param>
/// <param name="Version">The version that this item is held in by the Pokémon.</param>
public record ItemHolderPokemonVersionDetail(
    int Rarity,
    NamedApiResource<Version> Version);