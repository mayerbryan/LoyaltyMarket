using Domain.Models;

namespace Domain.Services
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryRequestModel model);
        Task<IEnumerable<CategoryResponseModel>> GetAllAsync(CancellationToken token = default);
        Task<CategoryResponseModel> GetById(string id, CancellationToken cancellationToken = default);        
        Task UpdateAsync(string id, CategoryUpdateModel model, CancellationToken cancellationToken = default);        
        Task RemoveAsync(string id, CancellationToken cancellationToken = default);
    }
}