using Catalog.Api.Data.Interface;
using Catalog.Api.Entities;
using Catalog.Api.Repositories.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }
        public Task CreateAsync(Product product)
        {
            return _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var res = await _context.Products.DeleteOneAsync(filter);
            return res.IsAcknowledged && res.DeletedCount > 0;
        }

        public Task<Product> GetProductAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id); 
            return _context.Products.Find(filter).FirstOrDefaultAsync();
        }

        public Task<List<Product>> GetProductByCategoryAsync(string categoryName)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            return _context.Products.Find(filter).ToListAsync();
        }

        public Task<List<Product>> GetProductByNameAsync(string name)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return _context.Products.Find(filter).ToListAsync();
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return _context.Products.Find(x => true).ToListAsync();
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var result = await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
