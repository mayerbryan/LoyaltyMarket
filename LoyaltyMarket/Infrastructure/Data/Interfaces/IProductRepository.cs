
using Infrastructure.Data.Entities;

namespace Infrastructure.Data.Interfaces
{
    public interface IProductRepository
    {
        Task CreateAsync(Product Product);
        Task<List<Product>> GetAllAsync(CancellationToken token  = default);
        Task<Product> GetById(string id, CancellationToken cancellationToken = default);
        Task UpdateAsync(string id, Product Product, CancellationToken cancellationToken = default);        
        Task RemoveAsync(string id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);
    }
}