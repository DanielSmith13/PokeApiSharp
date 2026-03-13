using PokeApiSharp.PokeApi.Games;
using PokeApiSharp.PokeApi.Machines;

namespace PokeApiSharp.PokeApi.Utility.Common;

public record MachineVersionDetail(
    ApiResource<Machine> Machine,
    NamedApiResource<VersionGroup> VersionGroup);