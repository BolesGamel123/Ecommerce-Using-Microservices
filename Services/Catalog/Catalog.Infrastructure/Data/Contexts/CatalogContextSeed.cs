using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catolog.Infrastructure.Data.Contexts
{
    public static class CatalogContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<Product> productCollection)
        {

            var hasProducts = await productCollection.Find(_ => true).AnyAsync();
            if (hasProducts)
                return;
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Data/SeedData/products.json");
            // var filepath = Path.Combine("Data", "SeedData", "products.json");
            if (!File.Exists(filepath))
                throw new FileNotFoundException("Seed data file not found", filepath);

            var productsData = await File.ReadAllTextAsync(filepath);
            var products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(productsData);

            if (products?.Any() is true)
            {
                await productCollection.InsertManyAsync(products);
            }
        }
    }
}
