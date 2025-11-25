using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catolog.Infrastructure.Data.Contexts;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypeRepository
    {
        public ICatalogContext _context { get; set; }
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var result = await _context.Products.DeleteOneAsync(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var result = await _context.Products.ReplaceOneAsync(
             filter: p => p.Id == product.Id,
             replacement: product);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
        public async Task<Product> GetProductById(string id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsByBrand(string name)
        {
            return await _context.Products.Find(p => p.Brand.Name == name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsByName(string name)
        {
            return await _context.Products.Find(p => p.Name == name).ToListAsync();
        }
        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await _context.Brands.Find(b => true).ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {
            return await _context.Types.Find(t => true).ToListAsync();
        }


    }
}
