using System.Text.Json.Serialization;

namespace PokeApiSharp;

/// <summary>
/// Represents the sprites used to depict a Pokémon in the game.
/// </summary>
/// <param name="BackDefault">The default depiction of this Pokémon from the back in battle.</param>
/// <param name="BackFemale">The female depiction of this Pokémon from the back in battle.</param>
/// <param name="BackShiny">The shiny depiction of this Pokémon from the back in battle.</param>
/// <param name="BackShinyFemale">The shiny female depiction of this Pokémon from the back in battle.</param>
/// <param name="FrontDefault">The default depiction of this Pokémon from the front in battle.</param>
/// <param name="FrontFemale">The female depiction of this Pokémon from the front in battle.</param>
/// <param name="FrontShiny">The shiny depiction of this Pokémon from the front in battle.</param>
/// <param name="FrontShinyFemale">The shiny female depiction of this Pokémon from the front in battle.</param>
/// <param name="Other">Other depictions of this Pokémon.</param>
/// <param name="Versions">Depictions of this Pokémon from different generations of games.</param>
public record PokemonSprites(
    string? BackDefault,
    string? BackFemale,
    string? BackShiny,
    string? BackShinyFemale,
    string? FrontDefault,
    string? FrontFemale,
    string? FrontShiny,
    string? FrontShinyFemale,
    PokemonSpritesOther? Other,
    PokemonSpritesVersions? Versions
);

/// <summary>
/// Other depictions of a Pokémon.
/// </summary>
/// <param name="DreamWorld">Depiction of this Pokémon from the Dream World.</param>
/// <param name="Home">Depiction of this Pokémon from Pokémon HOME.</param>
/// <param name="OfficialArtwork">Official artwork of this Pokémon.</param>
/// <param name="Showdown">Depiction of this Pokémon from Pokémon Showdown.</param>
public record PokemonSpritesOther(
    [property: JsonPropertyName("dream_world")] PokemonSpritesDreamWorld? DreamWorld,
    PokemonSpritesHome? Home,
    [property: JsonPropertyName("official-artwork")] PokemonSpritesOfficialArtwork? OfficialArtwork,
    PokemonSpritesShowdown? Showdown
);

/// <summary>
/// Depiction of a Pokémon from the Dream World.
/// </summary>
/// <param name="FrontDefault">The default depiction of this Pokémon from the front.</param>
/// <param name="FrontFemale">The female depiction of this Pokémon from the front.</param>
public record PokemonSpritesDreamWorld(
    string? FrontDefault,
    string? FrontFemale
);

/// <summary>
/// Depiction of a Pokémon from Pokémon HOME.
/// </summary>
/// <param name="FrontDefault">The default depiction of this Pokémon from the front.</param>
/// <param name="FrontFemale">The female depiction of this Pokémon from the front.</param>
/// <param name="FrontShiny">The shiny depiction of this Pokémon from the front.</param>
/// <param name="FrontShinyFemale">The shiny female depiction of this Pokémon from the front.</param>
public record PokemonSpritesHome(
    string? FrontDefault,
    string? FrontFemale,
    string? FrontShiny,
    string? FrontShinyFemale
);

/// <summary>
/// Official artwork of a Pokémon.
/// </summary>
/// <param name="FrontDefault">The default official artwork of this Pokémon.</param>
/// <param name="FrontShiny">The shiny official artwork of this Pokémon.</param>
public record PokemonSpritesOfficialArtwork(
    string? FrontDefault,
    string? FrontShiny
);

/// <summary>
/// Depiction of a Pokémon from Pokémon Showdown.
/// </summary>
/// <param name="BackDefault">The default depiction of this Pokémon from the back.</param>
/// <param name="BackFemale">The female depiction of this Pokémon from the back.</param>
/// <param name="BackShiny">The shiny depiction of this Pokémon from the back.</param>
/// <param name="BackShinyFemale">The shiny female depiction of this Pokémon from the back.</param>
/// <param name="FrontDefault">The default depiction of this Pokémon from the front.</param>
/// <param name="FrontFemale">The female depiction of this Pokémon from the front.</param>
/// <param name="FrontShiny">The shiny depiction of this Pokémon from the front.</param>
/// <param name="FrontShinyFemale">The shiny female depiction of this Pokémon from the front.</param>
public record PokemonSpritesShowdown(
    string? BackDefault,
    string? BackFemale,
    string? BackShiny,
    string? BackShinyFemale,
    string? FrontDefault,
    string? FrontFemale,
    string? FrontShiny,
    string? FrontShinyFemale
);

