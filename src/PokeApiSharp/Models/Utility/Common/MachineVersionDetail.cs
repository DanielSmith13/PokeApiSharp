namespace PokeApiSharp;

/// <summary>
/// Represents a machine version detail for a move taught by an item.
/// </summary>
/// <param name="Machine">The machine that teaches a move from an item.</param>
/// <param name="VersionGroup">The version group of this specific machine.</param>
public record MachineVersionDetail(
    ApiResource<Machine> Machine,
    NamedApiResource<VersionGroup> VersionGroup);