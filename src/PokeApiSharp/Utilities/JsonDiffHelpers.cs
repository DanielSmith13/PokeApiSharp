namespace PokeApiSharp.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Detect unmapped JSON properties while respecting JsonPropertyName, JsonIgnore,
/// JsonExtensionData, JsonSerializerOptions.PropertyNamingPolicy and PropertyNameCaseInsensitive.
/// - Recurses into nested objects
/// - Aggregates repeated missing properties across array items into concise messages
/// </summary>
public static class JsonDiffHelpers
{
    /// <summary>
    /// Identifies and returns a list of JSON property names from the input JSON string
    /// that do not map to properties of the specified type. The method takes into account:
    /// - Properties annotated with JsonPropertyName and JsonIgnore attributes.
    /// - JsonExtensionData for flexible JSON patterns.
    /// - Serialization rules such as PropertyNamingPolicy and PropertyNameCaseInsensitive.
    /// It also handles nested objects and array structures, consolidating repeated
    /// missing properties into concise messages.
    /// </summary>
    /// <typeparam name="T">The type that the JSON is expected to map to.</typeparam>
    /// <param name="json">The JSON string to analyse for unmapped properties.</param>
    /// <param name="options">JsonSerializerOptions to customise the property matching behaviour.</param>
    /// <returns>
    /// A list of strings representing the paths of unmapped properties within the JSON.
    /// Paths use dot notation for nested objects and indices for arrays.
    /// </returns>
    public static List<string> FindUnmappedJsonProperties<T>(string json, JsonSerializerOptions options)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        return GetMissingForObject(typeof(T), root, options, "");
    }

    private static List<string> GetMissingForObject(Type type, JsonElement element, JsonSerializerOptions options, string currentPath)
    {
        var outMissing = new List<string>();

        if (element.ValueKind != JsonValueKind.Object 
            || type == typeof(JsonElement) 
            || type == typeof(object))
            return outMissing;

        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(p => p.CanRead && p.GetCustomAttribute<JsonIgnoreAttribute>() == null)
                        .ToArray();

        var hasExtensionData = props.Any(p => p.GetCustomAttribute<JsonExtensionDataAttribute>() != null);

        var expected = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);

        foreach (var p in props)
        {
            if (p.GetCustomAttribute<JsonExtensionDataAttribute>() != null) continue;
            
            var jsonNameAttr = p.GetCustomAttribute<JsonPropertyNameAttribute>();
            expected.TryAdd(jsonNameAttr?.Name ?? options.PropertyNamingPolicy?.ConvertName(p.Name) ?? p.Name, p);
        }

        foreach (var jProp in element.EnumerateObject())
        {
            if (!expected.TryGetValue(jProp.Name, out var pi))
            {
                if (!hasExtensionData)
                    outMissing.Add(JoinPath(currentPath, jProp.Name));
                continue;
            }

            var propType = Nullable.GetUnderlyingType(pi.PropertyType) ?? pi.PropertyType;
            var value = jProp.Value;
            
            if (propType == typeof(JsonElement) || propType == typeof(object))
                continue;

            switch (value.ValueKind)
            {
                case JsonValueKind.Object:
                {
                    var nested = GetMissingForObject(propType, value, options, JoinPath(currentPath, jProp.Name));
                    outMissing.AddRange(nested);
                    break;
                }
                case JsonValueKind.Array:
                {
                    var itemType = GetEnumerableItemType(propType);
                    if (itemType == null || itemType == typeof(object) || itemType == typeof(JsonElement)) continue;
                    var arraySummaries = AnalyseArrayForMissing(itemType, value, options, JoinPath(currentPath, jProp.Name));
                    outMissing.AddRange(arraySummaries);
                    break;
                }
                case JsonValueKind.Undefined:
                case JsonValueKind.String:
                case JsonValueKind.Number:
                case JsonValueKind.True:
                case JsonValueKind.False:
                case JsonValueKind.Null:
                default:
                    break;
            }
        }

        return outMissing;
    }

    private static List<string> AnalyseArrayForMissing(
        Type itemType,
        JsonElement arrayElement,
        JsonSerializerOptions options,
        string prefixPath)
    {
        var results = new List<string>();
        if (arrayElement.ValueKind != JsonValueKind.Array)
            return results;

        var objIndices = new List<int>();
        var occurrences = new Dictionary<string, List<int>>(StringComparer.OrdinalIgnoreCase);

        var idx = 0;
        foreach (var item in arrayElement.EnumerateArray())
        {
            if (item.ValueKind == JsonValueKind.Object)
            {
                objIndices.Add(idx);
                var missingForItem = GetMissingForObject(itemType, item, options, "");
                foreach (var m in missingForItem)
                {
                    if (!occurrences.TryGetValue(m, out var list)) { list = []; occurrences[m] = list; }
                    list.Add(idx);
                }
            }
            idx++;
        }

        if (objIndices.Count == 0) return results;
        
        foreach (var (key, value) in occurrences)
        {
            if (value.Count == objIndices.Count)
            {
                results.Add($"{prefixPath}[].{key}");
            }
            else
            {
                var exampleCount = Math.Min(3, value.Count);
                var examples = string.Join(", ", value.Take(exampleCount).Select(i => $"{prefixPath}[{i}].{key}"));
                var remaining = value.Count - exampleCount;
                results.Add(remaining > 0 ? $"{examples} (+{remaining} more)" : examples);
            }
        }

        return results;
    }

    private static string JoinPath(string prefix, string name) => string.IsNullOrEmpty(prefix) ? name : $"{prefix}.{name}";
    
    private static Type? GetEnumerableItemType(Type type)
    {
        if (type.IsArray) return type.GetElementType();

        if (!type.IsGenericType)
            return type.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                .Select(i => i.GetGenericArguments()[0])
                .FirstOrDefault();
        var gen = type.GetGenericArguments().FirstOrDefault();
        if (gen != null && typeof(System.Collections.IEnumerable).IsAssignableFrom(type))
            return gen;

        return type.GetInterfaces()
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            .Select(i => i.GetGenericArguments()[0])
            .FirstOrDefault();
    }
}
