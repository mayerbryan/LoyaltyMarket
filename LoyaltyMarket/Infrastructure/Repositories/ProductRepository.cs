using Infrastructure.Configuration;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public ProductRepository(IMongoConfiguration mongoConfiguration)
        {
            _productsCollection = mongoConfiguration.GetProductCollection();
        }

        public async Task CreateAsync(Product newProduct, CancellationToken cancellationToken  = default) =>
            await _productsCollection.InsertOneAsync(newProduct);
        
        public async Task<List<Product>> GetAllAsync(CancellationToken token = default) {
            
            IAsyncCursor<Product> cursor = await _productsCollection.FindAsync(_ => true, null, token);
            List<Product> categories = await cursor.ToListAsync(token);
            return categories;
        }
            
        public async Task<Product> GetById(string id, CancellationToken cancellationToken = default){
            var filter = Builders<Product>.Filter.Eq(nameof(Product.Id), id);

            var product = await _productsCollection.Find(filter).FirstOrDefaultAsync();
            return product;
        }

        public async Task UpdateAsync(string id, Product ProductToUpdate, CancellationToken cancellationToken = default)
        {            
            await _productsCollection.ReplaceOneAsync(x => Equals(x.Id, id), ProductToUpdate);
        }

        public async Task RemoveAsync(string id, CancellationToken cancellationToken = default) =>
            await _productsCollection.DeleteOneAsync(x => Equals(x.Id, id));

        public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default) {
            var exists = false;

            var result = await _productsCollection.CountDocumentsAsync(entity => Equals(entity.Id, id));
            if (Equals(result, 1))
            {
                exists = true;
            }

            return exists;
        }
        
    }
}