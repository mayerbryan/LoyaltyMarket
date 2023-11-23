using Infrastructure.Data.Entities;

namespace Domain.Models
{
    public class ProductResponseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
        public string Color { get; set; }

        public static implicit operator ProductResponseModel(Product product)
        {
            if (product == null)
            {
                return new ProductResponseModel();
            }
            
            ArgumentException.ThrowIfNullOrEmpty(product.Id.ToString());

            var model = new ProductResponseModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Color = product.Color
            };

            return model;
        }
    }
}