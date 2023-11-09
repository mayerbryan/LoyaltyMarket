using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoyaltyMarket.Models;
using LoyaltyMarket.Server.Data;
using MongoDB.Driver;

namespace LoyaltyMarket.Server.Services
{
    public class CategoryService
    {
        private readonly DataBaseContext _context;

        public CategoryService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.Find(c => true).ToListAsync();
        }

        public async Task<Category> GetCategoryById(string id)
        {
            return await _context.Categories.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Category> CreateCategory(Category category)
        {
            await _context.Categories.InsertOneAsync(category);
            return category;
        }

        public async Task<bool> UpdateCategory(string id, Category updatedCategory)
        {
            var result = await _context.Categories.FindOneAndUpdateAsync(
                Builders<Category>.Filter.Eq(c => c.Id, id),
                Builders<Category>.Update
                    .Set(c => c.Name, updatedCategory.Name)
                    .Set(c => c.Description, updatedCategory.Description),
                new FindOneAndUpdateOptions<Category> { ReturnDocument = ReturnDocument.After });

            return result != null;
        }

        public async Task<bool> DeleteCategory(string id)
        {
            var result = await _context.Categories.DeleteOneAsync(c => c.Id == id);
            return result.DeletedCount > 0;
        }
    }
}