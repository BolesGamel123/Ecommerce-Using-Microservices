using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catolog.Infrastructure.Data.Contexts
{
    public static class BrandContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<ProductBrand> brandCollection)
        {

            var hasBrands = await brandCollection.Find(_ => true).AnyAsync();
            if (hasBrands)
                return;

            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "SeedData", "brands.json");
            //var filepath = Path.Combine("Data", "SeedData", "brands.json");
            if (!File.Exists(filepath))
                throw new FileNotFoundException("Seed data file not found", filepath);

            var brandsData = await File.ReadAllTextAsync(filepath);
            var brands = System.Text.Json.JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

            if (brands?.Any() is true)
            {
                await brandCollection.InsertManyAsync(brands);
            }
        }
    }
}