/// <summary>
/// Depictions of a Pokémon from different generations of games.
/// </summary>
public record PokemonSpritesVersions(
    [property: JsonPropertyName("generation-i")] PokemonSpritesGenerationI? GenerationI,
    [property: JsonPropertyName("generation-ii")] PokemonSpritesGenerationII? GenerationII,
    [property: JsonPropertyName("generation-iii")] PokemonSpritesGenerationIII? GenerationIII,
    [property: JsonPropertyName("generation-iv")] PokemonSpritesGenerationIV? GenerationIV,
    [property: JsonPropertyName("generation-v")] PokemonSpritesGenerationV? GenerationV,
    [property: JsonPropertyName("generation-vi")] PokemonSpritesGenerationVI? GenerationVI,
    [property: JsonPropertyName("generation-vii")] PokemonSpritesGenerationVII? GenerationVII,
    [property: JsonPropertyName("generation-viii")] PokemonSpritesGenerationVIII? GenerationVIII,
    [property: JsonPropertyName("generation-ix")] PokemonSpritesGenerationIX? GenerationIX
);

/// <summary>
/// Depictions of a Pokémon from Generation I.
/// </summary>
public record PokemonSpritesGenerationI(
    [property: JsonPropertyName("red-blue")] PokemonSpritesRedBlue? RedBlue,
    PokemonSpritesYellow? Yellow
);

/// <summary>
/// Depictions of a Pokémon from Generation II.
/// </summary>
public record PokemonSpritesGenerationII(
    PokemonSpritesCrystal? Crystal,
    PokemonSpritesGold? Gold,
    PokemonSpritesSilver? Silver
);

/// <summary>
/// Depictions of a Pokémon from Generation III.
/// </summary>
public record PokemonSpritesGenerationIII(
    PokemonSpritesEmerald? Emerald,
    [property: JsonPropertyName("firered-leafgreen")] PokemonSpritesFireRedLeafGreen? FireRedLeafGreen,
    [property: JsonPropertyName("ruby-sapphire")] PokemonSpritesRubySapphire? RubySapphire
);

/// <summary>
/// Depictions of a Pokémon from Generation IV.
/// </summary>
public record PokemonSpritesGenerationIV(
    [property: JsonPropertyName("diamond-pearl")] PokemonSpritesDiamondPearl? DiamondPearl,
    [property: JsonPropertyName("heartgold-soulsilver")] PokemonSpritesHeartGoldSoulSilver? HeartGoldSoulSilver,
    PokemonSpritesPlatinum? Platinum
);

/// <summary>
/// Depictions of a Pokémon from Generation V.
/// </summary>
public record PokemonSpritesGenerationV(
    [property: JsonPropertyName("black-white")] PokemonSpritesBlackWhite? BlackWhite
);

/// <summary>
/// Depictions of a Pokémon from Generation VI.
/// </summary>
public record PokemonSpritesGenerationVI(
    [property: JsonPropertyName("omegaruby-alphasapphire")] PokemonSpritesOmegarubyAlphasapphire? OmegarubyAlphasapphire,
    [property: JsonPropertyName("x-y")] PokemonSpritesXY? XY
);

/// <summary>
/// Depictions of a Pokémon from Generation VII.
/// </summary>
public record PokemonSpritesGenerationVII(
    PokemonSpritesIcons? Icons,
    [property: JsonPropertyName("ultra-sun-ultra-moon")] PokemonSpritesUltraSunUltraMoon? UltraSunUltraMoon
);

/// <summary>
/// Depictions of a Pokémon from Generation VIII.
/// </summary>
public record PokemonSpritesGenerationVIII(
    [property: JsonPropertyName("brilliant-diamond-shining-pearl")] PokemonSpritesBrilliantDiamondShiningPearl? BrilliantDiamondShiningPearl,
    PokemonSpritesIcons? Icons
);

/// <summary>
/// Depictions of a Pokémon from Generation IX.
/// </summary>
public record PokemonSpritesGenerationIX(
    [property: JsonPropertyName("scarlet-violet")] PokemonSpritesScarletViolet? ScarletViolet
);

/// <summary>
/// Depictions of a Pokémon from Red and Blue.
/// </summary>
public record PokemonSpritesRedBlue(
    string? BackDefault,
    string? BackGray,
    string? BackTransparent,
    string? FrontDefault,
    string? FrontGray,
    string? FrontTransparent
);

/// <summary>
/// Depictions of a Pokémon from Yellow.
/// </summary>
public record PokemonSpritesYellow(
    string? BackDefault,
    string? BackGray,
    string? BackTransparent,
    string? FrontDefault,
    string? FrontGray,
    string? FrontTransparent
);

/// <summary>
/// Depictions of a Pokémon from Crystal.
/// </summary>
public record PokemonSpritesCrystal(
    string? BackDefault,
    string? BackShiny,
    string? BackShinyTransparent,
    string? BackTransparent,
    string? FrontDefault,
    string? FrontShiny,
    string? FrontShinyTransparent,
    string? FrontTransparent
);

