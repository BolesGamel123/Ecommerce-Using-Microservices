using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catolog.Infrastructure.Data.Contexts
{
    public static class TypeContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<ProductType> typeCollection)
        {

            var hasTypes = await typeCollection.Find(_ => true).AnyAsync();
            if (hasTypes)
                return;

            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "SeedData", "types.json");
            //var filepath = Path.Combine("Data", "SeedData", "types.json");
            if (!File.Exists(filepath))
                throw new FileNotFoundException("Seed data file not found", filepath);

            var typesData = await File.ReadAllTextAsync(filepath);
            var types = System.Text.Json.JsonSerializer.Deserialize<List<ProductType>>(typesData);

            if (types?.Any() is true)
            {
                await typeCollection.InsertManyAsync(types);
            }
        }
    }
}
