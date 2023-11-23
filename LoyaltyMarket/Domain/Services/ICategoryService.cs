using Domain.Models;

namespace Domain.Services
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryRequestModel model, CancellationToken cancellationToken = default);
        Task<IEnumerable<CategoryResponseModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CategoryResponseModel> GetById(string id, CancellationToken cancellationToken = default);        
        Task UpdateAsync(string id, CategoryUpdateModel model, CancellationToken cancellationToken = default);        
        Task RemoveAsync(string id, CancellationToken cancellationToken = default);
    }
}