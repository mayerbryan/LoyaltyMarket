using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(string id);

        Task CreateAsync(Category category);
        Task UpdateAsync(string id, Category category);
        Task RemoveAsync(string id);

    }
}