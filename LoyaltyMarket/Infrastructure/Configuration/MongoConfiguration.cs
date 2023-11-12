using Infrastructure.Data.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Configuration
{
    public class MongoConfiguration : IMongoConfiguration
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IOptions<LoyaltyMarketDatabaseSettings> _loyaltyMarketDatabaseSettings;
        public MongoConfiguration(IOptions<LoyaltyMarketDatabaseSettings> loyaltyMarketDatabaseSettings)
        {
            _loyaltyMarketDatabaseSettings = loyaltyMarketDatabaseSettings;
            _mongoClient = new MongoClient(loyaltyMarketDatabaseSettings.Value.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(loyaltyMarketDatabaseSettings.Value.DatabaseName);
        }

        public IMongoCollection<Category> GetCategoryCollection()
        {
            return _mongoDatabase.GetCollection<Category>(_loyaltyMarketDatabaseSettings.Value.CategoriesCollectionName);
        }

        // public IMongoCollection<Product> GetProductCollection()
        // {
        //     return _mongoDatabase.GetCollection<Product>(_loyaltyMarketDatabaseSettings.Value.ProductsCollectionName);
        // }
    }
}