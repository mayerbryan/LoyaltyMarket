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

        public async Task UpdateAsync(string id, CategoryRequestModel model)
        {
            await _categoryRepository.UpdateAsync(id, model);
        }

        public async Task RemoveAsync(string id)
        {
            await _categoryRepository.RemoveAsync(id);
        }

        public async Task<IEnumerable<CategorySummaryResponseModel>> GetAllAsync(CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            var resultList = await _categoryRepository.GetAllAsync(token);
            var modelList = resultList.Select(item => (CategorySummaryResponseModel)item);            
            return modelList;
        }
    }
}