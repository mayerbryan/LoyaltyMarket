using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime.Internal;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Domain.Models;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Entities;
using MongoDB.Bson;

namespace Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(CategoryRequestModel model)
        {
            Category entity = model;
            await _categoryRepository.CreateAsync(entity);
        }   

        public async Task<IEnumerable<CategorySummaryResponseModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var resultList = await _categoryRepository.GetAllAsync(cancellationToken);
            var modelList = resultList.Select(item => (CategorySummaryResponseModel)item);            
            return modelList;
        }

        public async Task<CategorySummaryResponseModel> GetById(string id, CancellationToken cancellationToken = default)
        {
            CategorySummaryResponseModel response = await _categoryRepository.GetById(id, cancellationToken);
            return response;
        }

        public async Task UpdateAsync(string id, CategoryRequestModel model, CancellationToken cancellationToken = default)
        {
            await _categoryRepository.UpdateAsync(id, model, cancellationToken);
        }

        public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
        {
            await _categoryRepository.RemoveAsync(id, cancellationToken);
        }
    }
}