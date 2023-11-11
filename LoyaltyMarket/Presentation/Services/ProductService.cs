using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime.Internal;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Presentation.Models;

namespace Presentation.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _ProductsCollection;

        public ProductService(
            IOptions<LoyaltyMarketDatabaseSettings> loyaltyMarketDatabaseSettings){
                var mongoClient = new MongoClient(loyaltyMarketDatabaseSettings.Value.ConnectionString);
                var mongoDatabase = mongoClient.GetDatabase(loyaltyMarketDatabaseSettings.Value.DatabaseName);
                _ProductsCollection = mongoDatabase.GetCollection<Product>(loyaltyMarketDatabaseSettings.Value.ProductsCollectionName);
        }

        public async Task<List<Product>> GetAllAsync() =>
            await _ProductsCollection.Find(_ => true).ToListAsync();

        public async Task<Product?> GetByIdAsync(string id) =>
            await _ProductsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product newBook) =>
            await _ProductsCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Product updatedBook) =>
            await _ProductsCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _ProductsCollection.DeleteOneAsync(x => x.Id == id);
    }
}