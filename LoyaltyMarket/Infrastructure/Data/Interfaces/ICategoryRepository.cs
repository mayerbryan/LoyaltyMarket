
using Infrastructure.Data.Entities;

namespace Infrastructure.Data.Interfaces
{
    public interface ICategoryRepository
    {
        Task CreateAsync(Category category);
        Task<List<Category>> GetAllAsync(CancellationToken token  = default);
        Task<Category> GetById(string id, CancellationToken cancellationToken = default);
        Task UpdateAsync(string id, Category category, CancellationToken cancellationToken = default);        
        Task RemoveAsync(string id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    }
}