using Catalog.Core.Entities;
using Catolog.Infrastructure.Data.Contexts;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Contexts
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public IMongoCollection<ProductBrand> Brands { get; }

        public IMongoCollection<ProductType> Types { get; }

        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);


            Brands = database.GetCollection<ProductBrand>(configuration["DatabaseSettings:CollectionNameBrands"]);
            Types = database.GetCollection<ProductType>(configuration["DatabaseSettings:CollectionNameTypes"]);
            Products = database.GetCollection<Product>(configuration["DatabaseSettings:CollectionNameProducts"]);

            //_ = BrandContextSeed.SeedDataAsync(Brands);
            //_ = TypeContextSeed.SeedDataAsync(Types);
            //_ = CatalogContextSeed.SeedDataAsync(Products);



        }
    }
}
