using Microsoft.Extensions.Caching.Memory;
using PokeApiSharp.Caching;

namespace Unit.Caching;

public class PokeApiCacheTests
{
        [Fact]
        public void Cache_ShouldStoreAndRetrieveResource()
        {
            // Arrange
            var cache = new PokeApiCache(new MemoryCache(new MemoryCacheOptions()));
            const string url = "https://pokeapi.co/api/v2/pokemon/1/";
            var expectedResource = new { Name = "bulbasaur", Id = 1 };
    
            // Act
            cache.SetCachedResource(url, expectedResource);
            var result = cache.GetCachedResource<dynamic>(url);
    
            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedResource.Name, result.Match(r => r.Name, _ => null));
            Assert.Equal(expectedResource.Id, result.Match(r => r.Id, _ => 0));
        }
    
        [Fact]
        public void Cache_ShouldReturnFailureForMissingResource()
        {
            // Arrange
            var cache = new PokeApiCache(new MemoryCache(new MemoryCacheOptions()));
            const string url = "https://pokeapi.co/api/v2/pokemon/9999/";

            // Act
            var result = cache.GetCachedResource<dynamic>(url);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Resource not found in cache.", result.Match(_ => url, f => f));
        }

        [Fact]
        public void Cache_ReturnsFailureWithNullMessage_WhenNullIsStored()
        {
            var cache = new PokeApiCache(new MemoryCache(new MemoryCacheOptions()));
            const string url = "https://pokeapi.co/api/v2/pokemon/1/";

            cache.SetCachedResource<string?>(url, null);
            var result = cache.GetCachedResource<string?>(url);

            Assert.True(result.IsFailure);
            Assert.Equal("Cached resource is null.", result.Match(_ => "", f => f));
        }

        [Fact]
        public void ClearCache_RemovesAllStoredEntries()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var cache = new PokeApiCache(memoryCache);
            cache.SetCachedResource("url1", "value1");
            cache.SetCachedResource("url2", "value2");

            cache.ClearCache();

            Assert.True(cache.GetCachedResource<string>("url1").IsFailure);
            Assert.True(cache.GetCachedResource<string>("url2").IsFailure);
        }
}