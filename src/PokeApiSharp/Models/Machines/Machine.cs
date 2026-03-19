using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Machines are the representation of items that teach moves to Pokémon.
/// They vary from version to version, so it is not certain that one specific
/// TM or HM corresponds to a single Machine.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="Item">The TM or HM item that corresponds to this machine.</param>
/// <param name="Move">The move that is taught by this machine.</param>
/// <param name="VersionGroup">The version group that this machine applies to.</param>
[PokeApiResource("machine")]
public record Machine(
    int Id,
    NamedApiResource<Item> Item,
    NamedApiResource<Move> Move,
    NamedApiResource<VersionGroup> VersionGroup);