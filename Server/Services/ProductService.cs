using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using LoyaltyMarket.Models;
using LoyaltyMarket.Server.Data;


namespace LoyaltyMarket.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly DataBaseContext _context;

        public ProductService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _context.Products.Find(_ => true).ToListAsync();
            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> CreateProduct(string name, string description, decimal price, string category, string color)
        {
            var product = new Product(name, description, price, category, color);
            await _context.Products.InsertOneAsync(product);            
            return product;
        }

        public async Task<bool> UpdateProduct(int id, Product updatedProduct)
        {
            var result = await _context.Products.ReplaceOneAsync(p => p.Id == id, updatedProduct);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var result = await _context.Products.DeleteOneAsync(p => p.Id == id);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}