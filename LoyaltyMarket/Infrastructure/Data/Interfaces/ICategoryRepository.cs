
using Infrastructure.Data.Entities;

namespace Infrastructure.Data.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync(CancellationToken token  = default);
        Task CreateAsync(Category category);
        Task UpdateAsync(string id, Category category);
        Task RemoveAsync(string id);
        Task<bool> ExistsAsync(string id);
    }
}