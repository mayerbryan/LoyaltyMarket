using Infrastructure.Configuration;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categorysCollection;

        public CategoryRepository(IMongoConfiguration mongoConfiguration)
        {
            _categorysCollection = mongoConfiguration.GetCategoryCollection();
        }
        
        public async Task<List<Category>> GetAllAsync(CancellationToken token = default) {
            IAsyncCursor<Category> cursor = await _categorysCollection.FindAsync(_ => true, null, token);
            List<Category> categories = await cursor.ToListAsync(token);
            return categories;
        }
            

        public async Task CreateAsync(Category newCategory) =>
            await _categorysCollection.InsertOneAsync(newCategory);

        public async Task UpdateAsync(string id, Category categoryToUpdate)
        {
            var objectId = new ObjectId(id).ToString();
            await _categorysCollection.ReplaceOneAsync(x => Equals(x.Id, objectId), categoryToUpdate);
        }

        public async Task RemoveAsync(string id) =>
            await _categorysCollection.DeleteOneAsync(x => Equals(x.Id, id));

        public async Task<bool> ExistsAsync(string id) {
            var exists = false;

            var result = await _categorysCollection.CountDocumentsAsync(entity => Equals(entity.Id, id));
            if (Equals(result, 1))
            {
                exists = true;
            }

            return exists;
        }
        
    }
}