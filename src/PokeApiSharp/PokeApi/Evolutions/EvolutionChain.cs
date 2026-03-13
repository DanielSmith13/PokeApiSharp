using System.Collections.Generic;
using PokeApiSharp.Attributes;
using PokeApiSharp.PokeApi.Items;
using PokeApiSharp.PokeApi.Locations;
using PokeApiSharp.PokeApi.Moves;
using PokeApiSharp.PokeApi.Pokemon;
using Type = PokeApiSharp.PokeApi.Pokemon.Type;

namespace PokeApiSharp.PokeApi.Evolutions;

[PokeApiResource("evolution-chain")]
public record EvolutionChain(
    int Id,
    NamedApiResource<Item> BabyTriggerItem,
    ChainLink Chain);
    
public record ChainLink(
    bool IsBaby,
    NamedApiResource<PokemonSpecies> Species,
    NamedApiResource<EvolutionDetail> EvolutionDetails,
    IEnumerable<ChainLink> EvolvesTo
);

public record EvolutionDetail(
    NamedApiResource<Item> Item,
    NamedApiResource<EvolutionTrigger> Trigger,
    int Gender,
    NamedApiResource<Item> HeldItem,
    NamedApiResource<Move> KnownMove,
    NamedApiResource<Type> KnownMoveType,
    NamedApiResource<Location> Location,
    int MinLevel,
    int MinHappiness,
    int MinBeauty,
    int MinAffection,
    bool NeedsMultiplayer,
    bool NeedsOverworldRain,
    NamedApiResource<PokemonSpecies> PartySpecies,
    NamedApiResource<Type> PartyType,
    int RelativePhysicalStats,
    string TimeOfDay,
    NamedApiResource<PokemonSpecies> TradeSpecies,
    bool TurnUpsideDown,
    NamedApiResource<Region> Region,
    NamedApiResource<PokemonSpecies> BaseForm,
    NamedApiResource<Move> UsedMove,
    int MinMoveCount,
    int MinSteps,
    int MinDamageTaken
);