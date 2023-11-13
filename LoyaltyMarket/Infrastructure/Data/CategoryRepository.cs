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

        public async Task CreateAsync(Category newCategory) =>
            await _categorysCollection.InsertOneAsync(newCategory);
        
        public async Task<List<Category>> GetAllAsync(CancellationToken token = default) {
            
            IAsyncCursor<Category> cursor = await _categorysCollection.FindAsync(_ => true, null, token);
            List<Category> categories = await cursor.ToListAsync(token);
            return categories;
        }
            
        public async Task<Category> GetById(string id, CancellationToken cancellationToken = default){
            var filter = Builders<Category>.Filter.Eq(nameof(Category.Id), id);

            var category = await _categorysCollection.Find(filter).FirstOrDefaultAsync();
            return category;
        }

        public async Task UpdateAsync(string id, Category categoryToUpdate, CancellationToken cancellationToken = default)
        {            
            await _categorysCollection.ReplaceOneAsync(x => Equals(x.Id, id), categoryToUpdate);
        }

        public async Task RemoveAsync(string id, CancellationToken cancellationToken = default) =>
            await _categorysCollection.DeleteOneAsync(x => Equals(x.Id, id));

        public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default) {
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