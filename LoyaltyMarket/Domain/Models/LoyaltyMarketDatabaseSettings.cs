using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class LoyaltyMarketDatabaseSettings
    {
        public string ConnectionString {get;set;} = null!;
        public string LoyaltyMarketDatabase { get; set; } = null!;

        public string ProductsCollectionName { get; set; } = null!;

        public string CategoriesCollectionName {get; set;} = null!;
    }
}