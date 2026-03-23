using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Pokémon are the creatures that inhabit the world of the Pokémon games.
/// They can be caught using Pokéballs and trained by battling with other Pokémon.
/// Each Pokémon belongs to a specific species but may take on a variant which
/// makes it differ from other Pokémon of the same species, such as base stats,
/// available abilities, and typings. See
/// <see href="https://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_(species)">Bulbapedia</see>
/// for greater detail.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="BaseExperience">The base experience gained for defeating this Pokémon.</param>
/// <param name="Height">The height of this Pokémon in decimetres.</param>
/// <param name="IsDefault">Set for exactly one Pokémon used as the default for each species.</param>
/// <param name="Order">Order for sorting. Almost national order, except families are grouped together.</param>
/// <param name="Weight">The weight of this Pokémon in hectograms.</param>
/// <param name="Abilities">A list of abilities this Pokémon could potentially have.</param>
/// <param name="Forms">A list of forms this Pokémon can take on.</param>
/// <param name="GameIndices">A list of game indices relevant to Pokémon item by generation.</param>
/// <param name="HeldItems">A list of items this Pokémon may be holding when encountered.</param>
/// <param name="LocationAreaEncounters">A link to a list of location areas, as well as encounter details pertaining to specific versions.</param>
/// <param name="Moves">A list of moves along with learn methods and level details pertaining to specific version groups.</param>
/// <param name="PastTypes">A list of details showing types this Pokémon had in previous generations.</param>
/// <param name="PastAbilities">A list of details showing abilities this Pokémon had in previous generations.</param>
/// <param name="PastStats">A list of details showing stats this Pokémon had in previous generations.</param>
/// <param name="Sprites">
/// A set of sprites used to depict this Pokémon in the game.
/// A visual representation of the various sprites can be found at
/// <see href="https://github.com/PokeAPI/sprites#sprites">PokeAPI/sprites</see>.
/// </param>
/// <param name="Cries">
/// A set of cries used to depict this Pokémon in the game.
/// A visual representation of the various cries can be found at
/// <see href="https://github.com/PokeAPI/cries#cries">PokeAPI/cries</see>.
/// </param>
/// <param name="Species">The species this Pokémon belongs to.</param>
/// <param name="Stats">A list of base stat values for this Pokémon.</param>
/// <param name="Types">A list of details showing types this Pokémon has.</param>
[PokeApiResource("pokemon")]
public record Pokemon(
    int Id,
    string Name,
    int BaseExperience,
    int Height,
    bool IsDefault,
    int Order,
    int Weight,
    IEnumerable<PokemonAbility> Abilities,
    IEnumerable<NamedApiResource<PokemonForm>> Forms,
    IEnumerable<VersionGameIndex> GameIndices,
    IEnumerable<PokemonHeldItem> HeldItems,
    string LocationAreaEncounters,
    IEnumerable<PokemonMove> Moves,
    IEnumerable<PokemonTypePast> PastTypes,
    IEnumerable<PokemonAbilityPast> PastAbilities,
    IEnumerable<PokemonStatPast> PastStats,
    PokemonSprites Sprites,
    PokemonCries Cries,
    NamedApiResource<PokemonSpecies> Species,
    IEnumerable<PokemonStat> Stats,
    IEnumerable<PokemonType> Types
);

/// <summary>
/// Represents an ability of a Pokémon.
/// </summary>
/// <param name="IsHidden">Whether this is a hidden ability.</param>
/// <param name="Slot">The slot this ability occupies in this Pokémon species.</param>
/// <param name="Ability">The ability the Pokémon may have.</param>
public record PokemonAbility(
    bool IsHidden,
    int Slot,
    NamedApiResource<Ability> Ability
);

/// <summary>
/// Represents an item held by a Pokémon.
/// </summary>
/// <param name="Item">The item the referenced Pokémon holds.</param>
/// <param name="VersionDetails">The details of the different versions in which the item is held.</param>
public record PokemonHeldItem(
    NamedApiResource<Item> Item,
    IEnumerable<PokemonHeldItemVersion> VersionDetails
);

