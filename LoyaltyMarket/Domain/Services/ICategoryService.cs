using Domain.Models;
using MongoDB.Bson;

namespace Domain.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategorySummaryResponseModel>> GetAllAsync(CancellationToken token = default);
        Task CreateAsync(CategoryRequestModel model);
        Task UpdateAsync(string id, CategoryRequestModel model);
        Task RemoveAsync(string id);
    }
}