using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Types are properties for Pokémon and their moves.
/// Each type has three properties: which types of Pokémon
/// it is super effective against, which types of Pokémon
/// it is not very effective against, and which types of Pokémon
/// it is completely ineffective against.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Name">The name for this resource.</param>
/// <param name="DamageRelations">A detail of how effective this type is toward others and vice versa.</param>
/// <param name="PastDamageRelations">
/// A list of details of how effective this type was
/// toward others and vice versa in previous generations
/// </param>
/// <param name="GameIndices">A list of game indices relevant to this item by generation.</param>
/// <param name="Generation">The generation that this type was introduced in.</param>
/// <param name="MoveDamageClass">The class of damage inflicted by this type.</param>
/// <param name="Names">The name of this resource listed in different languages.</param>
/// <param name="Pokemon">A list of details of Pokémon that have this type.</param>
/// <param name="Moves">A list of moves that have this type.</param>
[PokeApiResource("type")]
public record Type(
    int Id,
    string Name,
    IEnumerable<TypeRelation> DamageRelations,
    IEnumerable<TypeRelationsPast> PastDamageRelations,
    IEnumerable<GenerationGameIndex> GameIndices,
    NamedApiResource<Generation> Generation,
    NamedApiResource<MoveDamageClass> MoveDamageClass,
    IEnumerable<NameEntry> Names,
    IEnumerable<TypePokemon> Pokemon,
    IEnumerable<NamedApiResource<Move>> Moves
);

/// <summary>
/// A reference to Pokémon that have the referenced type.
/// </summary>
/// <param name="Slot">The order the Pokémon's types are listed in.</param>
/// <param name="Pokemon">The Pokémon that has the referenced type.</param>
public record TypePokemon(
    int Slot,
    NamedApiResource<Pokemon> Pokemon
);

/// <summary>
/// A detail of how effective this type is toward others and vice versa.
/// </summary>
/// <param name="NoDamageTo">A list of types this type has no effect on.</param>
/// <param name="HalfDamageTo">A list of types this type is not very effective against.</param>
/// <param name="DoubleDamageTo">A list of types this type is very effective against.</param>
/// <param name="NoDamageFrom">A list of types that have no effect against this type.</param>
/// <param name="HalfDamageFrom">A list of types that are not very effective against this type.</param>
/// <param name="DoubleDamageFrom">A list of types that are very effective against this type.</param>
public record TypeRelation(
    IEnumerable<NamedApiResource<Type>> NoDamageTo,
    IEnumerable<NamedApiResource<Type>> HalfDamageTo,
    IEnumerable<NamedApiResource<Type>> DoubleDamageTo,
    IEnumerable<NamedApiResource<Type>> NoDamageFrom,
    IEnumerable<NamedApiResource<Type>> HalfDamageFrom,
    IEnumerable<NamedApiResource<Type>> DoubleDamageFrom
);

/// <summary>
/// A detail of how effective this type was toward
/// others and vice versa in previous generations.
/// </summary>
/// <param name="Generation">The last generation in which the referenced type had the listed damage relations</param>
/// <param name="DamageRelations">The damage relations the referenced type had up to and including the listed generation</param>
public record TypeRelationsPast(
    NamedApiResource<Generation> Generation,
    TypeRelation DamageRelations
);