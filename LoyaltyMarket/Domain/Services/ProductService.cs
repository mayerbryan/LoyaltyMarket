using Domain.Models;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Entities;

namespace Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _ProductRepository;

        public ProductService(IProductRepository ProductRepository)
        {
            _ProductRepository = ProductRepository;
        }

        public async Task CreateAsync(ProductRequestModel model)
        {
            Product entity = model;
            await _ProductRepository.CreateAsync(entity);
        }   

        public async Task<IEnumerable<ProductResponseModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var resultList = await _ProductRepository.GetAllAsync(cancellationToken);
            var modelList = resultList.Select(item => (ProductResponseModel)item);            
            return modelList;
        }

        public async Task<ProductResponseModel> GetById(string id, CancellationToken cancellationToken = default)
        {
            ProductResponseModel response = await _ProductRepository.GetById(id, cancellationToken);
            return response;
        }

        public async Task UpdateAsync(string id, ProductRequestModel model, CancellationToken cancellationToken = default)
        {
            await _ProductRepository.UpdateAsync(id, model, cancellationToken);
        }

        public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
        {
            await _ProductRepository.RemoveAsync(id, cancellationToken);
        }
    }
}