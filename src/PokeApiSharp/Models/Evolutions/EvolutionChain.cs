using PokeApiSharp.Attributes;

namespace PokeApiSharp;

/// <summary>
/// Evolution chains are essentially family trees.
/// They start with the lowest stage within a family
/// and detail evolution conditions for each as well
/// as Pokémon they can evolve into up through the hierarchy.
/// </summary>
/// <param name="Id">The identifier for this resource.</param>
/// <param name="BabyTriggerItem">The item that a Pokémon would be holding when mating that would trigger the egg hatching a baby Pokémon rather than a basic Pokémon.</param>
/// <param name="Chain">The base chain link object. Each link contains evolution details for a Pokémon in the chain. Each link references the next Pokémon in the natural evolution order.</param>
[PokeApiResource("evolution-chain")]
public record EvolutionChain(
    int Id,
    NamedApiResource<Item> BabyTriggerItem,
    ChainLink Chain);
    
/// <summary>
/// A chain link object. Each link contains evolution details for a Pokémon in the chain. Each link references the next Pokémon in the natural evolution order.
/// </summary>
/// <param name="IsBaby">Whether this link is for a baby Pokémon. This would only ever be true on the base link.</param>
/// <param name="Species">The Pokémon species at this point in the evolution chain.</param>
/// <param name="EvolutionDetails">All details regarding the specific details of the referenced Pokémon species evolution.</param>
/// <param name="EvolvesTo">A list of chain links that are possible next steps for this Pokémon evolution chain.</param>
public record ChainLink(
    bool IsBaby,
    NamedApiResource<PokemonSpecies> Species,
    IEnumerable<EvolutionDetail> EvolutionDetails,
    IEnumerable<ChainLink> EvolvesTo
);

/// <summary>
/// All details regarding the specific details of the referenced Pokémon species evolution.
/// </summary>
/// <param name="Item">The item required to cause evolution this into Pokémon species.</param>
/// <param name="Trigger">The type of event that triggers evolution into this Pokémon species.</param>
/// <param name="Gender">The id of the gender of the evolving Pokémon species must be to evolve into this Pokémon species.</param>
/// <param name="HeldItem">The item the evolving Pokémon species must be holding during the evolution trigger event to evolve into this Pokémon species.</param>
/// <param name="KnownMove">The move that must be known by the evolving Pokémon species during the evolution trigger event to evolve into this Pokémon species.</param>
/// <param name="KnownMoveType">The evolving Pokémon species must know a move with this type during the evolution trigger event to evolve into this Pokémon species.</param>
/// <param name="Location">The location the evolution must be triggered at.</param>
/// <param name="MinLevel">The minimum required level of the evolving Pokémon species to evolve into this Pokémon species.</param>
/// <param name="MinHappiness">The minimum required level of happiness the evolving Pokémon species to evolve into this Pokémon species.</param>
/// <param name="MinBeauty">The minimum required level of beauty for the evolving Pokémon species to evolve into this Pokémon species.</param>
/// <param name="MinAffection">The minimum required level of affection for the evolving Pokémon species to evolve into this Pokémon species.</param>
/// <param name="NeedsMultiplayer">	Whether multiplayer link play is needed to evolve into this Pokémon species (e.g. Union Circle).</param>
/// <param name="NeedsOverworldRain">Whether it must be raining in the overworld to cause evolution of this Pokémon species.</param>
/// <param name="PartySpecies">The Pokémon species that must be in the players party in order for the evolving Pokémon species to evolve into this Pokémon species.</param>
/// <param name="PartyType">The player must have a Pokémon of this type in their party during the evolution trigger event in order for the evolving Pokémon species to evolve into this Pokémon species.</param>
/// <param name="RelativePhysicalStats">The required relation between the Pokémon's Attack and Defence stats. 1 means Attack &gt; Defence. 0 means Attack = Defence. -1 means Attack &lt; Defence.</param>
/// <param name="TimeOfDay">The required time of day. Day or night.</param>
/// <param name="TradeSpecies">The Pokémon species for which this one must be traded.</param>
/// <param name="TurnUpsideDown">Whether the device needs to be turned upside-down as this Pokémon levels up.</param>
/// <param name="RegionId">The required region in which this evolution can occur.</param>
/// <param name="BaseFormId">The required form for which this evolution can occur.</param>
/// <param name="UsedMove">The move that must be used by the evolving Pokémon species during the evolution trigger event to evolve into this Pokémon species.</param>
/// <param name="MinMoveCount">The minimum number of times a move must be used to evolve into this Pokémon species.</param>
/// <param name="MinSteps">The minimum number of steps that must be taken to evolve into this Pokémon species.</param>
/// <param name="MinDamageTaken">The minimum amount of damage taken during the evolution trigger event to evolve into this Pokémon species.</param>
public record EvolutionDetail(
    NamedApiResource<Item>? Item,
    NamedApiResource<EvolutionTrigger> Trigger,
    int? Gender,
    NamedApiResource<Item>? HeldItem,
    NamedApiResource<Move>? KnownMove,
    NamedApiResource<Type>? KnownMoveType,
    NamedApiResource<Location>? Location,
    int? MinLevel,
    int? MinHappiness,
    int? MinBeauty,
    int? MinAffection,
    bool NeedsMultiplayer,
    bool NeedsOverworldRain,
    NamedApiResource<PokemonSpecies>? PartySpecies,
    NamedApiResource<Type>? PartyType,
    int? RelativePhysicalStats,
    string TimeOfDay,
    NamedApiResource<PokemonSpecies>? TradeSpecies,
    bool TurnUpsideDown,
    int? RegionId,
    int? BaseFormId,
    NamedApiResource<Move>? UsedMove,
    int? MinMoveCount,
    int? MinSteps,
    int? MinDamageTaken
);