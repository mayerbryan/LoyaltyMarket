using Infrastructure.Entities;
using MongoDB.Driver;

namespace Infrastructure.Configuration
{
    public interface IMongoConfiguration
    {
        public IMongoCollection<Category> GetCategoryCollection();

        public IMongoCollection<Product> GetProductCollection();

    }
}