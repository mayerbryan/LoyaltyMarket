// using MongoDB.Driver;
// using Microsoft.Extensions.Options;
// using Domain.Models;
// using MongoDB.Bson;

// namespace Domain.Services
// {
//     public class ProductService : IProductService
//     {
//         private readonly IMongoCollection<Product> _ProductsCollection;

//         public ProductService(
//             IOptions<LoyaltyMarketDatabaseSettings> loyaltyMarketDatabaseSettings){
//                 var mongoClient = new MongoClient(loyaltyMarketDatabaseSettings.Value.ConnectionString);
//                 var mongoDatabase = mongoClient.GetDatabase(loyaltyMarketDatabaseSettings.Value.DatabaseName);
//                 _ProductsCollection = mongoDatabase.GetCollection<Product>(loyaltyMarketDatabaseSettings.Value.ProductsCollectionName);
//         }

//         public async Task<List<Product>> GetAllAsync() 
//         {
//             var pipeline = new BsonDocument[]
//             {
//                 new BsonDocument("$lookup", new BsonDocument
//                 {
//                     { "from", "CategoriesCollection" },
//                     { "localField", "CategoryId" },
//                     { "foreignField", "_id" },
//                     { "as", "product_category" }
//                 }),
//                 new BsonDocument("$unwind", "$product_category"),
//                 new BsonDocument("$project", new BsonDocument{
//                     { "_id", 1},
//                     { "CategoryId", 1},
//                     { "Name", 1},
//                     { "Description", 1},
//                     { "Price", 1},
//                     { "Color", 1},
//                     { "CategoryName", "$product_category.Name" }
//                 })
//             };

//             var results = await _ProductsCollection.Aggregate<Product>(pipeline).ToListAsync();
//             return results;

//         }

//         public async Task<Product?> GetByIdAsync(string id) =>
//             await _ProductsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

//         public async Task CreateAsync(Product newBook) =>
//             await _ProductsCollection.InsertOneAsync(newBook);

//         public async Task UpdateAsync(string id, Product updatedBook) =>
//             await _ProductsCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

//         public async Task RemoveAsync(string id) =>
//             await _ProductsCollection.DeleteOneAsync(x => x.Id == id);
//     }
// }