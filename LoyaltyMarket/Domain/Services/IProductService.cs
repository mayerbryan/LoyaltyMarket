using Domain.Models;

namespace Domain.Services
{
    public interface IProductService
    {
        Task CreateAsync(ProductRequestModel model, CancellationToken cancellationToken  = default);
        Task<IEnumerable<ProductResponseModel>> GetAllAsync(CancellationToken token = default);
        Task<ProductResponseModel> GetById(string id, CancellationToken cancellationToken = default);        
        Task UpdateAsync(string id, ProductRequestModel model, CancellationToken cancellationToken = default);        
        Task RemoveAsync(string id, CancellationToken cancellationToken = default);

    }
}