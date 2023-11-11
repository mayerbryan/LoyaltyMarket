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
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categorysCollection;

        public CategoryService(
            IOptions<LoyaltyMarketDatabaseSettings> loyaltyMarketDatabaseSettings){
                var mongoClient = new MongoClient(loyaltyMarketDatabaseSettings.Value.ConnectionString);
                var mongoDatabase = mongoClient.GetDatabase(loyaltyMarketDatabaseSettings.Value.DatabaseName);
                _categorysCollection = mongoDatabase.GetCollection<Category>(loyaltyMarketDatabaseSettings.Value.CategoriesCollectionName);
        }

        public async Task<List<Category>> GetAllAsync() =>
            await _categorysCollection.Find(_ => true).ToListAsync();

        public async Task<Category?> GetByIdAsync(string id) =>
            await _categorysCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Category newBook) =>
            await _categorysCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Category updatedBook) =>
            await _categorysCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _categorysCollection.DeleteOneAsync(x => x.Id == id);
    }
}