using System.Text.Json;
using System.Text.Json.Serialization;
using PokeApiSharp.Utilities;

namespace Unit.Utilities;

public class JsonDiffHelpersTests
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    private record SimpleRecord(int Id, string Name);
    private record NestedOuter(int Id, NestedInner Inner);
    private record NestedInner(string Value);
    private record WithList(IEnumerable<ListItem> Items);
    private record ListItem(int Id);
    private record WithAlias([property: JsonPropertyName("api_name")] string Name);
    private record WithIgnoredProperty(int Id, [property: JsonIgnore] string Secret);

    private class WithExtensionData
    {
        public int Id { get; init; }
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? Extra { get; init; }
    }

    [Fact]
    public void FindUnmappedJsonProperties_ReturnsEmpty_WhenAllPropertiesAreMapped()
    {
        const string json = """{"id":1,"name":"foo"}""";

        var result = JsonDiffHelpers.FindUnmappedJsonProperties<SimpleRecord>(json, Options);

        Assert.Empty(result);
    }

    [Fact]
    public void FindUnmappedJsonProperties_ReturnsRootProperty_WhenUnmappedAtRoot()
    {
        const string json = """{"id":1,"name":"foo","extra_prop":"bar"}""";

        var result = JsonDiffHelpers.FindUnmappedJsonProperties<SimpleRecord>(json, Options);

        Assert.Single(result);
        Assert.Equal("extra_prop", result[0]);
    }

    [Fact]
    public void FindUnmappedJsonProperties_ReturnsMultipleRootProperties_WhenSeveralUnmapped()
    {
        const string json = """{"id":1,"name":"foo","alpha":"x","beta":"y"}""";

        var result = JsonDiffHelpers.FindUnmappedJsonProperties<SimpleRecord>(json, Options);

        Assert.Equal(2, result.Count);
        Assert.Contains("alpha", result);
        Assert.Contains("beta", result);
    }

    [Fact]
    public void FindUnmappedJsonProperties_ReturnsDottedPath_WhenUnmappedInNestedObject()
    {
        const string json = """{"id":1,"inner":{"value":"v","extra":"x"}}""";

        var result = JsonDiffHelpers.FindUnmappedJsonProperties<NestedOuter>(json, Options);

        Assert.Single(result);
        Assert.Equal("inner.extra", result[0]);
    }

    [Fact]
    public void FindUnmappedJsonProperties_UsesBracketFormat_WhenAllArrayItemsMissingProperty()
    {
        const string json = """{"items":[{"id":1,"extra":"x"},{"id":2,"extra":"y"}]}""";

        var result = JsonDiffHelpers.FindUnmappedJsonProperties<WithList>(json, Options);

        Assert.Single(result);
        Assert.Equal("items[].extra", result[0]);
    }

    [Fact]
    public void FindUnmappedJsonProperties_ReturnsIndexedPath_WhenSomeArrayItemsMissingProperty()
    {
        const string json = """{"items":[{"id":1,"extra":"x"},{"id":2}]}""";

        var result = JsonDiffHelpers.FindUnmappedJsonProperties<WithList>(json, Options);

        Assert.Single(result);
        Assert.Equal("items[0].extra", result[0]);
    }

    [Fact]
    public void FindUnmappedJsonProperties_IncludesMoreSuffix_WhenMoreThanThreeItemsMissingProperty()
    {
        // Items 0–3 have "extra" (4 items), item 4 does not → not all 5 → indexed format with (+1 more)
        const string json = """{"items":[{"id":1,"extra":"x"},{"id":2,"extra":"x"},{"id":3,"extra":"x"},{"id":4,"extra":"x"},{"id":5}]}""";

        var result = JsonDiffHelpers.FindUnmappedJsonProperties<WithList>(json, Options);

        Assert.Single(result);
        Assert.Contains("items[0].extra", result[0]);
        Assert.Contains("+1 more", result[0]);
    }

    [Fact]
    public void FindUnmappedJsonProperties_ReturnsEmpty_WhenJsonPropertyNameMatchesAlias()
    {
        const string json = """{"api_name":"foo"}""";

        var result = JsonDiffHelpers.FindUnmappedJsonProperties<WithAlias>(json, Options);

        Assert.Empty(result);
    }

    [Fact]
    public void FindUnmappedJsonProperties_FlagsProperty_WhenSnakeCaseNameUsedInsteadOfAlias()
    {
        // The C# "Name" property is aliased to "api_name". "name" (snake_case) is not in expected → flagged.
        const string json = """{"name":"foo"}""";

        var result = JsonDiffHelpers.FindUnmappedJsonProperties<WithAlias>(json, Options);

        Assert.Single(result);
        Assert.Equal("name", result[0]);
    }

    [Fact]
    public void FindUnmappedJsonProperties_FlagsIgnoredPropertyName_WhenPresentInJson()
    {
        // "secret" is [JsonIgnore] so it is excluded from expected → flagged if present in JSON.
        const string json = """{"id":1,"secret":"hidden"}""";

        var result = JsonDiffHelpers.FindUnmappedJsonProperties<WithIgnoredProperty>(json, Options);

        Assert.Single(result);
        Assert.Equal("secret", result[0]);
    }

    [Fact]
    public void FindUnmappedJsonProperties_ReturnsEmpty_WhenTypeHasJsonExtensionData()
    {
        // [JsonExtensionData] absorbs all unknown fields → nothing flagged.
        const string json = """{"id":1,"unknown_a":"x","unknown_b":"y"}""";

        var result = JsonDiffHelpers.FindUnmappedJsonProperties<WithExtensionData>(json, Options);

        Assert.Empty(result);
    }

    [Fact]
    public void FindUnmappedJsonProperties_ReturnsEmpty_ForEmptyJsonObject()
    {
        const string json = """{}""";

        var result = JsonDiffHelpers.FindUnmappedJsonProperties<SimpleRecord>(json, Options);

        Assert.Empty(result);
    }
}
