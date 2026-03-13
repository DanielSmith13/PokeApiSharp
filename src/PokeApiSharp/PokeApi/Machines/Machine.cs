using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Games;
using PokeApiSharp.PokeApi.Items;
using PokeApiSharp.PokeApi.Moves;

namespace PokeApiSharp.PokeApi.Machines;

[PokeApiResource("machine")]
public record Machine(
    int Id,
    NamedApiResource<Item> Item,
    NamedApiResource<Move> Move,
    NamedApiResource<VersionGroup> VersionGroup);