/// <summary>
/// Depictions of a Pokémon from Gold.
/// </summary>
public record PokemonSpritesGold(
    string? BackDefault,
    string? BackShiny,
    string? FrontDefault,
    string? FrontShiny,
    string? FrontTransparent
);

/// <summary>
/// Depictions of a Pokémon from Silver.
/// </summary>
public record PokemonSpritesSilver(
    string? BackDefault,
    string? BackShiny,
    string? FrontDefault,
    string? FrontShiny,
    string? FrontTransparent
);

/// <summary>
/// Depictions of a Pokémon from Emerald.
/// </summary>
public record PokemonSpritesEmerald(
    string? FrontDefault,
    string? FrontShiny
);

/// <summary>
/// Depictions of a Pokémon from FireRed and LeafGreen.
/// </summary>
public record PokemonSpritesFireRedLeafGreen(
    string? BackDefault,
    string? BackShiny,
    string? FrontDefault,
    string? FrontShiny
);

/// <summary>
/// Depictions of a Pokémon from Ruby and Sapphire.
/// </summary>
public record PokemonSpritesRubySapphire(
    string? BackDefault,
    string? BackShiny,
    string? FrontDefault,
    string? FrontShiny
);

/// <summary>
/// Depictions of a Pokémon from Diamond and Pearl.
/// </summary>
public record PokemonSpritesDiamondPearl(
    string? BackDefault,
    string? BackFemale,
    string? BackShiny,
    string? BackShinyFemale,
    string? FrontDefault,
    string? FrontFemale,
    string? FrontShiny,
    string? FrontShinyFemale
);

/// <summary>
/// Depictions of a Pokémon from HeartGold and SoulSilver.
/// </summary>
public record PokemonSpritesHeartGoldSoulSilver(
    string? BackDefault,
    string? BackFemale,
    string? BackShiny,
    string? BackShinyFemale,
    string? FrontDefault,
    string? FrontFemale,
    string? FrontShiny,
    string? FrontShinyFemale
);

/// <summary>
/// Depictions of a Pokémon from Platinum.
/// </summary>
public record PokemonSpritesPlatinum(
    string? BackDefault,
    string? BackFemale,
    string? BackShiny,
    string? BackShinyFemale,
    string? FrontDefault,
    string? FrontFemale,
    string? FrontShiny,
    string? FrontShinyFemale
);

/// <summary>
/// Depictions of a Pokémon from Black and White.
/// </summary>
public record PokemonSpritesBlackWhite(
    PokemonSpritesAnimated? Animated,
    string? BackDefault,
    string? BackFemale,
    string? BackShiny,
    string? BackShinyFemale,
    string? FrontDefault,
    string? FrontFemale,
    string? FrontShiny,
    string? FrontShinyFemale
);

/// <summary>
/// Animated depictions of a Pokémon.
/// </summary>
public record PokemonSpritesAnimated(
    string? BackDefault,
    string? BackFemale,
    string? BackShiny,
    string? BackShinyFemale,
    string? FrontDefault,
    string? FrontFemale,
    string? FrontShiny,
    string? FrontShinyFemale
);

/// <summary>
/// Depictions of a Pokémon from Omega Ruby and Alpha Sapphire.
/// </summary>
public record PokemonSpritesOmegarubyAlphasapphire(
    string? FrontDefault,
    string? FrontFemale,
    string? FrontShiny,
    string? FrontShinyFemale
);

/// <summary>
/// Depictions of a Pokémon from X and Y.
/// </summary>
public record PokemonSpritesXY(
    string? FrontDefault,
    string? FrontFemale,
    string? FrontShiny,
    string? FrontShinyFemale
);

/// <summary>
/// Icon depictions of a Pokémon.
/// </summary>
public record PokemonSpritesIcons(
    string? FrontDefault,
    string? FrontFemale
);

/// <summary>
/// Depictions of a Pokémon from Ultra Sun and Ultra Moon.
/// </summary>
public record PokemonSpritesUltraSunUltraMoon(
    string? FrontDefault,
    string? FrontFemale,
    string? FrontShiny,
    string? FrontShinyFemale
);

/// <summary>
/// Depictions of a Pokémon from Brilliant Diamond and Shining Pearl.
/// </summary>
public record PokemonSpritesBrilliantDiamondShiningPearl(
    string? FrontDefault,
    string? FrontFemale
);

/// <summary>
/// Depictions of a Pokémon from Scarlet and Violet.
/// </summary>
public record PokemonSpritesScarletViolet(
    string? FrontDefault,
    string? FrontFemale
);