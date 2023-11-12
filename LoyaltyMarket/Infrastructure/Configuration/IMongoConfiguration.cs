using System;
using System.Collections.Generic;

using Infrastructure.Data.Entities;
using MongoDB.Driver;

namespace Infrastructure.Configuration
{
    public interface IMongoConfiguration
    {
        public IMongoCollection<Category> GetCategoryCollection();

        //public IMongoCollection<Product> GetProductCollection();

    }
}