using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoyaltyMarket.Models;

namespace LoyaltyMarket.Server.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(string id);
        Task<Category> CreateCategory(Category category);
        Task<bool> UpdateCategory(string id, Category updatedCategory);
        Task<bool> DeleteCategory(string id);
    }
}