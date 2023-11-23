using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Tests
{
    public class TestDatabaseFixture 
    {
        public TestDatabaseFixture()
        {
            MongoConfiguration = new MongoConfiguration(new OptionsWrapper<LoyaltyMarketDatabaseSettings>(
                new LoyaltyMarketDatabaseSettings
                {
                    ConnectionString = "mongodb://localhost:27017",
                    DatabaseName = "LoyaltyMarketDb",
                    ProductsCollectionName = "ProductsCollection",
                    CategoriesCollectionName = "CategoriesCollection"
                }));
        }

        public IMongoConfiguration MongoConfiguration { get; }
        
    }
}