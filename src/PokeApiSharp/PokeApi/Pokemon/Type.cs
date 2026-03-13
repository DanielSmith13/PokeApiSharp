using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Games;
using PokeApiSharp.PokeApi.Moves;
using PokeApiSharp.PokeApi.Utility.Common;

namespace PokeApiSharp.PokeApi.Pokemon;

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

public record TypePokemon(
    int Slot,
    NamedApiResource<Pokemon> Pokemon
);

public record TypeRelation(
    IEnumerable<NamedApiResource<Type>> NoDamageTo,
    IEnumerable<NamedApiResource<Type>> HalfDamageTo,
    IEnumerable<NamedApiResource<Type>> DoubleDamageTo,
    IEnumerable<NamedApiResource<Type>> NoDamageFrom,
    IEnumerable<NamedApiResource<Type>> HalfDamageFrom,
    IEnumerable<NamedApiResource<Type>> DoubleDamageFrom
);

public record TypeRelationsPast(
    NamedApiResource<Generation> Generation,
    TypeRelation DamageRelations
);