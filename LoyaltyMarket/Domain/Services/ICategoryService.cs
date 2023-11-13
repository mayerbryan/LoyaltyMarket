using Domain.Models;
using MongoDB.Bson;

namespace Domain.Services
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryRequestModel model);
        Task<IEnumerable<CategorySummaryResponseModel>> GetAllAsync(CancellationToken token = default);
        Task<CategorySummaryResponseModel> GetById(string id, CancellationToken cancellationToken = default);        
        Task UpdateAsync(string id, CategoryRequestModel model, CancellationToken cancellationToken = default);        
        Task RemoveAsync(string id, CancellationToken cancellationToken = default);
    }
}