/// <summary>
/// Represents the rarity and version details of an item held by a Pokémon.
/// </summary>
/// <param name="Rarity">How often the item is held.</param>
/// <param name="Version">The version in which the item is held.</param>
public record PokemonHeldItemVersion(
    int Rarity,
    NamedApiResource<Version> Version
);

/// <summary>
/// Represents a move that a Pokémon can learn.
/// </summary>
/// <param name="Move">The move the Pokémon can learn.</param>
/// <param name="VersionGroupDetails">The details of the version in which the Pokémon can learn the move.</param>
public record PokemonMove(
    NamedApiResource<Move> Move,
    IEnumerable<PokemonMoveVersion> VersionGroupDetails
);

/// <summary>
/// Represents the details of how a Pokémon learns a move.
/// </summary>
/// <param name="MoveLearnMethod">The method by which the move is learned.</param>
/// <param name="VersionGroup">The version group in which the move is learned.</param>
/// <param name="LevelLearnedAt">The minimum level to learn the move.</param>
/// <param name="Order">
/// Order by which the Pokémon will learn the move.
/// A newly learnt move will replace the move with the lowest order.
/// </param>
public record PokemonMoveVersion(
    NamedApiResource<MoveLearnMethod> MoveLearnMethod,
    NamedApiResource<VersionGroup> VersionGroup,
    int? LevelLearnedAt,
    int? Order
);

/// <summary>
/// Represents the types a Pokémon had in a specific generation.
/// </summary>
/// <param name="Types">
/// The types the referenced Pokémon had up to and including the listed generation.
/// </param>
/// <param name="Generation">
/// The last generation in which the referenced Pokémon had the listed types.
/// </param>
public record PokemonTypePast(
    IEnumerable<PokemonType> Types,
    NamedApiResource<Generation> Generation
);

/// <summary>
/// Represents a type a Pokémon has.
/// </summary>
/// <param name="Slot">The order the Pokémon's types are listed in.</param>
/// <param name="Type">The type the referenced Pokémon has.</param>
public record PokemonType(
    int Slot,
    NamedApiResource<Type> Type
);

/// <summary>
/// Represents the abilities a Pokémon had in a specific generation.
/// </summary>
/// <param name="Abilities">
/// The last generation in which the referenced Pokémon had the listed abilities.
/// </param>
/// <param name="Generation">
/// The abilities the referenced Pokémon had up to and including the listed generation.
/// If null, the slot was previously empty.
/// </param>
public record PokemonAbilityPast(
    IEnumerable<PokemonAbility> Abilities,
    NamedApiResource<Generation> Generation
);

/// <summary>
/// Represents the stats a Pokémon had in a specific generation.
/// </summary>
/// <param name="Generation">The last generation in which the referenced Pokémon had the listed stats.</param>
/// <param name="Stats">The stat the Pokémon had up to and including the listed generation.</param>
public record PokemonStatPast(
    NamedApiResource<Generation> Generation,
    IEnumerable<PokemonStat> Stats
);

/// <summary>
/// Represents the cries used to depict a Pokémon in the game.
/// </summary>
/// <param name="Latest">The latest depiction of this Pokémon's cry.</param>
/// <param name="Legacy">The legacy depiction of this Pokémon's cry.</param>
public record PokemonCries(
    string Latest,
    string Legacy
);

/// <summary>
/// Represents the stats a Pokémon has in the game.
/// </summary>
/// <param name="BaseStat">The base value of the stat.</param>
/// <param name="Effort">The effort points (EV) the Pokémon has in the stat.</param>
/// <param name="Stat">The stat the Pokémon has.</param>
public record PokemonStat(
    int BaseStat,
    int Effort,
    NamedApiResource<Stat> Stat
);