using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(string id);

        Task CreateAsync(Product product);
        Task UpdateAsync(string id, Product product);
        Task RemoveAsync(string id);

    }
}