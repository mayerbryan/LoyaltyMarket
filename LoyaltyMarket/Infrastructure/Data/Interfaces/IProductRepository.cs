
using Infrastructure.Data.Entities;

namespace Infrastructure.Data.Interfaces
{
    public interface IProductRepository
    {
        Task CreateAsync(Product Product, CancellationToken cancellationToken  = default);
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken  = default);
        Task<Product> GetById(string id, CancellationToken cancellationToken = default);
        Task UpdateAsync(string id, Product Product, CancellationToken cancellationToken = default);        
        Task RemoveAsync(string id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    }
}