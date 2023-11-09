using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoyaltyMarket.Models;

namespace LoyaltyMarket.Server.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Product> CreateProduct(string name, string description, decimal price, string category, string color);
        Task<bool> UpdateProduct(int id, Product updatedProduct);
        Task<bool> DeleteProduct(int id);
    }
}