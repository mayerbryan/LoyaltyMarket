using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoyaltyMarket.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace LoyaltyMarket.Server.Data
{
    public class DataBaseContext : DbContext
    {
        private readonly IMongoDatabase _database;

        public DataBaseContext(IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase("LoyaltyMarketDB");
        }

        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories");

        
    }
}